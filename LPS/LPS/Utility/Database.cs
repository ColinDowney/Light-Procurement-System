using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using Microsoft.SqlServer.Management.Common;
using Microsoft.SqlServer.Management.Smo;


namespace LPS.Utility
{
    public static class Database
    {//LAPTOP-G05DHBB3\\SQLEXPRESS
        public static readonly string ServerName = "LAPTOP-G05DHBB3\\SQLEXPRESS";
        public static readonly string DefaultConnectionString = "server="+ServerName+";database=LPSDB;";//Integrated Security=true;
        public static readonly string DatabaseAddress = "server=" + ServerName + ";";
        public static readonly string InstanceName = ServerName;
        public static readonly string DatabaseName = "LPSDB";
        public static string UID = String.Empty;//for debug, it should be String.Empty;//登陆ID
        public static int UNO = -1;
        private static SecureString Password = new SecureString();//登陆密码

        public static void OnTest()
        {
            UID = "Colin";
            UNO = 1;
            Password.Clear();
            Password.AppendChar('1');
            Password.AppendChar('2');
            Password.AppendChar('3');
            Password.AppendChar('4');
        }

        private static void setUID(string uid)
        {
            UID = uid;
        }

        private static void setPassword(SecureString password)
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
            UNO = -1;
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

        public static bool Login(string _UID, SecureString _Password)
        {
            if (Authentication(_UID, _Password))
            {
                UID = _UID;
                Password = _Password.Copy();
                UNO = (int)Query("SELECT Admin_id_PK FROM Admin_information WHERE Admin_name='" + _UID + "'");
                return true;
            }
            else
                return false;
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
                return -111;
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
        public static string AddTimeStamp(string STRING)
        {
            string CurrTime = System.DateTime.Now.ToString();
            CurrTime = CurrTime.Replace(" ", "-");
            CurrTime = CurrTime.Replace(":", "");
            CurrTime = CurrTime.Replace("/", "");
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
        public static bool DbBackup()
        {
            bool returnVal = false;
            try
            {
                // Connecting to a named instance of SQL Server with SQL Server Authentication using ServerConnection  
                ServerConnection srvConn = new ServerConnection();
                srvConn.ServerInstance = InstanceName;   // connects to named instance  
                srvConn.LoginSecure = false;   // set to true for Windows Authentication  
                srvConn.Login = UID;
                srvConn.Password = Tools.SecureStringToString(Password);
                Server srv = new Server(srvConn);
                //Console.WriteLine(srv.Information.Version);   // connection is established  


               // Connect to the local, default instance of SQL Server.   
                //Server srv = new Server();
            // Reference the AdventureWorks2012 database.   
            Microsoft.SqlServer.Management.Smo.Database db = default(Microsoft.SqlServer.Management.Smo.Database);
            db = srv.Databases[DatabaseName];

            // Store the current recovery model in a variable.   
            int recoverymod;
            recoverymod = (int)db.DatabaseOptions.RecoveryModel;

            // Define a Backup object variable.   
            Backup bk = new Backup();

            // Specify the type of backup, the description, the name, and the database to be backed up.   
            bk.Action = BackupActionType.Database;
            bk.BackupSetDescription = "Full backup of LPS";
            bk.BackupSetName = AddTimeStamp(DatabaseName)+"BACKUP";
            bk.Database = DatabaseName;

            // Declare a BackupDeviceItem by supplying the backup device file name in the constructor, and the type of device is a file.   
            BackupDeviceItem bdi = default(BackupDeviceItem);
            bdi = new BackupDeviceItem("LPS_Full_Backup", DeviceType.File);

            // Add the device to the Backup object.   
            bk.Devices.Add(bdi);
            // Set the Incremental property to False to specify that this is a full database backup.   
            bk.Incremental = false;

            // Set the expiration date.   
            System.DateTime backupdate = new System.DateTime();
            backupdate = System.DateTime.Now;
            bk.ExpirationDate = backupdate.AddMonths(6);

            // Specify that the log must be truncated after the backup is complete.   
            bk.LogTruncation = BackupTruncateLogType.Truncate;

            // Run SqlBackup to perform the full database backup on the instance of SQL Server.   
            bk.SqlBackup(srv);

            // Inform the user that the backup has been completed.   
            //System.Console.WriteLine("Full Backup complete.");

            // Remove the backup device from the Backup object.   
            bk.Devices.Remove(bdi);

                returnVal = true;
            }
            catch(Exception ex)
            {
                Tools.ShowMessageBox(ex.Message);
            }
            return returnVal;

            //// Remove the backup files from the hard disk.  
            //// This location is dependent on the installation of SQL Server  
            //System.IO.File.Delete("C:\\Program Files\\Microsoft SQL Server\\MSSQL12.MSSQLSERVER\\MSSQL\\Backup\\Test_Full_Backup1");
            //System.IO.File.Delete("C:\\Program Files\\Microsoft SQL Server\\MSSQL12.MSSQLSERVER\\MSSQL\\Backup\\Test_Differential_Backup1");

            /*
            bool returnVal = false;
            
            SQLDMO.Backup oBackup = new SQLDMO.BackupClass();
            SQLDMO.SQLServer oSQLServer = new SQLDMO.SQLServerClass();
            try
            {
                oSQLServer.LoginSecure = false;
                oSQLServer.Connect("(local)", "sa","1234");
                oBackup.Action = SQLDMO.SQLDMO_BACKUP_TYPE.SQLDMOBackup_Database;
                //SQLDMO.BackupSink_PercentCompleteEventHandler pceh = new SQLDMO.BackupSink_PercentCompleteEventHandler(Step);
                //oBackup.PercentComplete += pceh;
                oBackup.Database = DatabaseName;
                oBackup.Files = FilePath;
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
            return returnVal;*/
        }

        /// <summary>
        /// 数据库恢复
        /// </summary>
        public static bool DbRestore()
        {
            bool returnVal = false;
            try
            {//
                if (ExecuteSqlCommand(String.Format("ALTER DATABASE {0} SET RESTRICTED_USER WITH ROLLBACK IMMEDIATE", DatabaseName)) == -111)
                {
                    throw new Exception("|| Unable to kill the other user connection.");
                }


                // Connecting to a named instance of SQL Server with SQL Server Authentication using ServerConnection  
                ServerConnection srvConn = new ServerConnection();
                srvConn.ServerInstance = InstanceName;   // connects to named instance  
                srvConn.LoginSecure = false;   // set to true for Windows Authentication  
                srvConn.Login = UID;
                srvConn.Password = Tools.SecureStringToString(Password);
                Server srv = new Server(srvConn);
                //Console.WriteLine(srv.Information.Version);   // connection is established  

                // Reference the AdventureWorks2012 database.   
                Microsoft.SqlServer.Management.Smo.Database db = default(Microsoft.SqlServer.Management.Smo.Database);
                db = srv.Databases[DatabaseName];

                // Store the current recovery model in a variable.   
                int recoverymod;
                recoverymod = (int)db.DatabaseOptions.RecoveryModel;

                // Declare a BackupDeviceItem by supplying the backup device file name in the constructor, and the type of device is a file.   
                BackupDeviceItem bdi = default(BackupDeviceItem);
                bdi = new BackupDeviceItem("LPS_Full_Backup", DeviceType.File);

                // Delete the AdventureWorks2012 database before restoring it  
                //db.Drop();

                // Define a Restore object variable.  
                Restore rs = new Restore();

                // Set the NoRecovery property to true, so the transactions are not recovered.   
                rs.NoRecovery = true;

                // Add the device that contains the full database backup to the Restore object.   
                rs.Devices.Add(bdi);

                // Specify the database name.   
                rs.Database = DatabaseName;

                // Restore the full database backup with no recovery.   
                rs.SqlRestore(srv);

                // Inform the user that the Full Database Restore is complete.   
                //Console.WriteLine("Full Database Restore complete.");

                // reacquire a reference to the database  
                db = srv.Databases[DatabaseName];

                // Remove the device from the Restore object.  
                rs.Devices.Remove(bdi);

                // Set the NoRecovery property to False.   
                rs.NoRecovery = false;

                returnVal = true;

            }
            catch(Exception ex)
            {
                Tools.ShowMessageBox(ex.Message);
            }
            finally
            {
                ExecuteSqlCommand(String.Format("ALTER DATABASE '{0}' SET MULTI_USER; ", DatabaseName));
            }
            return returnVal;
            /* bool returnVal = false;
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
             return returnVal;*/
        }

        public static bool IncrementBackUp()
        {
            bool returnVal = false;
            try
            {

                // Connect to the local, default instance of SQL Server.   
                Server srv = new Server();
                // Reference the database.   
                Microsoft.SqlServer.Management.Smo.Database db = default(Microsoft.SqlServer.Management.Smo.Database);
                db = srv.Databases[DatabaseName];
                // Define a Backup object variable.   
                Backup bk = new Backup();

                // Specify the type of backup, the description, the name, and the database to be backed up.   
                bk.Action = BackupActionType.Database;
                bk.BackupSetDescription = "Full backup of LPS";
                bk.BackupSetName = AddTimeStamp(DatabaseName) + "BACKUP";
                bk.Database = DatabaseName;

                // Create another file device for the differential backup and add the Backup object.   
                BackupDeviceItem bdid = default(BackupDeviceItem);
                bdid = new BackupDeviceItem("LPS_Differential_Backup", DeviceType.File);

                // Add the device to the Backup object.   
                bk.Devices.Add(bdid);

                // Set the Incremental property to True for a differential backup.   
                bk.Incremental = true;

                // Run SqlBackup to perform the incremental database backup on the instance of SQL Server.   
                bk.SqlBackup(srv);

                // Inform the user that the differential backup is complete.   
                //System.Console.WriteLine("Differential Backup complete.");

                // Remove the device from the Backup object.   
                bk.Devices.Remove(bdid);
                returnVal = true;
            }
            catch(Exception ex)
            {
                Tools.ShowMessageBox(ex.Message);
            }
            return returnVal;
        }

        public static bool RestoreFromIncrementalBackUp()
        {
            bool returnVal = false;
            try
            {
                // Connect to the local, default instance of SQL Server.   
                Server srv = new Server();
                // Reference the database.   
                Microsoft.SqlServer.Management.Smo.Database db = default(Microsoft.SqlServer.Management.Smo.Database);
                db = srv.Databases[DatabaseName];

                // Store the current recovery model in a variable.   
                int recoverymod;
                recoverymod = (int)db.DatabaseOptions.RecoveryModel;

                // Create another file device for the differential backup and add the Backup object.   
                BackupDeviceItem bdid = default(BackupDeviceItem);
                bdid = new BackupDeviceItem("LPS_Differential_Backup", DeviceType.File);

                // Define a Restore object variable.  
                Restore rs = new Restore();

                // Set the NoRecovery property to true, so the transactions are not recovered.   
                rs.NoRecovery = true;

                // Specify the database name.   
                rs.Database = DatabaseName;

                // Add the device that contains the differential backup to the Restore object.   
                rs.Devices.Add(bdid);

                // Restore the differential database backup with recovery.   
                rs.SqlRestore(srv);

                // Inform the user that the differential database restore is complete.   
                //System.Console.WriteLine("Differential Database Restore complete.");

                // Remove the device.   
                rs.Devices.Remove(bdid);

                // Set the database recovery mode back to its original value.  
                db.RecoveryModel = (RecoveryModel)recoverymod;
                returnVal = true;
            }catch(Exception ex)
            {
                Tools.ShowMessageBox(ex.Message);
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
                svr.Connect(InstanceName, UID, Password);
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
                System.Windows.MessageBox.Show("||Error occur when killing the process connecting to db. " + ex.Message);
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


        public static void CreateAdminAccount(string userID, SecureString password)
        {
            if (!Tools.checkLetters(userID))
                throw new Exception("Illegal ID.");

            // Connecting to a named instance of SQL Server with SQL Server Authentication using ServerConnection  
            ServerConnection srvConn = new ServerConnection();
            srvConn.ServerInstance = InstanceName;   // connects to named instance  
            srvConn.LoginSecure = false;   // set to true for Windows Authentication  
            srvConn.Login = UID;
            srvConn.Password = Tools.SecureStringToString(Password);
            Server srv = new Server(srvConn);
            //Console.WriteLine(srv.Information.Version);   // connection is established  

            // Reference the AdventureWorks2012 database.   
            Microsoft.SqlServer.Management.Smo.Database db = default(Microsoft.SqlServer.Management.Smo.Database);
            db = srv.Databases[DatabaseName];

            // Creating Logins  
            Microsoft.SqlServer.Management.Smo.Login login = new Microsoft.SqlServer.Management.Smo.Login(srv, userID);
            login.LoginType = LoginType.SqlLogin;
            login.Create(password);

            // Creating Users in the database for the logins created  
            User user = new User(db, userID);
            user.Login = userID;
            user.Create();

            // Creating database permission Sets  
            //DatabasePermissionSet dbPermSet = new DatabasePermissionSet(DatabasePermission.);
            //dbPermSet.Add(DatabasePermission.AlterAnyUser);
            //dbPermSet.Add(DatabasePermission.CreateSchema);
            //dbPermSet.Add(DatabasePermission.Alter);
            //dbPermSet.Add(DatabasePermission.BackupDatabase);
            //dbPermSet.Add(DatabasePermission.Connect);
            //dbPermSet.Add(DatabasePermission.Delete);
            //dbPermSet.Add(DatabasePermission.Insert);
            //dbPermSet.Add(DatabasePermission.Select);
            //dbPermSet.Add(DatabasePermission.Update);
            //dbPermSet.Add(DatabasePermission.ViewDatabaseState);AlterAnySchema

            //// Enumerating through explicit permissions granted to Role1  
            //// enumerates all database permissions for the Grantee  
            //DatabasePermissionInfo[] dbPermsRole1 = db.EnumDatabasePermissions("db_myadmin");
            //foreach (DatabasePermissionInfo dbp in dbPermsRole1)
            //{
            //    dbPermSet.Add(dbp.PermissionType.);
            //    //Console.WriteLine(dbp.Grantee + " has " + dbp.PermissionType.ToString() + " permission.");
            //}

            //// Creating Database roles  
            //DatabaseRole role1 = new DatabaseRole(db, "Role1");
            //role1.Create();

            DatabaseRole role = new DatabaseRole(db, "sysadmin");
            //role.

            // Granting Database Permission Sets to Roles  
            //db.Grant(dbPermSet, "sysadmin");

            // Adding members (Users / Roles) to Role  
            //role.AddMember(userID);

           
        }
    }
}
