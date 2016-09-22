using System;
using System.IO;

namespace ClientNetFrame.Encoding
{
    // 此類封裝了MemoryStream、BinaryWriter/Reader，讓讀寫byte更方便.
    public class BytePipe
    {
        MemoryStream ms = new MemoryStream();

        BinaryWriter bw;
        BinaryReader br;

        public void Close()
        {
            bw.Close();
            br.Close();
            ms.Close();
        }

        /// <summary>
        /// 默認構造
        /// </summary>
        public BytePipe()
        {
            bw = new BinaryWriter(ms);
            br = new BinaryReader(ms);
        }

        /// <summary>
        /// 傳入初始數據的構造函數
        /// </summary>
        /// <param name="buff"></param>
        public BytePipe(byte[] buff)
        {
            ms = new MemoryStream(buff);
            bw = new BinaryWriter(ms);
            br = new BinaryReader(ms);
        }

        /// <summary>
        /// 得到當前數據讀取到的下標位置
        /// </summary>
        public int Position
        {
            get { return (int)ms.Position; }
        }

        /// <summary>
        /// 得到當前數據長度
        /// </summary>
        public int Length
        {
            get { return (int)ms.Length; }
        }
        /// <summary>
        /// 當前是否還有數據可以讀取
        /// </summary>
        public bool Readnable
        {
            get { return ms.Length > ms.Position; }
        }

        public void write(int value)
        {
            bw.Write(value);
        }
        public void write(byte value)
        {
            bw.Write(value);
        }
        public void write(bool value)
        {
            bw.Write(value);
        }
        public void write(string value)
        {
            bw.Write(value);
        }
        public void write(byte[] value)
        {
            bw.Write(value);
        }

        public void write(double value)
        {
            bw.Write(value);
        }
        public void write(float value)
        {
            bw.Write(value);
        }
        public void write(long value)
        {
            bw.Write(value);
        }


        public void Read(out int value)
        {
            value = br.ReadInt32();
        }
        public void Read(out byte value)
        {
            value = br.ReadByte();
        }
        public void Read(out bool value)
        {
            value = br.ReadBoolean();
        }
        public void Read(out string value)
        {
            value = br.ReadString();
        }

        /// <summary>
        /// 從數據流讀取length長度的byte
        /// </summary>
        /// <param name="value"></param>
        /// <param name="length"></param>
        public void Read(out byte[] value, int length)
        {
            value = br.ReadBytes(length);
        }

        public void Read(out double value)
        {
            value = br.ReadDouble();
        }
        public void Read(out float value)
        {
            value = br.ReadSingle();
        }
        public void Read(out long value)
        {
            value = br.ReadInt64();
        }

        public void reposition()
        {
            ms.Position = 0;
        }

        /// <summary>
        /// 獲取所有寫入進來在暫存區的數據.
        /// </summary>
        /// <returns></returns>
        public byte[] GetBuff()
        {
            byte[] result = new byte[ms.Length];
            Buffer.BlockCopy(ms.GetBuffer(), 0, result, 0, (int)ms.Length);
            return result;
        }
    }
}

