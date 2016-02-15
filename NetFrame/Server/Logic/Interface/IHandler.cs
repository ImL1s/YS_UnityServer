using NetFrame;
using Protocols;

namespace Server.Logic
{
    interface IHandler
    {
        /// <summary>
        /// 客戶端關閉
        /// </summary>
        /// <param name="token"></param>
        /// <param name="error"></param>
        void ClientClose(UserToken token, string error);

        /// <summary>
        /// 收到消息
        /// </summary>
        /// <param name="token"></param>
        /// <param name="message"></param>
        void MessageReceive(UserToken token, SocketModel message);
    }
}