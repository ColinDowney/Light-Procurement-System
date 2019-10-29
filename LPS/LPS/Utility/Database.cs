using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security;
using System.Text;

namespace LPS.Utility
{
    public static class Database
    {
        public static readonly string DefaultConnectionString = "server=LAPTOP-G05DHBB3\\SQLEXPRESS;database=LPS-Database;";//Integrated Security=true;

        private static string connectionString(string ConnectionString, string UID, SecureString Password)
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
    }
}
