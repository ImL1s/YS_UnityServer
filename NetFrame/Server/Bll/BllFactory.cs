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

namespace Server.Bll
{
    public class BllFactory
    {
        public readonly static IAccountBll accountBll;

        static BllFactory()
        {
            accountBll = new AccountBll();
        }
    }
}
