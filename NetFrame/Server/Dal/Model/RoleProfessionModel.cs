/*
 * Author:ImL1s
 *
 * Date:2016/03/01
 *
 * Email:ImL1s@outlook.com
 *
 * description:數據庫的角色職業模型.
 *
 */

using System.Data.SqlClient;
using Dal;
using System;
using NetFrame.Tool;

namespace Server.Dal.Model
{
    public class RoleProfessionModel : IModel
    {
        private int id;
        private string profession;

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

        public string Profession
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

        #region static method

        /// <summary>
        /// 從資料庫中取得角色職業類型.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static RoleProfessionModel GetByID(int id)
        {
            string cmd = "select * from RoleProfession where id = @id";
            SqlParameter para = new SqlParameter("@id", id);

            //try
            {
                using (SqlDataReader reader = NSQLHelper.ExecuteReader(cmd, para))
                {
                    if (reader.HasRows)
                    {
                        reader.Read();
                        return new RoleProfessionModel()
                        {
                            id = reader.GetInt32(0),
                            profession = reader.GetString(1)
                        };
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            //catch (Exception e)
            {
                //OutPutManager.WriteConsole("RoleProfessionModel.GetByID(int id) has exception! ID:" + id + "Exception:" + e, true);
                return null;
            }
        }

        #endregion


        public bool AddToDatabase()
        {
            string cmd = "insert into RoleProfessionModel(id,profession) values(@id,@profession);";

            // TODO 完成加入(應該不需要)
            return false;
        }
    }
}

