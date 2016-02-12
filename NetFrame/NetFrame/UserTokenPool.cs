using NetFrame.ABS;
using System;
using System.Collections.Generic;
using System.Net.Sockets;
/*
*Des:
*      此類為一個連接池，用來管理所有的userToken(連接對象),使用此池可以降低創建對象的開銷，
*      並且達重複使用節省資源的目的.
*/
namespace NetFrame
{
    class UserTokenPool
    {
        private Stack<UserToken> pool;

        public int Size
        {
            get { return pool.Count; }
        }

        /// <summary>
        /// 初始化UserToken連接池
        /// </summary>
        /// <param name="maxCount">UserToken最大數目</param>
        /// <param name="IOCcallBack">當UserToken收/發消息時的回調函數</param>
        /// <param name="LE">長度(頭)編碼器</param>
        /// <param name="LD">長度(頭)解碼器</param>
        /// <param name="decode">資料解碼器</param>
        /// <param name="encode">資料編碼器</param>
        public UserTokenPool(AbsHandlerCenter center,int maxCount,EventHandler<SocketAsyncEventArgs> IOCcallBack,Delegate LE,Delegate LD,
            Delegate encode,Delegate decode)
        {
            pool = new Stack<UserToken>(maxCount);
            int idIndex = 0;

            for (int i = 0; i <= maxCount; i++)
            {
                UserToken userToken = new UserToken();

                userToken.TokenID = idIndex;

                // 增加將SocketAsyncEventArgs IO結束後回調函數
                userToken.SetReceiveSAEACallback(IOCcallBack);
                userToken.SetSendASEACallback(IOCcallBack);

                userToken.LD = (EventDelegate.LengthDecode)LD;
                userToken.LE = (EventDelegate.LengthEncode)LE;

                userToken.Decode = (EventDelegate.Decode)decode;
                userToken.Encode = (EventDelegate.Encode)encode;

                userToken.ApplicationCenter = center;

                pool.Push(userToken);

                idIndex++;
            }
        }

        /// <summary>
        /// 取得一個UserToken
        /// </summary>
        /// <returns></returns>
        public UserToken Pop()
        {
            return pool.Pop();
        }

        /// <summary>
        /// 將一個使用完畢的對象放入池中
        /// </summary>
        /// <param name="token"></param>
        public void push(UserToken token)
        {
            if(token != null)
            {
                pool.Push(token);
            }
        }


    }
}
