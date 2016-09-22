/*
 * Author:ImL1s
 *
 * Date:2016/03/01
 *
 * description:業務邏輯層介面.
 *
 */

using NetFrame;

namespace Server.Bll
{
    /// <summary>
    /// 業務邏輯層介面.
    /// </summary>
    public interface IBll
    {
        void ClientClose(UserToken token, string error);
    }
}
