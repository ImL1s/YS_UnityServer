/*
 * Author:ImL1s
 *
 * Date:2016/02/08
 *
 * Email:ImL1s@outlook.com
 *
 * description:
 */

using NetFrame;
using Protocols.Dto;

namespace Server.Bll
{
    public interface IAccountBll
    {
        /// <summary>
        /// 註冊新帳號
        /// </summary>
        /// <param name="token"></param>
        /// <param name="account"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        CreateResult Create(UserToken token, string account, string password);

        /// <summary>
        /// 登入
        /// </summary>
        /// <param name="token"></param>
        /// <param name="account"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        LoginResult Login(UserToken token, string account, string password);

        /// <summary>
        /// 離線
        /// </summary>
        /// <param name="token"></param>
        void Close(UserToken token);

        int Get(UserToken token);
    }
}
