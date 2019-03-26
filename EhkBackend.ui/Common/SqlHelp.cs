using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace EhkBackend.ui.Common
{
    public abstract class SqlHelp
    {


        public static int ExecuteNonQuery(string connection, string sql, params SqlParameter[] param)
        {
            //实例连接对象 并指定连接字符串 自动释放资源 不用关闭
            using (SqlConnection conn = new SqlConnection(connection))
            {
                //实例化命令对象 指定sql与连接对象
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    //如果有参数
                    if (param != null)
                    {

                        //批量添加参数
                        cmd.Parameters.AddRange(param);
                    }
                    //打开连接
                    conn.Open();
                    //执行sql并返回影响行数
                    return cmd.ExecuteNonQuery();
                }

            }
        }
    }
}