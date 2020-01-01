using LPS.Forms;
using LPS.Forms.Order;
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
    /// MainMenu.xaml 的交互逻辑
    /// </summary>
    public partial class MainMenu : Window
    {

        public MainMenu()
        {
            InitializeComponent();
            Database.OnTest();
        }

        private void StorageMenuItem_Click(object sender, RoutedEventArgs e)
        {
            DataView dv = new DataView();
            dv.Initialize(new bool[] { true, false, false, false, false, false, false, false, false, false },
                "Inventory_information",
                new string[] { "单号", "货品编号", "数量", "价格", "位置" });
            dv.Show();
        }

        private void Backup_Click(object sender, RoutedEventArgs e)
        {
            //// Configure open file dialog box
            //Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
            //dlg.FileName = Database.AddTimeStamp("LPS_DB"); // Default file name
            //dlg.DefaultExt = ".BAK"; // Default file extension
            //dlg.Filter = " 数据库备份文件 (.BAK)|*.BAK"; // Filter files by extension

            //// Show open file dialog box
            //Nullable<bool> result = dlg.ShowDialog();

            bool result = true;

            bool reV = false;
            // Process open file dialog box results
            if (result == true)
            {
                // Open document
               // string filename = dlg.FileName;
                reV = Database.DbBackup();
            }
            if (reV)
            {
                MessageBox.Show("备份成功~！");
            }
            else
            {
                MessageBox.Show("备份失败哦。");
            }
        }

        private void Import_Click(object sender, RoutedEventArgs e)
        {
            //// Configure open file dialog box
            //Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            //dlg.FileName = Database.AddTimeStamp("LPS_DB"); // Default file name
            //dlg.DefaultExt = ".BAK"; // Default file extension
            //dlg.Filter = " 数据库备份文件 (.BAK)|*.BAK"; // Filter files by extension

            //// Show open file dialog box
            //Nullable<bool> result = dlg.ShowDialog();

            bool result = true;

            bool reV = false;
            // Process open file dialog box results
            if (result == true)
            {
                // Open document
               // string filename = dlg.FileName;
                reV = Database.DbRestore();
            }
            if (reV)
            {
                MessageBox.Show("恢复成功~！");
            }
            else
            {
                MessageBox.Show("恢复失败哦。");
            }
        }

        private void Setting_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Info_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            Application.Current.Shutdown();
            Environment.Exit(0);
        }

        private void OrderAudit_Click(object sender, RoutedEventArgs e)
        {
            OrderAudit or = new OrderAudit(Forms.Order.OrderAudit.OrderAuditType.OrderAudit);
            or.Show();
        }

        private void ViewOrder_Click(object sender, RoutedEventArgs e)
        {
            DataView dv = new DataView();
            dv.Initialize(new bool[] { true, false, false, true, true, false, true, false, false, true },
                "Order_form",
                new string[] { "单号","顾客编号","日期","状态","备注" });
            dv.Show();
        }

        private void OrderInfo_Click(object sender, RoutedEventArgs e)
        {
            DataView dv = new DataView();
            dv.Initialize(new bool[] { true, false, false, false, false, false, false, false, false, false },
                "Order_information",
                new string[] { "单号","订购单号","货品类别","货品名称","货品型号","数量" });
            dv.Show();
        }

        private void ViewInventory_Click(object sender, RoutedEventArgs e)
        {
            DataView dv = new DataView();
            dv.Initialize(new bool[] { true, false, false, false, false, false, false, false, false, false },
                "Inventory_information",
                new string[] { "单号", "货品编号", "数量", "价格", "位置" });
            dv.Show();
        }

        private void ViewProduct_Click(object sender, RoutedEventArgs e)
        {
            DataView dv = new DataView();
            dv.Initialize(new bool[] { true, false, false, false, false, false, false, false, false, false },
                "Product_information",
                new string[] { "货品编号", "货品类别", "货品名称", "货品型号"});
            dv.Show();
        }

        private void AuditQuotation_Click(object sender, RoutedEventArgs e)
        {
            QuotationAudit aq = new QuotationAudit();
            aq.Show();
        }

        private void ViewRFQ_Click(object sender, RoutedEventArgs e)
        {
            DataView dv = new DataView();
            dv.Initialize(new bool[] { true, false, false, true, false, false, true, false, false, true },
                "RFQ",
                new string[] { "单号", "日期", "状态", "备注" , "订购单编号" });
            dv.Show();
        }

        private void ViewQuotation_Click(object sender, RoutedEventArgs e)
        {
            DataView dv = new DataView();
            dv.Initialize(new bool[] { true, false, false, false, false, false, true, true, true, true },
                "Quotation",
                new string[] { "单号", "来源", "供货商编号", "日期", "备注", "询价单编号" });
            dv.Show();
        }

        private void ViewQuotationInfo_Click(object sender, RoutedEventArgs e)
        {
            DataView dv = new DataView();
            dv.Initialize(new bool[] { true, false, false, false, false, false, false, false, false, false },
                "Quotation_information",
                new string[] { "单号", "报价单单号", "订购细单单号", "单价" });
            dv.Show();
        }

        private void ViewSale_Click(object sender, RoutedEventArgs e)
        {
            DataView dv = new DataView();
            dv.Initialize(new bool[] { true, true, false, true, true, false, true, false, false, true },
                "Sales_batch",
                new string[] { "单号", "顾客编号", "订购单编号", "供货来源", "管理员编号", "总价" , "日期","状态","备注","供货商编号" });
            dv.Show();
        }

        private void ViewSaleInfo_Click(object sender, RoutedEventArgs e)
        {
            DataView dv = new DataView();
            dv.Initialize(new bool[] { true, false, false, false, false, false, false, false, false, false },
                "Sales_order",
                new string[] { "单号", "销售单号", "货品编号", "单价", "数量" });
            dv.Show();
        }

        private void PickUp_Click(object sender, RoutedEventArgs e)
        {
            PickUp pu = new PickUp();
            pu.Show();
        }

        private void OrdertoSupplier_Click(object sender, RoutedEventArgs e)
        {
            Order_to_supplier ots = new Order_to_supplier();
            ots.Show();
        }

        private void Purchase_Click(object sender, RoutedEventArgs e)
        {
            Purchase_input pi = new Purchase_input();
            pi.Show();
        }

        private void CreateAccount_Click(object sender, RoutedEventArgs e)
        {
            CreateAccount ca = new CreateAccount();
            ca.Show();
        }

        private void Increment_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ViewOrdertoSupplier_Click(object sender, RoutedEventArgs e)
        {
            DataView dv = new DataView();
            dv.Initialize(new bool[] { true, false, false, false, false, false, false, false, false, false },
                "Order_form_to_supplier",
                new string[] { "单号", "供货商编号", "总价", "日期", "备注" });
            dv.Show();
        }

        private void ViewOrdertoSupplierInfo_Click(object sender, RoutedEventArgs e)
        {
            DataView dv = new DataView();
            dv.Initialize(new bool[] { true, false, false, false, false, false, false, false, false, false },
                "Order_form_to_supplier_information",
                new string[] {"单号","订货单编号", "货品类别", "销售单号", "货品编号", "数量", "单价" });
            dv.Show();
        }

        private void ViewPickUp_Click(object sender, RoutedEventArgs e)
        {
            DataView dv = new DataView();
            dv.Initialize(new bool[] { true, false, false, false, false, false, false, false, false, false },
                "Pick_up_order",
                new string[] { "单号", "顾客编号", "订货单编号", "日期" });
            dv.Show();
        }

        private void ViewPurchase_Click(object sender, RoutedEventArgs e)
        {
            DataView dv = new DataView();
            dv.Initialize(new bool[] { true, false, false, false, false, false, false, false, false, false },
                "Purchase_form",
                new string[] { "单号", "供货商编号", "订货单编号","日期", "总价", "备注" });
            dv.Show();
        }
    }
}
