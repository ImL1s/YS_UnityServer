using System.Collections.Generic;

namespace NetFrame.EventDelegate
{
    /// <summary>
    /// 長度編碼器，用於保證數據的完整性.
    /// </summary>
    /// <param name="valus"></param>
    /// <returns></returns>
    public delegate byte[] LengthEncode(byte[] valus);

    /// <summary>
    /// 長度編碼器，用於確認數據的完整性.
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public delegate byte[] LengthDecode(ref List<byte> value);

    /// <summary>
    /// 資料編碼器，將資料轉成Byte型數據.
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public delegate byte[] Encode(object value);

    /// <summary>
    /// 資料解碼器，將Byte型資料轉回原本的物件.
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public delegate object Decode(byte[] value);

    /// <summary>
    /// UserToken離線委派.
    /// </summary>
    /// <param name="token"></param>
    /// <param name="message"></param>
    public delegate void TokenOfflineHandler(UserToken token, string message);

    public class EventDelegate
    {
        
    }
}
