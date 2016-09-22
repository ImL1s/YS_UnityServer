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

    /// <summary>
    /// 選擇角色結果.
    /// </summary>
    public enum SelectRoleResult
    {
        /// <summary>
        /// 成功.
        /// </summary>
        Succed,
        /// <summary>
        /// 錯誤.
        /// </summary>
        Error,
        /// <summary>
        /// 帳號尚未上線，不能進行新增角色動作
        /// </summary>
        AccountNotOnline,
        /// <summary>
        /// 創建角色失敗.
        /// </summary>
        CreateFailed,
        AlreadyExist
    }

    #endregion

    #region other



    #endregion
}
