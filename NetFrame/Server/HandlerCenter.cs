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

using NetFrame.ABS;
using System;
using NetFrame;
using Server.Logic;
using NetFrame.Tool;
using Protocols;

namespace Server
{
    class HandlerCenter : AbsHandlerCenter
    {
        protected IHandler loginHandler = new Logic.login.LoginHandler();
        protected IHandler selectRoleHandler = new Logic.SelectRole.SelectRoleHandler();


        public override void ClientClose(UserToken token, string error)
        {
            selectRoleHandler.ClientClose(token, error);
            loginHandler.ClientClose(token, error);
        }

        public override void ClientConnect(UserToken token)
        {
            OutPutManager.WriteConsole("[客戶端連接] IP:" + token.Connecting.RemoteEndPoint);
        }

        public override void MessageRecive(UserToken token, object message)
        {
            //SocketModel model = message as SocketModel;
            SocketModel model = (SocketModel)message;
            OutPutManager.WriteConsole("HandlerCenter 收到消息!!");

            switch (model.Type)
            {
                case (byte)Protocol.Type.Login:

                    loginHandler.MessageReceive(token, model);
                    break;

                case (byte)Protocol.Type.SelectRole:

                    selectRoleHandler.MessageReceive(token, model);
                    break;
            }
        }
    }
}
