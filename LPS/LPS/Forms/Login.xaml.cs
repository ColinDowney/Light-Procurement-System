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

namespace LPS.Forms
{
    /// <summary>
    /// Login.xaml 的交互逻辑
    /// </summary>
    public partial class Login : Window
    {


        public Login()
        {
            InitializeComponent();
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
            if (Database.Authentication(userid, password))
            {
                MessageBox.Show("成功登陆");
            }
            else
            {
                MessageBox.Show("登陆失败");
            }
        }
    }
}
