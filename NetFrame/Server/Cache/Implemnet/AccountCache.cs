/*
 * Author:ImL1s
 *
 * Date:2016/02/08
 *
 * description:緩存層，負責緩存、調用dal層方法.
 *
 */

using System;
using NetFrame;
using Server.Dal.Model;
using System.Text;
using System.Collections.Generic;

namespace Server.Cache
{
    /// <summary>
    /// 帳號緩存層.
    /// </summary>
    public class AccountCache : IAccountCache
    {
        private Dictionary<UserToken, string> onlineAccMap = new Dictionary<UserToken, string>();

        // 帳號與帳號模型緩存.
        private Dictionary<string, AccountModel> accountMap = new Dictionary<string, AccountModel>();

        /// <summary>
        /// 加入新的帳號資料並緩存.
        /// </summary>
        /// <param name="account"></param>
        /// <param name="password"></param>
        public void Add(string account, string password)
        {
            password = Convert.ToBase64String(Encoding.UTF8.GetBytes(password));
            AccountModel.AddToDatabase(account, password);
            accountMap.Add(account, AccountModel.CreateByAccount(account, password));
        }

        /// <summary>
        /// 使用連接對象得到Account的主鍵ID.
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public int GetAccountId(UserToken token)
        {
            if (!onlineAccMap.ContainsKey(token)) return -1;

            string account = onlineAccMap[token];
            InitAccountCache(account);
            return accountMap[account].Id;
        }

        /// <summary>
        /// 檢查是否有該帳號.
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        public bool HasAccount(string account)
        {
            int result = AccountModel.QueryAccount(account);
            return (result > 0 && result != -1) ? true : false;
        }

        /// <summary>
        /// 該帳號是否上線.
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        public bool IsOnline(string account)
        {
            return onlineAccMap.ContainsValue(account);
        }

        /// <summary>
        /// 檢查帳號密碼.
        /// </summary>
        /// <param name="account"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public bool Match(string account, string password)
        {
            return AccountModel.CreateByAccount(account, password) != null ? true : false;
        }

        /// <summary>
        /// 檢查帳號是否存在.
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        public bool MatchAccount(string account)
        {
            return AccountModel.QueryAccount(account) > 0 ? true : false;
        }

        /// <summary>
        /// 檢查帳號與密碼.
        /// </summary>
        /// <param name="account"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public bool MatchPassword(string account, string password)
        {
            return AccountModel.QueryFullAccountMatch(account, password) > 0 ? true : false;
        }

        /// <summary>
        /// 玩家離線.
        /// </summary>
        /// <param name="token"></param>
        public void Offline(UserToken token)
        {
            if (onlineAccMap.ContainsKey(token)) onlineAccMap.Remove(token);
        }

        /// <summary>
        /// 玩家上線.
        /// </summary>
        /// <param name="token"></param>
        /// <param name="account"></param>
        public void Online(UserToken token, string account)
        {
            if (!onlineAccMap.ContainsKey(token)) onlineAccMap.Add(token, account);
        }

        public void ClientClose(UserToken token, string error)
        {
            if (onlineAccMap.ContainsKey(token)) onlineAccMap.Remove(token);
        }

        /// <summary>
        /// 初始化緩存的資料.
        /// </summary>
        /// <param name="account"></param>
        private void InitAccountCache(string account)
        {
            if (accountMap.ContainsKey(account)) return;
            AccountModel accountModel = AccountModel.CreateByAccount(account);
            accountMap.Add(accountModel.Account, accountModel);
        }
    }
}
