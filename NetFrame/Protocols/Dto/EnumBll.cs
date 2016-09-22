/*
 * Author:ImL1s
 *
 * Date:2016/02/08
 *
 * Email:ImL1s@outlook.com
 *
 * description:
 */

namespace Protocols.Dto
{
    /// <summary>
    /// 註冊結果
    /// </summary>
    public enum CreateResult : sbyte
    {
        AlreadyExist = 110,
        Succed = 127,
        Error = -1
    }

    /// <summary>
    /// 登入結果
    /// </summary>
    public enum LoginResult : sbyte
    {
        error = 0,
        IncorrectPassword = -1,
        IncorrectAccount = -11,
        Succed = 127
        
    }
}
