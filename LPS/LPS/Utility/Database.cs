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
        public static readonly string DatabaseName = "LPS-Database";
        public static string UID = "0123123";//for debug, it should be String.Empty;//登陆ID
        public static int UNO = 1;
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
        /// <param name="Parameters">key为参数名（不可太长）@CODE，List的第一个元素是参数类型SqlDbType，第二个元素是参数的值Value</param>
        /// <returns>执行是否成功</returns>
        public static bool Insert(Dictionary<string,List<Object>> Parameters, string Command)
        {
            //插入命令 
            //string comInsert = "insert into Class (code, className, campus, school, grade, contact, phoneNumber, email) " +
            //    "values(@CODE, @CLASSNAME, @CAMPUS, @SCHOOL, @GRADE, @CONTACT, @PHONE, @EMAIL)";
            string comInsert = Command;
            int Return = 0;
            //try
            //{
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
            //}
            //catch (SqlException ex)
            //{
            //    //TODO:添加ex输出信息
            //    System.Windows.MessageBox.Show("Sql error(s) occur: " + ex.ToString());
            //    return false;
            //}catch(Exception ex)
            //{
            //    //TODO:添加ex输出信息
            //    System.Windows.MessageBox.Show("Error(s) occur: " + ex.ToString());
            //    return false;
            //}
            return true;
        }

        /// <summary>
        /// 对于数据库单个数据的查询
        /// </summary>
        /// <param name="Command">查询的命令</param>
        /// <returns>如果没有则返回Null</returns>
        public static object Query(string Command)
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
        /// <param name="command">执行的查询</param>
        /// <returns></returns>
        public static DataTable FillDataTable(string command)
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

        /// <summary>
        /// 切割字符串
        /// </summary>
        /// <param name="str"></param>
        /// <param name="bg"></param>
        /// <param name="ed"></param>
        /// <returns></returns>
        public static string StringCut(string str, string bg, string ed)
        {
            string sub;
            sub = str.Substring(str.IndexOf(bg) + bg.Length);
            sub = sub.Substring(0, sub.IndexOf(";"));
            return sub;
        }
        /// <summary>
        /// 构造文件名
        /// </summary>
        /// <returns>文件名</returns>
        private static string AddTimeStamp(string STRING)
        {
            string CurrTime = System.DateTime.Now.ToString();
            CurrTime = CurrTime.Replace(" ", "|");
            STRING += CurrTime;
            return STRING;
        }

        //private void Step(string message, int percent)
        //{
        //    Bar.Value = percent;
        //}

        /// <summary>
        /// 数据库备份
        /// </summary>
        /// <returns>备份是否成功</returns>
        public static bool DbBackup(string FilePath)
        {
            bool returnVal = false;
            string filePath = "//_db_" + AddTimeStamp(FilePath) + ".BAK";
            SQLDMO.Backup oBackup = new SQLDMO.BackupClass();
            SQLDMO.SQLServer oSQLServer = new SQLDMO.SQLServerClass();
            try
            {
                oSQLServer.LoginSecure = false;
                oSQLServer.Connect(DefaultConnectionString, UID, Password);
                oBackup.Action = SQLDMO.SQLDMO_BACKUP_TYPE.SQLDMOBackup_Database;
                //SQLDMO.BackupSink_PercentCompleteEventHandler pceh = new SQLDMO.BackupSink_PercentCompleteEventHandler(Step);
                //oBackup.PercentComplete += pceh;
                oBackup.Database = DatabaseName;
                oBackup.Files = filePath;
                oBackup.BackupSetName = DatabaseName;
                oBackup.BackupSetDescription = AddTimeStamp("数据库备份");
                oBackup.Initialize = true;
                oBackup.SQLBackup(oSQLServer);
                returnVal = true;
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message);
            }
            finally
            {
                oSQLServer.DisConnect();
            }
            return returnVal;
        }

        /// <summary>
        /// 数据库恢复
        /// </summary>
        public static bool DbRestore(string FilePath)
        {
            bool returnVal = false;
            if (exepro() != true)
            {
                
            }
            else
            {
                SQLDMO.Restore oRestore = new SQLDMO.RestoreClass();
                SQLDMO.SQLServer oSQLServer = new SQLDMO.SQLServerClass();
                try
                {
                    exepro();
                    oSQLServer.LoginSecure = false;
                    oSQLServer.Connect(DefaultConnectionString, UID, Password);
                    oRestore.Action = SQLDMO.SQLDMO_RESTORE_TYPE.SQLDMORestore_Database;
                    //SQLDMO.RestoreSink_PercentCompleteEventHandler pceh = new SQLDMO.RestoreSink_PercentCompleteEventHandler(Step);
                    //oRestore.PercentComplete += pceh;
                    oRestore.Database = DatabaseName;
                    ///自行修改
                    oRestore.Files = FilePath;
                    oRestore.FileNumber = 1;
                    oRestore.ReplaceDatabase = true;
                    oRestore.SQLRestore(oSQLServer);
                    returnVal = true;
                }
                catch (Exception e)
                {
                    System.Windows.MessageBox.Show("恢复失败" + e.Message);
                }
                finally
                {
                    oSQLServer.DisConnect();
                }
            }
            return returnVal;
        }

        /// <summary>
        /// 杀死当前库的所有进程
        /// </summary>
        /// <returns></returns>
        private static bool exepro()
        {
            bool success = true;
            SQLDMO.SQLServer svr = new SQLDMO.SQLServerClass();
            try
            {
                svr.Connect(DefaultConnectionString, UID, Password);
                //取得所有的进程列表
                SQLDMO.QueryResults qr = svr.EnumProcesses(-1);
                int iColPIDNum = -1;
                int iColDbName = -1;
                //找到和要恢复数据库相关的进程
                for (int i = 1; i <= qr.Columns; i++)
                {
                    string strName = qr.get_ColumnName(i);
                    if (strName.ToUpper().Trim() == "SPID")
                    {
                        iColPIDNum = i;
                    }
                    else if (strName.ToUpper().Trim() == "DBNAME")
                    {
                        iColDbName = i;
                    }
                    if (iColPIDNum != -1 && iColDbName != -1)
                        break;
                }
                //将相关进程关闭   
                for (int i = 1; i <= qr.Rows; i++)
                {
                    int lPID = qr.GetColumnLong(i, iColPIDNum);
                    string strDBName = qr.GetColumnString(i, iColDbName);
                    if (strDBName.ToUpper() == DatabaseName)
                        svr.KillProcess(lPID);
                }
            }
            catch (Exception ex)
            {
                success = false;
                System.Windows.MessageBox.Show("Error occur when killing the process connecting to db. " + ex.Message);
            }
            return success;
        }

        /*
        public static bool Operate(bool isBackup)
        {
            //备份：use master;backup database @name to disk=@path;
            //恢复：use master;restore database @name from disk=@path; 
            SqlConnection connection = new SqlConnection("Data Source=" + server + ";initial catalog=" + database + ";user id=" + uid + ";password=" + pwd + ";");
            if (!restoreFile.EndsWith(".bak"))
            {
                restoreFile += ".bak";
            }
            if (isBackup)//备份数据库 
            {
                SqlCommand command = new SqlCommand("use master;backup database @name to disk=@path;", connection);
                connection.Open();
                command.Parameters.AddWithValue("@name", Database);
                command.Parameters.AddWithValue("@path", restoreFile);
                command.ExecuteNonQuery();
                connection.Close();
            }
            else//恢复数据库 
            {
                SqlCommand command = new SqlCommand("use master;restore database @name from disk=@path;", connection);
                connection.Open();
                command.Parameters.AddWithValue("@name", Database);
                command.Parameters.AddWithValue("@path", restoreFile);
                command.ExecuteNonQuery();
                connection.Close();
            }
            return true;
        }*/


        /// <summary>
        ///  ZHung修改数据表:要改的表 要修改表的主键 具体对象 修改后的内容
        /// </summary>
        /// <param name="Table_name"></param>
        /// <param name="change_ID"></param>
        /// <param name="Change_item"></param>
        /// <param name="Change_status"></param>
        public static void changeSql(String Table_name, String change_ID, String Change_item, String Change_status)
        {
            using (SqlConnection connection = new SqlConnection(
connectionString(DefaultConnectionString, UID, Password)))
            {
                try
                {
                    connection.Open();
                    string strmodify = "Update " + Table_name + "set " + Change_item + "='" + Change_status + "'" + "where " + Table_name + "_id_PK=" + "'" + change_ID + "'";
                    SqlCommand sqlcmd = new SqlCommand(strmodify, connection);
                    sqlcmd.ExecuteNonQuery();
                    System.Windows.MessageBox.Show("修改成功");
                }
                catch (Exception ex)
                {
                    System.Windows.MessageBox.Show("连接错误" + ex.Message);
                }
                finally
                {
                    connection.Close();
                }
            }
        }

    }
}
