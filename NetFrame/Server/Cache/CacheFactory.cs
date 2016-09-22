/*
 * Author:ImL1s
 *
 * Date:2016/02/08
 *
 * description:緩存層簡單工廠.
 *
 */

using Server.Cache.Implemnet;
using Server.Cache.Interface;

namespace Server.Cache
{
    /// <summary>
    /// 緩存層工廠.
    /// </summary>
    public class CacheFactory
    {
        public static readonly IAccountCache accountCache;

        /// <summary>
        /// 玩家角色緩存層.
        /// </summary>
        public static readonly IPlayerRoleCache playerRoleCache;

        static CacheFactory()
        {
            accountCache = new AccountCache();
            playerRoleCache = new PlayerRoleCache();
        }
    }
}
