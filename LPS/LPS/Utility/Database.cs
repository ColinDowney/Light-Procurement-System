using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace LPS.Utility
{
    public static class Database
    {
        public static readonly string DefaultConnectionString = "server=LAPTOP-G05DHBB3\\SQLEXPRESS;database=LPS-Database;Integrated Security=true;";//
        public static string UID = "0123123";//for debug, it should be String.Empty;//登陆ID
        private static SecureString Password = new SecureString();//登陆密码

        public static void setUID(string uid)
        {
            UID = uid;
        }

        public static void setPassword(SecureString password)
        {
            Password = password.Copy();
        }

        public static void ClearPassword()
        {
            Password.Clear();
        }

        public static void Clear()
        {
            ClearPassword();
            UID = String.Empty;
        }

        private static string connectionString(string ConnectionString, string UID, SecureString Password)
        {
            SqlConnectionStringBuilder builder =
    new SqlConnectionStringBuilder(ConnectionString);
            builder.UserID = UID; builder.Password = Tools.SecureStringToString(Password);
            return builder.ConnectionString;
        }

        private static string connectionString(string ConnectionString)
        {
            SqlConnectionStringBuilder builder =
    new SqlConnectionStringBuilder(ConnectionString);
            builder.UserID = UID; builder.Password = Tools.SecureStringToString(Password);
            return builder.ConnectionString;
        }

        /// <summary>
        /// (身份验证)
        /// To validate identities by the input user ID and password.
        /// InvalidOperationException: Cannot open a connection without specifying a data source or server.
        /// ConfigurationErrorsException: There are two entries with the same name in the<localdbinstances> section.
        /// </summary>
        /// <param name="UID"></param>
        /// <param name="Password"></param>
        /// <returns></returns>
        public static bool Authentication(string UID, SecureString Password)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(
   connectionString(DefaultConnectionString, UID, Password)))
                {
                    connection.Open();
                }
            }
            catch (SqlException ex)
            {
                //TODO:添加ex输出信息
                System.Windows.MessageBox.Show(ex.ToString());
                return false;
            }
            return true;
        }

        /// <summary>
        /// For test.
        /// </summary>
        public static void TryConnection()
        {
            using (SqlConnection connection = new SqlConnection(
            DefaultConnectionString))
            {
                connection.Open();
                System.Windows.MessageBox.Show(connection.Database);
            }

        }

        /// <summary>
        /// 向表名为TableName的表插入记录
        /// </summary>
        /// <param name="TableName">要插入的表名</param>
        /// <param name="Parameters">key为参数名（不可太长）@CODE，List的第一个元素是参数类型SqlDbType，第二个元素是参数的值Value</param>
        /// <returns>执行是否成功</returns>
        public static bool Insert(string TableName, Dictionary<string,List<Object>> Parameters, string Command)
        {
            //插入命令 
            //string comInsert = "insert into Class (code, className, campus, school, grade, contact, phoneNumber, email) " +
            //    "values(@CODE, @CLASSNAME, @CAMPUS, @SCHOOL, @GRADE, @CONTACT, @PHONE, @EMAIL)";
            string comInsert = Command;
            int Return = 0;
            try
            {
                using (SqlConnection connection = new SqlConnection(
   connectionString(DefaultConnectionString, UID, Password)))
                {
                    connection.Open();

                    SqlCommand command = connection.CreateCommand();

                    command = new SqlCommand(comInsert, connection);
                    var keys = Parameters.Keys;
                    SqlDbType stype;
                    Type type;
                    foreach (string key in keys)
                    {
                        stype = (SqlDbType)Parameters[key][0];
                        type = Tools.GetType(stype);
                        command.Parameters.Add(key, stype);
                        command.Parameters[key].Value = Convert.ChangeType(Parameters[key][1], type);
                    }


                    //执行添加语句 
                    Return = command.ExecuteNonQuery();

                    if (Return <= 0)
                        throw new Exception("No row is updated. Maybe the Sql go wrong.");
                }
            }
            catch (SqlException ex)
            {
                //TODO:添加ex输出信息
                System.Windows.MessageBox.Show("Sql error(s) occur: " + ex.ToString());
                return false;
            }catch(Exception ex)
            {
                //TODO:添加ex输出信息
                System.Windows.MessageBox.Show("Error(s) occur: " + ex.ToString());
                return false;
            }
            return true;
        }

        /// <summary>
        /// 对于数据库单个数据的查询
        /// </summary>
        /// <param name="TableName">查询的表名</param>
        /// <param name="Command">查询的命令</param>
        /// <returns></returns>
        public static object Query(string TableName, string Command)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(
   connectionString(DefaultConnectionString, UID, Password)))
                {
                    connection.Open();

                    SqlCommand command = connection.CreateCommand();
                    command = new SqlCommand(Command, connection);
                    return command.ExecuteScalar();
                }
            }
            catch (SqlException ex)
            {
                //TODO:添加ex输出信息
                System.Windows.MessageBox.Show("Sql Error(s) occur: " + ex.ToString());
                return null;
            }
        }

        /// <summary>
        /// 获取数据表
        /// </summary>
        /// <param name="tableName">要填充的表名</param>
        /// <param name="command">执行的查询</param>
        /// <returns></returns>
        public static DataTable FillDataTable(string tableName, string command)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(
   connectionString(DefaultConnectionString, UID, Password)))
                {
                    connection.Open();

                    SqlDataAdapter adapter = new SqlDataAdapter(command, connection);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);
                    return dataTable;
                }
            }
            catch (SqlException ex)
            {
                //TODO:添加ex输出信息
                System.Windows.MessageBox.Show("Sql Error(s) occur: " + ex.ToString());
                return null;
            }
        }

        /// <summary>
        /// 执行Sql命令(DELETE, UPDATE, ...)
        /// </summary>
        /// <param name="Command"></param>
        /// <returns></returns>
        public static int ExecuteSqlCommand(string Command)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(
   connectionString(DefaultConnectionString, UID, Password)))
                {
                    connection.Open();

                    SqlCommand command = new SqlCommand(Command, connection);
                    int re = command.ExecuteNonQuery();
                    return re;
                }
            }
            catch (SqlException ex)
            {
                //TODO:添加ex输出信息
                System.Windows.MessageBox.Show("Sql Error(s) occur: " + ex.ToString());
                return -1;
            }
        }
    }
}
