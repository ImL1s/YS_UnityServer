﻿/*
 * Author:ImL1s
 * Date:2016/02/08
 * description:
 *
 */

using NetFrame;

namespace Server.Cache
{
    /// <summary>
    /// 帳號緩存層.
    /// </summary>
    public interface IAccountCache : ICache
    {
        /// <summary>
        /// 帳號是否存在
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        bool HasAccount(string account);

        /// <summary>
        /// 帳密是否匹配
        /// </summary>
        /// <param name="account"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        bool Match(string account, string password);

        /// <summary>
        /// 帳號是否匹配
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        bool MatchAccount(string account);

        /// <summary>
        /// 密碼是否匹配.
        /// </summary>
        /// <param name="account"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        bool MatchPassword(string account, string password);

        /// <summary>
        /// 用戶是否在線
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        bool IsOnline(string account);

        /// <summary>
        /// 使用連接對象得到Account的主鍵ID.
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        int GetAccountId(UserToken token);

        /// <summary>
        /// 玩家上線.
        /// </summary>
        /// <param name="token"></param>
        /// <param name="account"></param>
        void Online(UserToken token, string account);

        void Offline(UserToken token);

        void Add(string account, string password);
    }
}
