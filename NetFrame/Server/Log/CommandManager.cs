using NetFrame.Tool;
using System;

namespace Server
{
    public static class CommandManager
    {
        /// <summary>
        /// 接收用戶輸入命令行工具
        /// </summary>
        public static void ReceiveInput()
        {
            while(true)
            {
                string str = Console.ReadLine();

                // 製作命令提示元工具
                //switch (str)
                //{
                //    case "Quit":
                        
                //        break;

                //    default:
                //        break;
                //}

                if(str == "quit")
                {
                    OutPutManager.WriteConsole("確認退出?(y/n)");
                    string s = Console.ReadLine();

                    if (s == "y") break;
                    else OutPutManager.WriteConsole("伺服器繼續執行^_^");
                }
            }
        }
    }
}
