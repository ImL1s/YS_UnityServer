using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetFrame;
using Protocols;
using NNProtocols.Dto;

namespace Server.Logic.SelectRole
{
    public class SelectRoleHandler : AbsOnceHandler, IHandler
    {
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
            
        }

        public void MessageReceive(UserToken token, SocketModel message)
        {
            switch ((Protocol.Command)message.Command)
            {
                case Protocol.Command.SelectRoleRequest:
                    ProcessSelectRole(message.GetMessage<SelectRoleDTO>());
                    break;

                default:
                    break;
            }
        }

        // 處理選擇角色.
        private void ProcessSelectRole(SelectRoleDTO selectRoleDTO)
        {

        }
    }
}
