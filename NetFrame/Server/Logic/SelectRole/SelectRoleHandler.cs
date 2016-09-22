/*
 * Author:ImL1s
 *
 * Date:2016/03/02
 *
 * description:選擇角色處理模塊.
 *
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetFrame;
using Protocols;
using NNProtocols.Dto;
using Server.Tool;
using NetFrame.Tool;
using Protocols.Dto;
using Server.Bll.Interface;
using Server.Bll;

namespace Server.Logic.SelectRole
{
    public class SelectRoleHandler : AbsOnceHandler, IHandler
    {
        private IPlayerRoleBll playerRoleBll = BllFactory.playerRoleBll;

        public override Protocol.Area Area
        {
            get
            {
                return Protocols.Protocol.Area.None;
            }
        }

        public override Protocol.Type Type
        {
            get
            {
                return Protocol.Type.SelectRole;
            }
        }

        public void ClientClose(UserToken token, string error)
        {
            playerRoleBll.ClientClose(token, error);
        }

        public void MessageReceive(UserToken token, SocketModel message)
        {
            switch ((Protocol.Command)message.Command)
            {
                case Protocol.Command.SelectRoleRequest:
                    ProcessSelectRole(token, message.GetMessage<RoleDTO>());
                    break;

                default:
                    break;
            }
        }

        // 處理選擇(創建)角色.
        private void ProcessSelectRole(UserToken token,RoleDTO selectRoleDTO)
        {
            ExecutorPool.Instance.Execute(() =>
            {
                OutPutManager.WriteConsole("處理選擇角色!");
                SelectRoleResult result = playerRoleBll.CreateRole(token, selectRoleDTO);
                WriteClient(token, Protocol.Command.SelectRoleResponse, result);
            });
        }
    }
}
