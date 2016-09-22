using System;

namespace NetFrame.Tool
{
    public class OutPutManager
    {
        /// <summary>
        /// 向控制台輸出消息，會自動帶有時間日期
        /// </summary>
        /// <param name="message">輸出訊息</param>
        /// <param name="record">是否記錄到本地記錄文件中</param>
        public static void WriteConsole(string message,bool record = false)
        {
            string msg = DateTime.Now.ToShortTimeString() + " - " + message + ".";
            Console.WriteLine(msg);

            if (record) Record(msg);
        }

        /// <summary>
        /// 將信息記錄到本地文件中
        /// </summary>
        /// <param name="message"></param>
        public static void Record(string message)
        {
            // TODO 將信息記錄到本地文件中
        }

        /// <summary>
        /// 紀錄用戶下線
        /// </summary>
        /// <param name="token"></param>
        public static void RecordOffline(UserToken token)
        {
            // TODO 將用戶下線記錄到本地文件中
        }
    }
}
