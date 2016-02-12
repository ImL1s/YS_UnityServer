using NetFrame;
using System.Threading;

namespace Server
{
    class Server
    {
        static void Main(string[] args)
        {
            ServerStart ss = new ServerStart();
            ss.InitServer(100, new HandlerCenter());
            ss.Start(50, 9527, 100);

            CommandManager.ReceiveInput();
            // TODO控制台輸入quit關閉後，處理伺服器
        }
    }
}
