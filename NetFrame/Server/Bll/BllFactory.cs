/*
 * Author:ImL1s
 *
 * Date:2016/02/08
 *
 * Email:ImL1s@outlook.com
 *
 * description:Bll層簡單工廠.
 *
 */

using Server.Bll.Implement;
using Server.Bll.Interface;

namespace Server.Bll
{
    public class BllFactory
    {
        public readonly static IAccountBll accountBll;

        public readonly static IPlayerRoleBll playerRoleBll;

        static BllFactory()
        {
            accountBll = new AccountBll();
            playerRoleBll = new PlayerRoleBll();
        }
    }
}
