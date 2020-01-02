using LPS.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace LPS
{
    /// <summary>
    /// Login.xaml 的交互逻辑
    /// </summary>
    public partial class Login : Window
    {
        int type = 0;

        public Login(int Type)
        {
            InitializeComponent();

            type = Type;
        }
        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string userid = "";
            SecureString password = new SecureString();
            if (String.IsNullOrEmpty(LoginUIDInput.Text) || String.IsNullOrEmpty(LoginPasswordInput.Password))
            {
                MessageBox.Show("请输入账号和密码。");
                return;
            }
            userid = LoginUIDInput.Text;
            password = LoginPasswordInput.SecurePassword;
            if (Manager.Functions.Login(userid, password))
            {
                MessageBox.Show("成功登陆");
                if (type == 1)
                {
                    CustomerMain cm = new CustomerMain();
                    cm.Show();
                }
                else if (type == 2)
                {
                    SupplierMain sm = new SupplierMain();
                    sm.Show();
                }
                else if (type == 3)
                {
                    MainMenu mu = new MainMenu();
                    mu.Show();
                }



                this.Close();
            }
            else
            {
                MessageBox.Show("登陆失败");
            }
        }
    }
}
