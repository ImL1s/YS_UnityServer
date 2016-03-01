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

    #region ReturnResult 返回結果
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
    /// 遊戲登入結果.
    /// </summary>
    public enum LoginResult : sbyte
    {
        /// <summary>
        /// 錯誤.
        /// </summary>
        error = 0,
        /// <summary>
        /// 密碼錯誤.
        /// </summary>
        IncorrectPassword = -1,
        /// <summary>
        /// 帳號錯誤.
        /// </summary>
        IncorrectAccount = -11,
        /// <summary>
        /// 登入成功.
        /// </summary>
        Succed = 127
    }

    #endregion

    #region other



    #endregion
}
