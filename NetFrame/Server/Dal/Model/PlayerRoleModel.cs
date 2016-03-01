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

class PlayerRoleModel : IModel
{
    private int id;
    private int accountId;
    private RoleProfessionModel profession;

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

    #region static method 靜態方法

    /// <summary>
    /// 使用帳號ID取得玩家角色.
    /// </summary>
    /// <param name="accountID"></param>
    /// <returns></returns>
    public static PlayerRoleModel GetByAccountID(int accountID)
    {
        string cmd = "select * from PlayerRoleModel where accountId = @id;";
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

    public static PlayerRoleModel GetByAccount(string account)
    {
        throw new System.Exception("尚為實現GetByAccout!");
    }

    #endregion


    public bool AddToDatabase()
    {
        string cmd = "insert into RoleProfessionModel(id,profession) values(@id,@accountId,@profession);";

        SqlParameter[] paras =
        {
            new SqlParameter("@id",this.Id),
            new SqlParameter("@accountId",this.AccountId),
            new SqlParameter("@profession",(int)this.Profession.Id)
        };

        int count = NSQLHelper.ExecuteNonQuery(cmd, paras);

        if (count > 0) return true;
        else return false;
    }

    
}

