using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Threading;

namespace Ailand.WebService.wsclass
{
    public class DataClass
    {
        //获取数据库链接
        private readonly string connStr = ConfigurationManager.AppSettings["DataLink"].ToString();

        private ReaderWriterLockSlim slim = new ReaderWriterLockSlim();
        private SqlConnection connection = null;

        /// <summary>
        /// 打开数据库链接
        /// </summary>
        private void OpenData()
        {
            if (connection == null)
            {
                slim.EnterWriteLock();
                if (connection == null)
                {
                    connection = new SqlConnection(connStr);
                    if (connection.State == ConnectionState.Open || ConnectionState.Broken == connection.State)
                    {
                        connection.Close();
                    }
                    connection.Open();
                }
                slim.ExitWriteLock();
            }
        }

        /// <summary>
        /// 关闭数据库连接
        /// </summary>
        private void CloseData()
        {
            if (ConnectionState.Open == connection.State || ConnectionState.Broken == connection.State)
            {
                connection.Close();
            }
            connection.Close();
        }



        /// <summary>
        /// 读取数据
        /// </summary>
        /// <returns></returns>
        public DataSet GetDataSet(string sql, SqlParameter[] para = null)
        {
            try
            {
                OpenData();
                using (SqlDataAdapter adapter = new SqlDataAdapter(sql, connection))
                {
                    DataSet ds = new DataSet();
                    if (para != null)
                    {
                        adapter.SelectCommand.Parameters.AddRange(para);
                    }
                    adapter.Fill(ds, "ds");
                    return ds;
                }
            }
            catch (Exception)
            {
                return new DataSet();
            }
            finally
            {
                CloseData();
            }
        }

        public bool ExeNonQuerys(string sql, SqlParameter[] para = null)
        {
            OpenData();
            using (SqlCommand cmd = new SqlCommand(sql, connection))
            {
                if (para != null)
                {
                    cmd.Parameters.AddRange(para);
                }
                SqlTransaction trans = connection.BeginTransaction();
                cmd.Transaction = trans;
                try
                {
                    cmd.ExecuteNonQuery();
                    trans.Commit();
                    return true;
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    return false;
                }
                finally
                {
                    CloseData();
                    trans.Dispose();
                }
            };
        }



    }
}