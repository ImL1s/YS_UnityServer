namespace Protocols
{
    public class Protocol
    {
        /// <summary>
        /// 登陸模塊
        /// </summary>
        public const byte TYPE_LOGIN = 0;

        public enum Type : byte
        {
            /// <summary>
            /// 登入與註冊.
            /// </summary>
            Login = 0,
            /// <summary>
            /// 選擇角色.
            /// </summary>
            SelectRole = 1
        }

        public enum Area : int
        {
            None = -1
        }

        public const int LOGIN_CREQ = 0;

        public enum Command : int
        {
            /// <summary>
            /// 登入請求
            /// </summary>
            LoginRequest = 0,
            /// <summary>
            /// 登入回應
            /// </summary>
            LoginResponse = 1,
            /// <summary>
            /// 註冊請求
            /// </summary>
            RegisterRequest = 2,
            /// <summary>
            /// 註冊回應
            /// </summary>
            RegisterResponse = 3,
            /// <summary>
            /// 選擇角色請求
            /// </summary>
            SelectRoleRequest = 4,
            /// <summary>
            /// 選擇角色回應
            /// </summary>
            SelectRoleResponse = 5
        }
    }
}
