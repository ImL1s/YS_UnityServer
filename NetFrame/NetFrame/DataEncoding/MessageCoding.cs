using Protocols;

namespace NetFrame.DataEncoding
{
    /// <summary>
    /// 此類用來解/編碼要傳輸的資料
    /// </summary>
    public class MessageCoding
    {
        /// <summary>
        /// 序列化對象、模型.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static byte[] Encode(object value)
        {
            BytePipe bp = new BytePipe();
            SocketModel model = value as SocketModel;

            // 將socketmodel的數據都寫入到記憶體
            bp.write(model.Type);
            bp.write(model.Area);
            bp.write(model.Command);
            
            if (model.Message != null) bp.write(SerializeUtil.Encode(model.Message));

            byte[] result = bp.GetBuff();
            bp.Close();

            return result;
        }

        /// <summary>
        /// 反序列化對象
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static object Decode(byte[] value)
        {
            BytePipe bp = new BytePipe(value);
            SocketModel socketModel = new SocketModel();
            byte type;
            int area;
            int command;

            bp.Read(out type);
            bp.Read(out area);
            bp.Read(out command);

            socketModel.Type = type;
            socketModel.Area = area;
            socketModel.Command = command;

            if (bp.Readnable)
            {
                byte[] message;
                bp.Read(out message, bp.Length - bp.Position);

                socketModel.Message = SerializeUtil.Decode(message);
            }

            bp.Close();

            return socketModel;
        }
    }
}
