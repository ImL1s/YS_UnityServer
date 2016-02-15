/*
 * Author:ImL1s
 *
 * Date:2016/02/08
 *
 * Email:ImL1s@outlook.com
 *
 * description:
 *
 */

using NetFrame;
using NetFrame.DataEncoding;
using Protocols;

namespace Server.Logic
{
    public abstract class AbsOnceHandler
    {
        public abstract byte Type { get; }

        public abstract int Area { get; }
        
        /// <summary>
        /// 向客戶端發送消息
        /// </summary>
        /// <param name="token"></param>
        /// <param name="command"></param>
        /// <param name="message"></param>
        public void WriteClient(UserToken token, int command , object message = null)
        {
            WriteClient(token, Area, command, message);
        }

        /// <summary>
        /// 向客戶端發送消息
        /// </summary>
        /// <param name="token"></param>
        /// <param name="area"></param>
        /// <param name="command"></param>
        /// <param name="message"></param>
        public void WriteClient(UserToken token, int area, int command, object message)
        {
            WriteClient(token, Type, area, command, message);
        }

        /// <summary>
        /// 向客戶端發送消息
        /// </summary>
        /// <param name="token"></param>
        /// <param name="type"></param>
        /// <param name="area"></param>
        /// <param name="command"></param>
        /// <param name="message"></param>
        public void WriteClient(UserToken token, byte type, int area, int command, object message)
        {
            byte[] value = MessageCoding.Encode(CreateSocketModel(type, area, command, message));
            value = LengthCoding.Encode(value);
            token.Send(value);
        }

        /// <summary>
        /// 取得一個SocketModel
        /// </summary>
        /// <param name="type"></param>
        /// <param name="area"></param>
        /// <param name="command"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        private object CreateSocketModel(byte type, int area, int command, object message)
        {
            return new SocketModel(type, area, command, message);
        }
    }
}
