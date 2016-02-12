/*
 * Author:ImL1s
 *
 * Date:2016/02/08
 *
 * Email:ImL1s@outlook.com
 *
 * description:
 *
 */

using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Dal
{

    public static class NSQLHelper
    {
        // 連接字符串
        static string connStr = ConfigurationManager.ConnectionStrings["connection1"].ToString();

        /// <summary>
        /// 連接字符串
        /// </summary>
        public static string ConnStr
        {
            get { return connStr; }

            set { connStr = value; }
        }

        /// <summary>
        /// 實現非查詢操作，增刪改返回受影響行數，否則返回-1
        /// </summary>
        /// <param name="cmdStr"></param>
        /// <param name="paras"></param>
        /// <returns></returns>
        public static int ExecuteNonQuery(string cmdStr, params SqlParameter[] paras)
        {
            using (SqlConnection conn = new SqlConnection(ConnStr))
            {
                using (SqlCommand cmd = new SqlCommand(cmdStr, conn))
                {
                    if (paras != null) cmd.Parameters.AddRange(paras);
                    conn.Open();
                    int affterRow = cmd.ExecuteNonQuery();

                    return affterRow;
                }
            }

        }

        /// <summary>
        /// 查詢並且返回可以讀取資料庫的鑰匙
        /// </summary>
        /// <param name="cmdStr"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static SqlDataReader ExecuteReader(string cmdStr, params SqlParameter[] parameters)
        {
            return ExecuteReader(cmdStr, CommandType.Text, parameters);
        }

        /// <summary>
        /// 查詢並且返回可以讀取資料庫的鑰匙(SqlDataReader)
        /// </summary>
        /// <param name="cmdStr"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static SqlDataReader ExecuteReader(string cmdStr, CommandType commandType, params SqlParameter[] parameters)
        {
            SqlConnection conn = new SqlConnection(ConnStr);


            using (SqlCommand cmd = new SqlCommand(cmdStr, conn))
            {
                try
                {
                    cmd.CommandType = commandType;
                    cmd.Parameters.AddRange(parameters);
                    conn.Open();

                    // CommandBehavior.CloseConnection Connection會在相關的SqlDataReader關閉時才關閉.
                    return cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
                }
                // 查詢類庫不要吃掉異常
                catch (Exception e)
                {
                    cmd.Dispose();
                    conn.Dispose();
                    throw e;
                }
            }
        }

        /// <summary>
        /// 執行查詢，並且返回第一個資料列的第一個資料行.
        /// </summary>
        /// <param name="cmdStr"></param>
        /// <param name="paras"></param>
        /// <returns></returns>
        public static object ExecuteScaler(string cmdStr, params SqlParameter[] paras)
        {
            using (SqlConnection conn = new SqlConnection(ConnStr))
            {
                using (SqlCommand cmd = new SqlCommand(cmdStr, conn))
                {
                    if (paras != null) cmd.Parameters.AddRange(paras);

                    conn.Open();
                    object affterRow = cmd.ExecuteScalar();

                    return affterRow;
                }
            }
        }


        /// <summary>
        /// 取得DataSet(不建議在資料量過多時使用，除非你的記憶體夠多...)
        /// </summary>
        /// <param name="cmdStr"></param>
        /// <param name="paras"></param>
        /// <returns></returns>
        public static DataSet GetDataSet(string cmdStr, params SqlParameter[] paras)
        {
            DataSet set = new DataSet();

            using (SqlDataAdapter adapter = new SqlDataAdapter(cmdStr, ConnStr))
            {
                adapter.SelectCommand.Parameters.AddRange(paras);

                adapter.Fill(set);

                return set;
            }

        }
    }
}
