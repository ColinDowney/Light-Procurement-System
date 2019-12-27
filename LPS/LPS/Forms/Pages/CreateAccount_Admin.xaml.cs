using LPS.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LPS.Forms.Pages
{
    /// <summary>
    /// CreateAccount_Admin.xaml 的交互逻辑
    /// </summary>
    public partial class CreateAccount_Admin : Page
    {
        public CreateAccount_Admin()
        {
            InitializeComponent();
        }

        private void CreateAccount_AdminAckButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (String.IsNullOrEmpty(NameText.Text) ||
                    String.IsNullOrEmpty(PasswordText.Password))
                    throw new Exception("Invalid input.");

                string comInsert = "INSERT INTO Admin_information(Admin_name, Admin_password)" +
                    "values(@NAME, @PASSWORD)";//插入数据库的命令-（列名）+（参数名-自己起的）
                System.Data.SqlDbType[] types = { System.Data.SqlDbType.VarChar, System.Data.SqlDbType.VarChar };//数据类型
                string[] keys = { "@NAME", "@PASSWORD" };//上面写的参数名

                List<string> values = new List<string>();//用来临时存参数的
                Dictionary<string, List<object>> parameters = new Dictionary<string, List<object>>();//用来传参的
                bool returnVal = false;//判断是否成功执行

                values.Add(NameText.Text.Trim());
                values.Add(PasswordText.Password.Trim());
                for (int j = 0; j < values.Count; ++j)
                {
                    //依次把参数放入字典中
                    parameters[keys[j]] = new List<object> { types[j], values[j] };
                }
                returnVal = Database.Insert(parameters, comInsert);

                if (!returnVal)
                    throw new Exception("Error occur when inserting." );

                MessageBox.Show("添加成功！");
                clearInputField();
            }
            catch (FormatException)
            {
                MessageBox.Show("请输入整数。");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void clearInputField()
        {
            NameText.Text = String.Empty;
            PasswordText.Password = String.Empty;
        }
    }
}
