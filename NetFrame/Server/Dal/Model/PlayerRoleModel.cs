/*
 * Author:ImL1s
 *
 * Date:2016/03/01
 *
 * Email:ImL1s@outlook.com
 *
 * description:數據庫的玩家角色資料模型.
 *
 */

using Server;
using Dal;
using System.Data.SqlClient;
using NetFrame.Tool;

namespace Server.Dal.Model
{
    /// <summary>
    /// 玩家擁有角色的資料庫模型.
    /// </summary>
    public class PlayerRoleModel : IModel
    {
        private int id;
        private int accountId;
        private RoleProfessionModel profession;
        private byte lv;
        private string name;

        /// <summary>
        /// ID(主鍵).
        /// </summary>
        public int Id
        {
            get
            {
                return id;
            }

            set
            {
                id = value;
            }
        }

        /// <summary>
        /// 帳號ID(外來鍵).
        /// </summary>
        public int AccountId
        {
            get
            {
                return accountId;
            }

            set
            {
                accountId = value;
            }
        }

        /// <summary>
        /// 角色職業(外來鍵).
        /// </summary>
        internal RoleProfessionModel Profession
        {
            get
            {
                return profession;
            }

            set
            {
                profession = value;
            }
        }

        /// <summary>
        /// 角色等級.
        /// </summary>
        public byte Lv
        {
            get
            {
                return lv;
            }

            set
            {
                lv = value;
            }
        }

        /// <summary>
        /// 角色名稱.
        /// </summary>
        public string Name
        {
            get
            {
                return name;
            }

            set
            {
                name = value;
            }
        }

        #region static method 靜態方法

        public static bool AddToDatabase(int accountID, int professionID, int lv, string name)
        {
            string cmd = "insert into PlayerRole(accountId,profession,lv,name) values(@accountId,@profession,@lv,@name);";

            SqlParameter[] paras =
            {
                new SqlParameter("@accountId",accountID),
                new SqlParameter("@profession",professionID),
                new SqlParameter("@lv",lv),
                new SqlParameter("@name",name)
            };

            int count = NSQLHelper.ExecuteNonQuery(cmd, paras);

            if (count > 0) return true;
            else return false;
        }

        // TODO 改為傳回多個角色.
        /// <summary>
        /// 使用帳號ID取得玩家角色.
        /// </summary>
        /// <param name="accountID"></param>
        /// <returns></returns>
        public static PlayerRoleModel GetByAccountID(int accountID)
        {
            string cmd = "select * from PlayerRole where accountId = @id;";
            SqlParameter[] paras =
            {
            new SqlParameter("@id",accountID)
        };

            using (SqlDataReader reader = NSQLHelper.ExecuteReader(cmd, paras))
            {
                if (reader.HasRows)
                {
                    reader.Read();
                    PlayerRoleModel role = new PlayerRoleModel()
                    {
                        id = reader.GetInt32(0),
                        accountId = reader.GetInt32(1),
                        profession = RoleProfessionModel.GetByID(reader.GetInt32(2))
                    };

                    return role;
                }
                else
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// 使用角色名稱取得模型.
        /// </summary>
        /// <param name="roleName"></param>
        /// <returns></returns>
        public static PlayerRoleModel GetByName(string roleName)
        {
            string cmd = "select * from PlayerRole where name = @name;";
            SqlParameter[] paras =
            {
                new SqlParameter("@name",roleName)
            };

            using (SqlDataReader reader = NSQLHelper.ExecuteReader(cmd, paras))
            {
                if (reader.HasRows)
                {
                    reader.Read();
                    PlayerRoleModel role = new PlayerRoleModel()
                    {
                        id = reader.GetInt32(0),
                        accountId = reader.GetInt32(1),
                        profession = RoleProfessionModel.GetByID(reader.GetInt32(2)),
                        lv = reader.GetByte(3),
                        name = reader.GetString(4)
                    };

                    return role;
                }
                else
                {
                    return null;
                }
            }

        }

        /// <summary>
        /// 查詢角色是否存在.
        /// </summary>
        /// <param name="roleName"></param>
        /// <returns></returns>
        public static bool IsExist(string roleName)
        {
            string cmd = "select COUNT(*) from PlayerRole where name = @name;";
            SqlParameter para = new SqlParameter("@name", roleName);

            int count = (int)NSQLHelper.ExecuteScaler(cmd, para);

            return count > 0 ? true : false;
        }

        public static PlayerRoleModel GetByAccount(string account)
        {
            throw new System.Exception("未實現GetByAccout!");
        }

        #endregion


        public bool AddToDatabase()
        {
            string cmd = "insert into PlayerRole(accountId,profession,lv,name) values(@accountId,@profession,@lv,@name);";

            SqlParameter[] paras =
            {
            //new SqlParameter("@id",this.Id),
            new SqlParameter("@accountId",this.AccountId),
            new SqlParameter("@profession",(int)this.Profession.Id),
            new SqlParameter("@lv",this.Lv),
            new SqlParameter("@name",this.Name)
        };

            int count = NSQLHelper.ExecuteNonQuery(cmd, paras);

            if (count > 0) return true;
            else return false;
        }

    }
}

