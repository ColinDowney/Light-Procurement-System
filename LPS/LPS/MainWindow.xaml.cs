using LPS.Forms;
using LPS.Manager;
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
using static LPS.Manager.Functions;

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

            //Forms.Order.OrderAudit orderAudit = new Forms.Order.OrderAudit();
            //orderAudit.Show();

            //InsertGoodsInOrderToInventory();
            //List<product> backList = Functions.checkInventory();
            //Functions.BackProduct(backList);
            //CreateRFQ(backList, "TEST20191210");

            //Forms.RFQ.RFQForm rfq = new Forms.RFQ.RFQForm();
            //rfq.Show();

            Forms.RFQ.QuotationForm qf = new Forms.RFQ.QuotationForm();
            qf.Show();
        }
    }
}
