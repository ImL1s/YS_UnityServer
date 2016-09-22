/*
 * Author:ImL1s
 *
 * Date:2016/03/01
 *
 * description:玩家角色緩存層.
 *
 */

using System;
using NNProtocols.Dto;
using Server.Cache.Interface;
using System.Collections.Generic;
using NetFrame;
using Server.Dal.Model;
using Protocols.Dto;

namespace Server.Cache.Implemnet
{
    /// <summary>
    /// 玩家擁有的角色.(緩存層)
    /// </summary>
    public class PlayerRoleCache : IPlayerRoleCache
    {
        /// <summary>
        /// 目前在線上的角色(ID)&連接對象.
        /// </summary>
        private Dictionary<int, UserToken> playerRoleOnline = new Dictionary<int, UserToken>();

        /// <summary>
        /// 連接對象(Key)與線上角色的綁定.
        /// </summary>
        private Dictionary<UserToken, int> onlinePlayerRole = new Dictionary<UserToken, int>();

        /// <summary>
        /// 角色ID與角色的映射緩存.
        /// </summary>
        private Dictionary<int, PlayerRoleModel> playerRoleMap = new Dictionary<int, PlayerRoleModel>();

        /// <summary>
        /// 加入玩家角色.
        /// </summary>
        /// <param name="accountID"></param>
        /// <param name="roleDTO"></param>
        /// <returns></returns>
        public SelectRoleResult Add(int accountID, RoleDTO roleDTO)
        {
            if (PlayerRoleModel.IsExist(roleDTO.Name.Trim())) return SelectRoleResult.AlreadyExist;

            bool succ = PlayerRoleModel.AddToDatabase(accountID, roleDTO.Profession, roleDTO.LV, roleDTO.Name);
            if (succ)
            {
                PlayerRoleModel model = PlayerRoleModel.GetByName(roleDTO.Name);
                if (!playerRoleMap.ContainsKey(model.Id))
                {
                    InitRoleCache(model.Id);
                    playerRoleMap.Add(model.Id, model);
                    return SelectRoleResult.Succed;
                }
            }

            return SelectRoleResult.CreateFailed;
        }


        /// <summary>
        /// 加入玩家角色.
        /// </summary>
        /// <param name="accountID"></param>
        /// <param name="roleDTO"></param>
        /// <returns></returns>
        public SelectRoleResult Add(int accountID,RoleDTO roleDTO,out PlayerRoleModel createdModel)
        {

            bool succ = PlayerRoleModel.AddToDatabase(accountID, roleDTO.Profession, roleDTO.LV, roleDTO.Name);
            if (succ)
            {
                PlayerRoleModel model = PlayerRoleModel.GetByName(roleDTO.Name);
                if (!playerRoleMap.ContainsKey(model.Id))
                {
                    InitRoleCache(model.Id);
                    playerRoleMap.Add(model.Id, model);
                    createdModel = model;

                    return SelectRoleResult.Succed;
                }
            }
            createdModel = null;
            return SelectRoleResult.CreateFailed;
        }

        /// <summary>
        /// 角色上線.
        /// </summary>
        /// <param name="token"></param>
        /// <param name="roleID"></param>
        public void Online(UserToken token, int roleID)
        {
            // 角色重複登入，將之前登入的連接對象踢下線.
            if (playerRoleOnline.ContainsKey(roleID))
            {
                UserToken kickToken = playerRoleOnline[roleID];
                playerRoleOnline.Remove(roleID);
                if(onlinePlayerRole.ContainsKey(kickToken)) onlinePlayerRole.Remove(kickToken);
                kickToken.ClientClose(kickToken, "角色重複上線!!");
            }

            playerRoleOnline.Add(roleID, token);
            onlinePlayerRole.Add(token, roleID);
        }

        /// <summary>
        /// 使用連接對象取得角色ID.
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public int GetOnlineRoleID(UserToken token)
        {
            if (onlinePlayerRole.ContainsKey(token)) return  onlinePlayerRole[token];

            return -1;
        }

        private void InitRoleCache(int roleID)
        {

        }

        // 客戶端下線
        public void ClientClose(UserToken token, string error)
        {
            if (onlinePlayerRole.ContainsKey(token))
            {
                int offlineRoleID = onlinePlayerRole[token];
                onlinePlayerRole.Remove(token);
                playerRoleOnline.Remove(offlineRoleID);
            }
        }
    }
}
