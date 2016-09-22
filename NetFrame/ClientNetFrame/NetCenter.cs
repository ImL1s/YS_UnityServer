/*
 * Author:ImL1s
 *
 * Date:2016/02/08
 *
 * Email:ImL1s@outlook.com
 *
 * description:
 *
 */

using Protocols;
using System.Collections.Generic;
using System.Net.Sockets;
using System;
using ClientNetFrame.Encoding;

namespace ClientNetFrame
{
    public class NetCenter : AbsSingleton<NetCenter>
    {
        private Socket connecting = null;
        private List<byte> cache = null;
        private List<SocketModel> messages = null;

        private string ip = "127.0.0.1";
        private int port = 9527;
        private byte[] readBuffer;
        private bool isReading;

        /// <summary>
        /// 收到資料的緩存
        /// </summary>
        public List<byte> Cache
        {
            get
            {
                return cache;
            }

            protected set
            {
                cache = value;
            }
        }

        public List<SocketModel> Messages
        {
            get
            {
                return messages;
            }

            protected set
            {
                messages = value;
            }
        }

        /// <summary>
        /// 初始化.
        /// </summary>
        public void Init()
        {
            connecting = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            Cache = new List<byte>();
            Messages = new List<SocketModel>();
            readBuffer = new byte[1024 * 1024 * 5];
            isReading = false;
        }

        /// <summary>
        /// 連接.
        /// </summary>
        /// <param name="IP"></param>
        /// <param name="port"></param>
        public void Connect(string IP, int port)
        {
            this.ip = IP;
            this.port = port;

            connecting.Connect(IP, port);
            connecting.BeginReceive(readBuffer, 0, readBuffer.Length, SocketFlags.None, OnReceivedHandler, readBuffer);
        }

        /// <summary>
        /// 向伺服器發送資訊
        /// </summary>
        /// <param name="type"></param>
        /// <param name="area"></param>
        /// <param name="command"></param>
        /// <param name="message"></param>
        public void Send(byte type,int area,int command,object message)
        {
            Send(new SocketModel(type, area, command, message));
        }

        /// <summary>
        /// 向伺服器發送資訊
        /// </summary>
        /// <param name="type"></param>
        /// <param name="area"></param>
        /// <param name="command"></param>
        /// <param name="message"></param>
        public void Send(SocketModel socketModel)
        {
            byte[] data = MessageCoding.Encode(socketModel);
            data = LengthCoding.Encode(data);

            try { connecting.Send(data); }
            catch(Exception error){ throw new Exception("網路錯誤，請重新登入!!" + error); }
        }

        /// <summary>
        /// 接收到資料的處理方法.
        /// </summary>
        /// <param name="result"></param>
        private void OnReceivedHandler(IAsyncResult result)
        {
            try
            {
                int length = connecting.EndReceive(result);
                byte[] message = new byte[length];
                Buffer.BlockCopy(readBuffer, 0, message, 0, length);
                Cache.AddRange(message);

                if (!isReading)
                {
                    isReading = true;
                    ReadData();
                }

                connecting.BeginReceive(readBuffer, 0, readBuffer.Length, SocketFlags.None, OnReceivedHandler, readBuffer); 
            }
            catch(Exception error)
            {
                connecting.Close();
                throw new Exception("連接失敗!! 錯誤:" + error.ToString());
            }
        }

        /// <summary>
        /// 開始遞歸讀取資料.
        /// </summary>
        private void ReadData()
        {
            byte[] result = LengthCoding.Decode(ref cache);

            if(result == null)
            {
                isReading = false;
                return;
            }

            SocketModel message = MessageCoding.Decode(result) as SocketModel;

            if(message == null)
            {
                isReading = false;
                return;
            }

            Messages.Add(message);

            ReadData();
        }
    }
}
