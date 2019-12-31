using LPS.Forms;
using LPS.Forms.Order;
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


            //Forms.RFQ.QuotationForm qf = new Forms.RFQ.QuotationForm();
            //qf.Show();

            //Forms.RFQ.QuotationAudit qa = new Forms.RFQ.QuotationAudit();
            //qa.Show();

            //Forms.DataView dv = new DataView();
            //dv.Initialize(new bool[] { true, true, true, true, true, true, true, true, true, true }, "Order_form", new string[] { "单号", "顾客编号", "创建日期", "状态", "备注" });
            //dv.Show();

            //Forms.Order.Purchase_input pi = new Purchase_input();
            //pi.Show();

            //Pay_cancel pc = new Pay_cancel();
            //pc.Show();

            Order_to_supplier os = new Order_to_supplier();
            os.Show();

        }
    }
}

            //InsertGoodsInOrderToInventory();
            //List<product> backList = Functions.checkInventory();
            //Functions.BackProduct(backList);
            //CreateRFQ(backList, "TEST20191210");

            //Forms.RFQ.RFQForm rfq = new Forms.RFQ.RFQForm();
            //rfq.Show();