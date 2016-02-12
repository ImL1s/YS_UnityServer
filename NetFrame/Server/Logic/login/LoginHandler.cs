/*
 * Author:ImL1s
 *
 * Date:2016/02/08
 *
 * Email:ImL1s@outlook.com
 *
 * description:
 */

using NetFrame;
using NetFrame.Tool;
using Protocols;
using Protocols.Dto;
using Server.Bll;
using Server.Tool;
using System;

namespace Server.Logic.login
{
    class LoginHandler : AbsOnceHandler,IHandler
    {
        IAccountBll accountBll = BllFactory.accountBll;

        public override int Area
        {
            get
            {
                return (int)Protocols.Protocol.Area.None;
            }
        }

        public override byte Type
        {
            get
            {
                return (byte)Protocols.Protocol.Type.Login;
            }
        }

        public void ClientClose(UserToken token, string error)
        {
            OutPutManager.RecordOffline(token);
        }

        public void MessageReceive(UserToken token, SocketModel message)
        {
            OutPutManager.WriteConsole("LoginHandler 收到消息!!");

            switch (message.Command)
            {
                case (int)Protocol.Command.LoginRequest:
                    Login(token, message.GetMessage<AccountInfoDTO>());
                    break;

                case (int)Protocol.Command.LoginResponse:
                    break;

                case (int)Protocol.Command.RegisterRequest:
                    Register(token, message.GetMessage<AccountInfoDTO>());
                    break;

                case (int)Protocol.Command.RegisterResponse:
                    break;

                default:
                    OutPutManager.WriteConsole("收到用戶發送為未知Command，用戶:" + token.Connecting.RemoteEndPoint,
                        true);
                    break;
            }
        }

        /// <summary>
        /// 登入處理.
        /// </summary>
        /// <param name="token"></param>
        /// <param name="accountInfo"></param>
        private void Login(UserToken token, AccountInfoDTO accountInfo)
        {
            if(accountInfo == null)
            {
                OutPutManager.WriteConsole("收到空的登入帳號訊息!!");
                return;
            }

            ExecutorPool.Instance.Execute(() =>
            {
                LoginResult result = accountBll.Login(token, accountInfo.Account, accountInfo.Password);
                WriteClient(token, (int)Protocol.Command.LoginResponse, result);
            });
        }

        /// <summary>
        /// 註冊處理.
        /// </summary>
        /// <param name="token"></param>
        /// <param name="accountInfo"></param>
        private void Register(UserToken token, AccountInfoDTO accountInfo)
        {
            if (accountInfo == null)
            {
                OutPutManager.WriteConsole("收到空的創造帳號訊息!!");
                return;
            }
            ExecutorPool.Instance.Execute(() =>
            {
                CreateResult result = accountBll.Create(token, accountInfo.Account, accountInfo.Password);
                WriteClient(token, (int)Protocol.Command.RegisterResponse, result);
            });
        }
    }
}
