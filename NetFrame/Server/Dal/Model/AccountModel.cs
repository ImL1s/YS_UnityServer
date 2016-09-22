/*
 * Author:ImL1s
 *
 * Date:2016/02/08
 *
 * Email:ImL1s@outlook.com
 *
 * description:數據庫的帳號模型
 *
 */

using Dal;
using NetFrame.Tool;
using System.Data.SqlClient;
///
namespace Server.Dal.Model
{
    /// <summary>
    /// 帳號Model
    /// </summary>
    public class AccountModel :IModel
    {

        #region field 字段

        private int id = -1;
        private string account;
        private string password;

        #endregion

        #region property 屬性

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

        public string Account
        {
            get
            {
                return account;
            }

            set
            {
                account = value;
            }
        }

        public string Password
        {
            get
            {
                return password;
            }

            set
            {
                password = value;
            }
        }

        #endregion

        #region method 方法

        #region static method

        /// <summary>
        /// 新增一個帳號.
        /// </summary>
        /// <returns>返回是否成功加入</returns>
        public static bool AddToDatabase(string account, string password)
        {
            string cmdStr = "insert into Account(account,password) values(@account,@password);";
            SqlParameter[] paras =
            {
                new SqlParameter("@account", account),
                new SqlParameter("@password", password)
            };

            int affectRow = NSQLHelper.ExecuteNonQuery(cmdStr, paras);
            return affectRow > 0 ? true : false;
        }

        /// <summary>
        /// 取得ID主鍵
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        public static int GetPrimaryKey(string account)
        {
            string cmdStr = "select id from Account where account = @account;";
            SqlParameter para = new SqlParameter("@account", account);

            int id = (int)NSQLHelper.ExecuteScaler(cmdStr, para);
            return id;
        }

        /// <summary>
        /// 藉由傳入的帳號名稱與密碼生成此Model實例
        /// </summary>
        /// <param name="account"></param>
        public static AccountModel CreateByAccount(string account,string password)
        {
            string cmdStr = "select * from Account where account = @account and password = @password;";
            SqlParameter para1 = new SqlParameter("@account", System.Data.SqlDbType.VarChar) { Value = account };
            SqlParameter para2 = new SqlParameter("@password", System.Data.SqlDbType.VarChar) { Value = password };

            using (SqlDataReader reader = NSQLHelper.ExecuteReader(cmdStr, para1, para2))
            {
                if (reader.HasRows)
                {
                    if (reader.Read())
                    {
                        AccountModel model = new AccountModel();
                        model.id = reader.GetInt32(0);
                        model.account = reader.GetString(1);
                        model.password = reader.GetString(2);

                        return model;
                    }
                }
            }

            return null;
        }

        /// <summary>
        /// 藉由傳入的帳號名稱生成此Model實例
        /// </summary>
        /// <param name="account"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public static AccountModel CreateByAccount(string account)
        {
            string cmdStr = "select * from Account where account = @account;";
            SqlParameter para1 = new SqlParameter("@account", System.Data.SqlDbType.VarChar) { Value = account };

            using (SqlDataReader reader = NSQLHelper.ExecuteReader(cmdStr, para1))
            {
                if (reader.HasRows)
                {
                    if (reader.Read())
                    {
                        AccountModel model = new AccountModel();
                        model.id = reader.GetInt32(0);
                        model.account = reader.GetString(1);
                        model.password = reader.GetString(2);

                        return model;
                    }
                }
            }

            return null;
        }


        /// <summary>
        /// 查詢帳號.
        /// </summary>
        /// <param name="account"></param>
        /// <returns>查詢帳號到的個數</returns>
        public static int QueryAccount(string account)
        {
            string cmdStr = "select count(*) from Account where account = @account;";
            SqlParameter para1 = new SqlParameter("@account", System.Data.SqlDbType.VarChar) { Value = account };

            int count = (int)NSQLHelper.ExecuteScaler(cmdStr, para1);
            return count;
        }

        /// <summary>
        /// 查詢帳號和密碼.
        /// </summary>
        /// <param name="account"></param>
        /// <returns>查詢帳號到的個數</returns>
        public static int QueryFullAccountMatch(string account,string password)
        {
            string cmdStr = "select * from Account where account = @account and password = @password;";
            SqlParameter para1 = new SqlParameter("@account", System.Data.SqlDbType.VarChar) { Value = account };
            SqlParameter para2 = new SqlParameter("@password", System.Data.SqlDbType.VarChar) { Value = password };

            int count = NSQLHelper.ExecuteNonQuery(cmdStr, para1, para2);
            return count;
        }

        #endregion

        public AccountModel() { }

        /// <summary>
        /// 藉由傳入的帳號名稱生成此Model實例
        /// </summary>
        public AccountModel(string account)
        {
            QueryData(account);
        }

        public bool AddToDatabase()
        {
            if (AddToDatabase(this.account, this.password))
            {
                this.Id = GetPrimaryKey(this.account);
                return true;
            }
            else
            {
                OutPutManager.WriteConsole("加入帳號失敗!資料庫中受引響的行數為0");
                return false;
            }
        }


        /// <summary>
        /// 藉由傳入的帳號名稱生成此Model實例
        /// </summary>
        /// <param name="account"></param>
        private void QueryData(string account)
        {
            string cmdStr = "select * from Account where account = @account;";
            SqlParameter para = new SqlParameter("@account", System.Data.SqlDbType.VarChar) { Value = account };
            using (SqlDataReader reader = NSQLHelper.ExecuteReader(cmdStr))
            {
                if (reader.HasRows)
                {
                    if (reader.Read())
                    {
                        this.id = reader.GetInt32(0);
                        this.account = reader.GetString(1);
                        this.password = reader.GetString(2);
                    }
                }
            }
        }

        #endregion
    }
}
