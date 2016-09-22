using NNProtocols.Dto;
using NetFrame;
using Server.Dal.Model;
using Protocols.Dto;

namespace Server.Cache.Interface
{
    public interface IPlayerRoleCache : ICache
    {
        /// <summary>
        /// 新增玩家角色.
        /// </summary>
        /// <param name="roleDTO"></param>
        /// <returns>新增是否成功.</returns>
        SelectRoleResult Add(int accountID,RoleDTO roleDTO);


        ///// <summary>
        ///// 新增玩家角色.
        ///// </summary>
        ///// <param name="roleDTO"></param>
        ///// <param name="createdModel">若創建成功返回創建好的角色訊息，若失敗返回null.</param>
        ///// <returns>新增結果.</returns>
        SelectRoleResult Add(int accountID, RoleDTO roleDTO, out PlayerRoleModel createdModel);

        ///// <summary>
        ///// 新增玩家角色.
        ///// </summary>
        ///// <param name="roleDTO"></param>
        ///// <param name="createdModel">若創建成功返回創建好的角色訊息，若失敗返回null.</param>
        ///// <returns>新增是否成功.</returns>
        //bool Add(int accountID, RoleDTO roleDTO, out PlayerRoleModel createdModel);


        /// <summary>
        /// 角色上線.
        /// </summary>
        /// <param name="token"></param>
        /// <param name="roleID"></param>
        void Online(UserToken token, int roleID);


        /// <summary>
        /// 使用連接對象取得上線中角色.
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        int GetOnlineRoleID(UserToken token);
    }
}
