using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Protocols.Dto;
using Protocols;

namespace ClientNetFrame
{
    class Start
    {
        static void Main(string[] args)
        {
            TestRegister();
            while (true)
            {
                Thread.Sleep(1000);
                if (NetCenter.Instance.Messages.Count > 0)
                {
                    SocketModel model = NetCenter.Instance.Messages[0];
                    NetCenter.Instance.Messages.Remove(model);
                    Console.WriteLine("收到伺服器回應!! 請求返回類型:" + model.Type + " 回應訊息:" + model.GetMessage<CreateResult>().ToString());
                    Console.WriteLine("<<TYPE_LOGIN = 0,TYPE_OGC = 1>>");
                }
                //NetCenter.Instance.Send(Protocols.Protocol.LOGIN_CREQ, 0, (int)Protocols.Protocol.Command.RegisterRequest, null);
            }
        }

        static void TestRegister()
        {
            NetCenter.Instance.Init();
            NetCenter.Instance.Connect("127.0.0.1", 9527);
            AccountInfoDTO accountInfo = new AccountInfoDTO()
            {
                Account = "asdfff",
                Password = "hahahah"
            };
            NetCenter.Instance.Send(Protocols.Protocol.LOGIN_CREQ, 0, (int)Protocols.Protocol.Command.RegisterRequest, accountInfo);
        }
    }
}
