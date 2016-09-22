using NetFrame.ABS;
using NetFrame.EventDelegate;
using System;
using System.Collections.Generic;
using System.Net.Sockets;

namespace NetFrame
{
    public class UserToken
    {
        #region field

        private int tokenID;
        private AbsHandlerCenter applicationCenter;             // 應用層接收中心
        private Socket connecting;                              // 本用戶的連接對象
        private SocketAsyncEventArgs receiveSAEA;               // 接收資料的事件對象
        private SocketAsyncEventArgs sendSAEA;                  // 發送資料的事件對象
        private List<byte> cache = new List<byte>();            // 接收資料的緩存

        private LengthEncode mLE;                               // 長度編碼方法
        private LengthDecode mLD;                               // 長度解碼方法
        private Encode mEncode;                                 // 資料編碼方法
        private Decode mDecode;                                 // 資料解碼方法
        //private TokenOfflineHandler mTokenOffline;              // Token下線事件

        private bool isReading = false;                         // 是否正在讀取數據
        private bool isWriting = false;                         // 是否正在寫入數據
        private Queue<byte[]> sendQueue = new Queue<byte[]>();  // 向用戶準備發送的消息

        #endregion

        #region property

        /// <summary>
        /// 用戶的連接的Socket
        /// </summary>
        public Socket Connecting
        {
            get
            {
                return connecting;
            }

            set
            {
                connecting = value;
            }
        }

        /// <summary>
        /// 非同步通訊端接收作業
        /// </summary>
        public SocketAsyncEventArgs ReceiveSAEA
        {
            get
            {
                return receiveSAEA;
            }

            set
            {
                receiveSAEA = value;
            }
        }

        /// <summary>
        /// 非同步通訊端發送作業
        /// </summary>
        public SocketAsyncEventArgs SendSAEA
        {
            get
            {
                return sendSAEA;
            }

            set
            {
                sendSAEA = value;
            }
        }

        /// <summary>
        /// 本Token的ID
        /// </summary>
        public int TokenID
        {
            get
            {
                return tokenID;
            }

            set
            {
                tokenID = value;
            }
        }

        /// <summary>
        /// 長度加密器
        /// </summary>
        public LengthEncode LE
        {
            get
            {
                return mLE;
            }

            set
            {
                mLE = value;
            }
        }

        /// <summary>
        /// 長度解碼器
        /// </summary>
        public LengthDecode LD
        {
            get
            {
                return mLD;
            }

            set
            {
                mLD = value;
            }
        }

        /// <summary>
        /// 資料編碼(序列化)器
        /// </summary>
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

        /// <summary>
        /// 資料解碼(反序列化)器
        /// </summary>
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

        /// <summary>
        /// 用戶離線通知
        /// </summary>
        public TokenOfflineHandler ClientClose
        {
            get;set;
        }

        /// <summary>
        /// 應用層消息處理中心
        /// </summary>
        public AbsHandlerCenter ApplicationCenter
        {
            get
            {
                return applicationCenter;
            }

            set
            {
                applicationCenter = value;
            }
        }



        #endregion

        public UserToken()
        {
            receiveSAEA = new SocketAsyncEventArgs();
            sendSAEA = new SocketAsyncEventArgs();

            receiveSAEA.UserToken = this;
            sendSAEA.UserToken = this;

            receiveSAEA.SetBuffer(new byte[1024 * 1024], 0, 1024 * 1024);
            sendSAEA.SetBuffer(new byte[1024 * 1024], 0, 1024 * 1024);            
        }

        // 接收到資料，開始處理
        internal void Receive(byte[] buffer)
        {
            cache.AddRange(buffer);

            // 因為調用本Receive方法的是異步的線程，所以必須確認是否在讀取中
            if (!isReading)
            {
                isReading = true;
                ProcessData();
            }
        }

        /// <summary>
        /// 處理數據
        /// </summary>
        private void ProcessData()
        {
            byte[] buffer = null;

            if (LD != null)
            {
                buffer = LD(ref cache);

                // 資料不完整，返回等待資料
                if (buffer == null) { isReading = false; return; }

                // 去掉頭資料後，傳輸的資料為0
                if (cache.Count == 0) { isReading = false; return; }

                if (mDecode == null) throw new Exception("Message Decode is Null!!");

                object socketObj = mDecode(buffer);
                ApplicationCenter.MessageRecive(this, socketObj);

                // 尾遞歸(遞迴、Recursion)，防止處理消息中有其他消息到達而沒有處理
                ProcessData();
            }

        }

        /// <summary>
        /// 向用戶發送Byte消息
        /// </summary>
        /// <param name="value"></param>
        public void Send(byte[] value)
        {
            if(connecting == null)
            {
                ClientClose(this, "目標Token已經斷線.");
                return;
            }
            sendQueue.Enqueue(value);


            if (!isWriting)
            {
                isWriting = true;
                Sending();
            }
        
        }
        
        // 開始處理向本客戶端發送的訊息
        public void Sending()
        {
            if(sendQueue.Count == 0)
            {
                isWriting = false;
                return;
            }
            byte[] buffer = sendQueue.Dequeue();
            SendSAEA.SetBuffer(buffer, 0, buffer.Length);
            bool result = connecting.SendAsync(SendSAEA);
            //if (!result) SendSAEA.Completed(SendSAEA);
        }

        /// <summary>
        /// 設定發送的SocketAsyncEventArgs
        /// </summary>
        /// <param name="IOCcallBack"></param>
        public void SetSendASEACallback(EventHandler<SocketAsyncEventArgs> IOCcallBack)
        {
            this.ReceiveSAEA.Completed += IOCcallBack;          
        }

        /// <summary>
        /// 設定接收的SocketAsyncEventArgs
        /// </summary>
        /// <param name="IOCcallBack"></param>
        public void SetReceiveSAEACallback(EventHandler<SocketAsyncEventArgs> IOCcallBack)
        {
            this.SendSAEA.Completed += IOCcallBack;
        }
    }
}