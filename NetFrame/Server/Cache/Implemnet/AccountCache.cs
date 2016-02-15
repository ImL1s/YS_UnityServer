/*
 * Author:ImL1s
 * Date:2016/02/08
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
    public class AccountCache : IAccountCache
    {
        private Dictionary<UserToken, string> onlineAccMap = new Dictionary<UserToken, string>();

        private Dictionary<string, AccountModel> accountMap = new Dictionary<string, AccountModel>();

        public void Add(string account, string password)
        {
            password = Convert.ToBase64String(Encoding.UTF8.GetBytes(password));
            AccountModel.AddToDatabase(account, password);
        }

        public int GetId(UserToken token)
        {
            throw new NotImplementedException();
        }

        public bool HasAccount(string account)
        {
            int result = AccountModel.QueryAccount(account);
            return (result > 0 && result != -1) ? true : false;
        }

        public bool IsOnline(string account)
        {
            return false;
        }

        public bool Match(string account, string password)
        {
            return AccountModel.QueryData(account, password) != null ? true : false;
        }

        public bool MatchAccount(string account)
        {
            return AccountModel.QueryAccount(account) > 0 ? true : false;
        }

        public bool MatchPassword(string account, string password)
        {
            return AccountModel.QueryFullAccountMatch(account, password) > 0 ? true : false;
        }

        public void Offline(UserToken token)
        {
            throw new NotImplementedException();
        }

        public void Online(UserToken token, string account)
        {
            throw new NotImplementedException();
        }
    }
}
