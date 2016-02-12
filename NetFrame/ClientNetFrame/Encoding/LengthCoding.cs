using System;
using System.Collections.Generic;
using System.IO;

namespace ClientNetFrame.Encoding
{
    public class LengthCoding
    {
        /// <summary>
        /// 編碼(加密)要傳輸的資料
        /// </summary>
        /// <param name="buffer">要傳輸的資料</param>
        /// <returns></returns>
        public static byte[] Encode(byte[] buffer)
        {
            MemoryStream mStream = new MemoryStream();
            BinaryWriter bWriter = new BinaryWriter(mStream);

            bWriter.Write(buffer.Length);
            bWriter.Write(buffer);

            byte[] result = new byte[mStream.Length];
            Buffer.BlockCopy(mStream.GetBuffer(), 0, result, 0, (int)mStream.Length);

            bWriter.Close();
            mStream.Close();

            return result;
        }

        /// <summary>
        /// 解碼(解密)接收的資料
        /// </summary>
        /// <param name="cache">要解碼的位元組資料</param>
        /// <returns></returns>
        public static byte[] Decode(ref List<byte> cache)
        {
            if (cache.Count < 4) return null;
            MemoryStream mStream = new MemoryStream(cache.ToArray());
            BinaryReader bReader = new BinaryReader(mStream);
            int Length = bReader.ReadInt32();

            // 如果頭信息長度大於資料長度，代表沒有傳送資料or資料不完整
            if (Length > mStream.Length - mStream.Position + 1)
            {
                return null;
            }

            byte[] result = bReader.ReadBytes(Length);
            cache.Clear();
            cache.AddRange(bReader.ReadBytes((int)(mStream.Length - mStream.Position)));

            bReader.Close();
            mStream.Close();
                
            return result;
        }
    }
}
