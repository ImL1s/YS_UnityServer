/*
 * Author:ImL1s
 *
 * Date:2016/02/08
 *
 * description:緩存層工廠.
 *
 */

namespace Server.Cache
{
    /// <summary>
    /// 緩存層工廠.
    /// </summary>
    public class CacheFactory
    {
        public static readonly IAccountCache accountCache;

        static CacheFactory()
        {
            accountCache = new AccountCache();
        }
    }
}
