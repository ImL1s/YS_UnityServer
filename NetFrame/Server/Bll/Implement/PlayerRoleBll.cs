/*
 * Author:ImL1s
 *
 * Date:2016/03/01
 *
 * description:玩家擁有角色業務邏輯層.
 *
 */

using System;
using NNProtocols.Dto;
using Protocols.Dto;
using Server.Bll.Interface;
using NetFrame;
using Server.Cache.Interface;
using Server.Cache;

namespace Server.Bll.Implement
{
    /// <summary>
    /// 玩家擁有角色業務邏輯層.
    /// </summary>
    public class PlayerRoleBll : IPlayerRoleBll
    {
        private IPlayerRoleCache playerRoleCache = CacheFactory.playerRoleCache;

        public void ClientClose(UserToken token, string error)
        {
            playerRoleCache.ClientClose(token, error);
        }

        /// <summary>
        /// 玩家創建角色.
        /// </summary>
        /// <param name="token"></param>
        /// <param name="roleDTO"></param>
        /// <returns></returns>
        public SelectRoleResult CreateRole(UserToken token, RoleDTO roleDTO)
        {
            int accountID = BllFactory.accountBll.GetAccountID(token);

            if(accountID != -1)
            {
                roleDTO.LV = 1;
                Dal.Model.PlayerRoleModel playerRoleModel;

                SelectRoleResult result = playerRoleCache.Add(accountID, roleDTO, out playerRoleModel);

                if (result == SelectRoleResult.Succed)
                {
                    playerRoleCache.Online(token, playerRoleModel.Id);
                }

                return result;

                //if (playerRoleCache.Add(accountID, roleDTO,out playerRoleModel))
                //{
                //    playerRoleCache.Online(token, playerRoleModel.Id);
                //    return SelectRoleResult.Succed;
                //}
                //else return SelectRoleResult.CreateFailed;
            }
            else
            {
                return SelectRoleResult.AccountNotOnline;
            }
            
        }
    }
}
