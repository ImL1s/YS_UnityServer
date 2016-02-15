using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace ClientNetFrame.Encoding
{
    /// <summary>
    /// 本類負責序列/反序列化物體
    /// </summary>
    public class SerializeUtil
    {
        /// <summary>
        /// 序列化對象
        /// </summary>
        /// <param name="value">要序列化的對象</param>
        /// <returns></returns>
        public static byte[] Encode(object value)
        {
            MemoryStream ms = new MemoryStream();
            BinaryFormatter bf = new BinaryFormatter();

            bf.Serialize(ms, value);
            byte[] result = new byte[ms.Length];
            Buffer.BlockCopy(ms.GetBuffer(), 0, result, 0, (int)ms.Length);
            ms.Close();

            return result;
        }


        /// <summary>
        /// 反序列化對象
        /// </summary>
        /// <param name="value">要反序列化的對象</param>
        /// <returns></returns>
        public static object Decode(byte[] value)
        {
            MemoryStream ms = new MemoryStream(value);
            BinaryFormatter bf = new BinaryFormatter();

            object obj = bf.Deserialize(ms);
            ms.Close();

            return obj;
        }
    }
}
