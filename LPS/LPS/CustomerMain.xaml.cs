using LPS.Forms;
using LPS.Forms.Order;
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
using System.Windows.Shapes;

namespace LPS
{
    /// <summary>
    /// CustomerMain.xaml 的交互逻辑
    /// </summary>
    public partial class CustomerMain : Window
    {
        public CustomerMain()
        {
            //Database.OnTest();

            InitializeComponent();

            Hello.Content = "你好！" + (string)Database.Query("SELECT Customer_contact FROM Customer_information WHERE Customer_id_PK=" + Database.UNO);
        }

        private void Create_Click(object sender, RoutedEventArgs e)
        {
            DataEntry de = new DataEntry();
            de.Show();
        }

        private void ViewOrder_Click(object sender, RoutedEventArgs e)
        {
            OrderAudit or = new OrderAudit(OrderAudit.OrderAuditType.ViewOrder);
            or.Show();
        }

        private void Pay_Click(object sender, RoutedEventArgs e)
        {
            Pay_cancel pc = new Pay_cancel();
            pc.Show();
        }
    }
}
