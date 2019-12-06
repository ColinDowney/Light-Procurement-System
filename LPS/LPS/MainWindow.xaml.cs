using LPS.Forms;
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

namespace LPS
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //Login login = new Login();
            //login.Show();

            //MainMenu menu = new MainMenu();
            //menu.Show();

            //DataView dataView = new DataView();
            //dataView.Show();

            //CreateAccount createAccount = new CreateAccount();
            //createAccount.Show();

            //Forms.Order.DataEntry dataEntry = new Forms.Order.DataEntry();
            //dataEntry.Show();

            Forms.Order.OrderAudit orderAudit = new Forms.Order.OrderAudit();
            orderAudit.Show();
        }
    }
}
