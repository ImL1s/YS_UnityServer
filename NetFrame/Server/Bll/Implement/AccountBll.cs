/*
 * Author:ImL1s
 *
 * Date:2016/02/08
 *
 * Email:ImL1s@outlook.com
 *
 * description:帳號Bll層.
 *
 */

using System;
using NetFrame;
using Server.Cache;
using Protocols.Dto;
using NetFrame.Tool;
using System.Text;

namespace Server.Bll
{
    public class AccountBll : IAccountBll
    {
        protected IAccountCache accountCache = CacheFactory.accountCache;

        /// <summary>
        /// 玩家下線.
        /// </summary>
        /// <param name="token"></param>
        public void Close(UserToken token)
        {
            throw new NotImplementedException();
        }

        public CreateResult Create(UserToken token, string account, string password)
        {
            if (accountCache.HasAccount(account)) return CreateResult.AlreadyExist;

            try
            {
                accountCache.Add(account, password);
                return CreateResult.Succed;
            }
            catch (Exception error)
            {
                OutPutManager.WriteConsole("[註冊帳號失敗]IP:" + token.Connecting.RemoteEndPoint + " 錯誤:" + error.ToString(), true);
                return CreateResult.Error;
            }
        }

        /// <summary>
        /// 使用連接對象得到Account的主鍵ID.
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public int GetAccountID(UserToken token)
        {
            return accountCache.GetAccountId(token);
        }

        public LoginResult Login(UserToken token, string account, string password)
        {
            try
            {
                string base64Pwd = Convert.ToBase64String(Encoding.UTF8.GetBytes(password));

                if (accountCache.Match(account, base64Pwd))
                {
                    accountCache.Online(token, account);
                    return LoginResult.Succed;
                }
                if (accountCache.MatchAccount(account)) { return LoginResult.IncorrectPassword; }
                else { return LoginResult.IncorrectAccount; }
            }
            catch (Exception e)
            {
                OutPutManager.WriteConsole("登入模組發生錯誤!! Error: " + e.ToString());
                return LoginResult.error;
            }
        }
    }    
}
