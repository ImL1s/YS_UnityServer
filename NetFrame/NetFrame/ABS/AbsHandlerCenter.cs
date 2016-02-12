namespace NetFrame.ABS
{
    /// <summary>
    /// 此類為抽象類，會由外部傳入的類繼承，之後將傳輸層的資料傳入.
    /// </summary>
    public abstract class AbsHandlerCenter
    {
        /// <summary>
        /// 通知客戶端連接
        /// </summary>
        /// <param name="token">客戶端連接對象</param>
        public abstract void ClientConnect(UserToken token);

        /// <summary>
        /// 收到客戶端消息
        /// </summary>
        /// <param name="token">客戶端連接對象</param>
        /// <param name="message">客戶端發送的消息</param>
        public abstract void MessageRecive(UserToken token, object message);

        /// <summary>
        /// 客戶端斷開連接
        /// </summary>
        /// <param name="token">客戶端連接對象</param>
        /// <param name="error">客戶端斷開的錯誤訊息</param>
        public abstract void ClientClose(UserToken token, string error);
    }
}
