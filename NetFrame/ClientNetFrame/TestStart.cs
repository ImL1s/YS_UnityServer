using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Protocols.Dto;
using Protocols;

namespace ClientNetFrame
{
    class TestStart
    {
        private static string IP = "123.193.91.27";
        private static int IPInt;

        static void Main1(string[] args)
        {

        StartConnect:
            try
            {
                NetCenter.Instance.Init();
                NetCenter.Instance.Connect(IP, 9527);
                Console.WriteLine("連線至:" + IP);
            }
            catch (Exception error)
            {
                while (true)
                {
                    Console.WriteLine("連線失敗...請手動輸入伺服器地址:");
                    IP = Console.ReadLine().Trim();
                    if (int.TryParse(IP, out IPInt)) break;
                }

                goto StartConnect;
            }



            //TestRegister();
            TestLogin();

            while (true)
            {
                Thread.Sleep(1000);
                if (NetCenter.Instance.Messages.Count > 0)
                {
                    SocketModel model = NetCenter.Instance.Messages[0];
                    NetCenter.Instance.Messages.Remove(model);
                    //Console.WriteLine("收到伺服器回應!! 請求返回類型:" + model.Type + " 回應訊息:" + model.GetMessage<CreateResult>().ToString());
                    Console.WriteLine("收到伺服器回應!! 請求返回類型:" + model.Type + " 回應訊息:" + model.GetMessage<LoginResult>().ToString());
                    Console.WriteLine("<<TYPE_LOGIN = 0,TYPE_OGC = 1>>");
                }
                //NetCenter.Instance.Send(Protocols.Protocol.LOGIN_CREQ, 0, (int)Protocols.Protocol.Command.RegisterRequest, null);
            }
        }

        static void TestRegister()
        {
            AccountInfoDTO accountInfo = new AccountInfoDTO()
            {
                Account = "asdfff",
                Password = "hahahah"
            };
            NetCenter.Instance.Send(Protocols.Protocol.LOGIN_CREQ, 0, (int)Protocols.Protocol.Command.RegisterRequest, accountInfo);
        }

        static void TestLogin()
        {
            AccountInfoDTO accountInfo = new AccountInfoDTO()
            {
                Account = "asdfff",
                Password = "hahaha"
            };
            NetCenter.Instance.Send(Protocols.Protocol.LOGIN_CREQ, 0, (int)Protocols.Protocol.Command.LoginRequest, accountInfo);
        }
    }
}
