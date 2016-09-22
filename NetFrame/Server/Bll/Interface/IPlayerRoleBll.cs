using Protocols.Dto;
using NNProtocols.Dto;
using NetFrame;

namespace Server.Bll.Interface
{
    public interface IPlayerRoleBll : IBll
    {
        /// <summary>
        /// 創建角色處理方法.
        /// </summary>
        /// <param name="token"></param>
        /// <param name="account"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        SelectRoleResult CreateRole(UserToken token, RoleDTO account);
        
    }
}
