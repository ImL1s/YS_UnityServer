/*
 * Author:ImL1s
 *
 * Date:2016/03/01
 *
 * description:緩存層介面.
 *
 */

using NetFrame;

namespace Server.Cache
{
    /// <summary>
    /// 緩存層介面.
    /// </summary>
    public interface ICache
    {
        /// <summary>
        /// 客戶端下線.
        /// </summary>
        /// <param name="token"></param>
        /// <param name="error"></param>
        void ClientClose(UserToken token, string error);
    }
}
