/*
 * Author:ImL1s
 * Date:2016/02/08
 * description:
 *
 */

namespace Server.Cache
{
    public class CacheFactory
    {
        public static readonly IAccountCache accountCache;

        static CacheFactory()
        {
            accountCache = new AccountCache();
        }
    }
}
