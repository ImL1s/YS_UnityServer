namespace NetFrame
{
    /// <summary>
    /// 協議
    /// </summary>
    public class SocketModelG
    {

        /// <summary>
        /// 一級協議，用於區分所屬模塊
        /// </summary>
        public byte Type { get; set; }
        /// <summary>
        /// 二級協議，用於 區分模塊下所屬子模塊
        /// </summary>
        public int Area { get; set; }
        /// <summary>
        /// 三級協議，用於 區分當前功能處理邏輯
        /// </summary>
        public int Command { get; set; }
        /// <summary>
        /// 消息體 當前需要處理的數據
        /// </summary>
        public object Message { get; set; }

        public SocketModelG()
        {

        }

        public SocketModelG(byte t, int a, int c, object o)
        {
            this.Type = t;
            this.Area = a;
            this.Command = c;
            this.Message = o;
        }

        /// <summary>
        /// 取得message的泛型方法
        /// </summary>
        /// <typeparam name="T">根據不同消息類型，返回該消息的DOT</typeparam>
        /// <returns></returns>
        public T GetMessage<T>()
        {
            return (T)Message;
        }

    }
}
