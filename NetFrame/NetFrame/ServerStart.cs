using NetFrame.ABS;
using NetFrame.DataEncoding;
using NetFrame.EventDelegate;
using NetFrame.Tool;
using System;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace NetFrame
{
    /// <summary>
    /// 本類用於開啟伺服器與傳輸層數據的轉送
    /// </summary>
    public class ServerStart
    {
        #region field

        private Socket listenSocket;
        private int maxCount;

        private LengthEncode mLE;                                           // 長度編碼器，用於傳入給Utoken編碼消息
        private LengthDecode mLD;                                           // 長度解碼器，用於傳入給Utoken解碼消息
        private Encode mEncode;                                             // 資料編碼器
        private Decode mDecode;                                             // 資料解碼器
        private AbsHandlerCenter handlerCenter;                             // 應用層消息處理中心
        private Semaphore semaphore;                                        // 線程鎖
        private UserTokenPool tokenPool;                                    // 用戶連接對象池
        private EventHandler<SocketAsyncEventArgs> mIOCompletedCallback;    // SocketAsyncEventArgs IO結束，回調函數.

        #endregion


        #region Property

        // IO結束回調
        public EventHandler<SocketAsyncEventArgs> IOCompletedCallback
        {
            get
            {
                return mIOCompletedCallback;
            }
        }

        public Encode Encode
        {
            get
            {
                return mEncode;
            }

            set
            {
                mEncode = value;
            }
        }

        public Decode Decode
        {
            get
            {
                return mDecode;
            }

            set
            {
                mDecode = value;
            }
        }


        #endregion

        /// <summary>
        /// 初始化伺服器所需要屬性.
        /// </summary>
        public ServerStart()
        {
            mIOCompletedCallback = new EventHandler<SocketAsyncEventArgs>(this.IOCompleted);
            mLE = LengthCoding.Encode;
            mLD = LengthCoding.Decode;
            mEncode = MessageCoding.Encode;
            mDecode = MessageCoding.Decode;
        }

        /// <summary>
        /// 初始化伺服器
        /// </summary>
        /// <param name="maxSemaphore">信號量最大值</param>
        public void InitServer(int maxSmpCount,AbsHandlerCenter handlerCenter)
        {
            listenSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            semaphore = new Semaphore(maxSmpCount, maxSmpCount);
            this.handlerCenter = handlerCenter;

            ConsoleLog("伺服器初始化成功， 設定最大承受量為:" + maxSmpCount);
        }

        /// <summary>
        /// 啟動伺服器
        /// </summary>
        /// <param name="port">要綁定的端口</param>
        /// <param name="userTokenPoolMax">最大用戶連接池個數</param>
        /// <param name="queueCount">最大隊列個數</param>
        public void Start(int queueCount, int port,int userTokenPoolMax)
        {
            // 初始化玩家連接池
            tokenPool = new UserTokenPool(handlerCenter,userTokenPoolMax, mIOCompletedCallback, mLE, mLD, mEncode, mDecode);

            listenSocket.Bind(new IPEndPoint(IPAddress.Any, port));
            listenSocket.Listen(queueCount);

            ConsoleLog("伺服器啟動成功， 最大隊列數:" + queueCount + "," + "綁定端口:" + port);

            StartAccept(null);
        }

        // 開始接受客戶端連入
        private void StartAccept(SocketAsyncEventArgs e)
        {
            if(e == null)
            {
                e = new SocketAsyncEventArgs();
                e.Completed += new EventHandler<SocketAsyncEventArgs>(AcceptComleted);
            }
            else
            {
                e.AcceptSocket = null;
            }

            semaphore.WaitOne();

            listenSocket.AcceptAsync(e);
            //// 這裡傳回的bool值代表是當接收客戶端時是否同步，false = 當下立即，必須手動處理(回調函數不會調用)
            //if (!listenSocket.AcceptAsync(e))
            //{
            //    ProcessAccept(e);
            //}
        }


        // 處理異步/同步連接過來的要求
        private void ProcessAccept(SocketAsyncEventArgs e)
        {
            UserToken token = tokenPool.Pop();
            token.Connecting = e.AcceptSocket;

            StartReceive(token);
            StartAccept(e);
        }

        private void AcceptComleted(object sender, SocketAsyncEventArgs e)
        {
            ProcessAccept(e);
        }

        // 開始一個token接收他服務的客戶端的數據
        private void StartReceive(UserToken token)
        {

            token.Connecting.ReceiveAsync(token.ReceiveSAEA);
            //// 如果是同步(當下處理完畢)，手動調用
            //if (!token.Connecting.ReceiveAsync(token.ReceiveSAEA))
            //{
            //    ProcessReceive(token.ReceiveSAEA);
            //}
        }

        // 處理接收完畢的數據
        private void ProcessReceive(SocketAsyncEventArgs receiveSAEA)
        {
            // 取得很receiveSAEA相關的Socket
            UserToken token = receiveSAEA.UserToken as UserToken;

            //try
            {
                // 檢查客戶端是否有傳送資料
                if (token.ReceiveSAEA.BytesTransferred > 0 && token.ReceiveSAEA.SocketError == SocketError.Success)
                {
                    // 將資料取出，放進新的變量:message裡面
                    byte[] message = new byte[token.ReceiveSAEA.BytesTransferred];
                    Buffer.BlockCopy(token.ReceiveSAEA.Buffer, 0, message, 0,message.Length);

                    OutPutManager.WriteConsole("收到資料!字節數量:" + message.Length);
                    token.Receive(message);
                    StartReceive(token);
                }
                // 客戶端沒有傳送資料、或是socket出錯了
                else
                {
                    // 網路出錯
                    if (token.ReceiveSAEA.SocketError != SocketError.Success)
                    {
                        ClientClose(token, receiveSAEA.SocketError.ToString());
                    }
                    // 主動斷開
                    else
                    {
                        ClientClose(token, "客戶端(" + ((IPEndPoint)(token.Connecting.RemoteEndPoint)).Address + ")發送了0個byte,判斷為斷線");
                        ClientClose(token, "客戶端(" + token.TokenID + ")斷線");
                    } 
                }
            }
            //catch(Exception e)
            //{
            //    //ClientClose(token, "客戶端(" + token.TokenID + ")斷線,Error: " + e.ToString());
            //    ClientClose(token, "客戶端(" + token.TokenID + ")斷線，IP地址: " + ((IPEndPoint)(token.Connecting.RemoteEndPoint)).Address);
            //}
        }

        private void IOCompleted(object sender,SocketAsyncEventArgs e)
        {
            // 檢查上一次SocketAsyncEventArgs的操作是什麼
            if (e.LastOperation == SocketAsyncOperation.Receive)
            {
                ProcessReceive(e);
            }
            else
            {
                ProcessSend(e);
            }
        }

        // TODO處理發送
        private void ProcessSend(SocketAsyncEventArgs e)
        {
            //throw new NotImplementedException();
            UserToken token = e.UserToken as UserToken;
            if (e.SocketError != SocketError.Success) ClientClose(token, e.SocketError.ToString());
            else token.Sending();
        }

        // 處理客戶端斷線、離線
        private void ClientClose(UserToken token, string error)
        {
            OutPutManager.WriteConsole(error);
            semaphore.Release();
            tokenPool.push(token);
        }

        /// <summary>
        /// 輸出文字
        /// </summary>
        /// <param name="message"></param>
        private void ConsoleLog(string message)
        {
            OutPutManager.WriteConsole(message);
        }
    }
}
