/*
 * Author:ImL1s
 *
 * Date:2016/02/08
 *
 * Email:ImL1s@outlook.com
 *
 * description:
 *
 */

using System;
using NetFrame;
using Server.Cache;
using Protocols.Dto;
using NetFrame.Tool;

namespace Server.Bll
{
    public class AccountBll : IAccountBll
    {
        protected IAccountCache accountCache = CacheFactory.accountCache;

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

        public int Get(UserToken token)
        {
            throw new NotImplementedException();
        }

        public LoginResult Login(UserToken token, string account, string password)
        {
            if (accountCache.Match(account, password)) { return LoginResult.Succed; }
            if (accountCache.MatchAccount(account)) { return LoginResult.IncorrectAccount; }

            return LoginResult.error;
        }
    }    
}
