using LPS.Forms.RFQ;
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
    /// SupplierMain.xaml 的交互逻辑
    /// </summary>
    public partial class SupplierMain : Window
    {
        public SupplierMain()
        {
            InitializeComponent();

            Hello.Content = "你好！" + (string)Database.Query("SELECT Supplier_name FROM Supplier_information WHERE Supplier_id_PK=" + Database.UNO);
        }

        private void Create_Click(object sender, RoutedEventArgs e)
        {
            QuotationForm qf = new QuotationForm();
            qf.Show();
        }

        private void ViewOrder_Click(object sender, RoutedEventArgs e)
        {
            OrderView voo = new OrderView();
            voo.Show();
        }
    }
}
