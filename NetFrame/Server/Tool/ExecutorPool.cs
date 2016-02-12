/*
 * Author:ImL1s
 *
 * Date:2016/02/08
 *
 * Email:ImL1s@outlook.com
 *
 * description:用於執行防止多執行緒引發的問題的方法
 *
 */

using System.Threading;

namespace Server.Tool
{
    /// <summary>
    /// 單執行緒執行委派
    /// </summary>
    public delegate void ExecutorEventHandler();

    /// <summary>
    /// 單執行緒執行類
    /// </summary>
    public class ExecutorPool
    {
        public static ExecutorPool Instance
        {
            get
            {
                if (instance == null) instance = new ExecutorPool();
                return instance;
            }

            set
            {
                instance = value;
            }
        }

        private static ExecutorPool instance;

        Mutex mutex = new Mutex();

        /// <summary>
        /// 執行
        /// </summary>
        /// <param name="handler"></param>
        public void Execute(ExecutorEventHandler handler)
        {
            lock (this)
            {
                mutex.WaitOne();
                handler.Invoke();
                mutex.ReleaseMutex();
            }
        }
    }
}
