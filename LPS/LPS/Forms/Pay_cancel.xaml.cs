using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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
//using System.Windows.Forms;

using LPS.Utility;


namespace LPS.Forms
{
    /// <summary>
    /// CreateAccount.xaml 的交互逻辑
    /// </summary>
    public partial class Pay_cancel : Window
    {
        public Pay_cancel()
        {
            InitializeComponent();
        }
        //print 销售单
        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {


        }

        //pay
        private void Button_Click(object sender, RoutedEventArgs e)
        {

            int Index = 0;
           
            if (ListView_pay.SelectedItems.Count > 0)//判断listview有被选中项
            {
                //Index = ListView_pay.SelectedItems[0].Index;//取当前选中项的index,SelectedItems[0]这必须为0       
                //listView1.Items[Index].Remove();
                if (ListView_pay.SelectedItems.Count == 0) return;
                else
                {
                    String changeID = ListView_pay.SelectedItems[0].ToString();
                    Database.changeSql("Sales_batch", changeID, "Sales_batch_status", "已付款");
                }
            }
            ListView_pay.Items.Refresh();//刷新listview


        }
        //cancel
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            //ListView.SelectedIndexCollection c = ListView_pay.SelectedIndices; //获取Listview_Name你选中的那条数据的索引
            //String =" ";//未获取
            
            if (ListView_pay.SelectedItems.Count == 0) return;
            else
            {
                //string site = ListView_pay.SelectedItems[0].Text;
                //string type = ListView_pay.SelectedItems[0].
                    //.SubItems[1].Text;
                string changeID2 = ListView_pay.SelectedItems[0].ToString();
                Database.changeSql("Sales_batch", changeID2, "Sales_batch_status", "取消");
            }

            //ListView_pay.ClearValue();
            
            ListView_pay.Items.Refresh();//刷新listview


        }
        
        //check
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            DataTable dataTable1 = Database.FillDataTable("select Sales_batch_id_PK, Price_of_all, createdate, Sales_batch_status from Sales_batch where Customer_id_FK = UID and Sales_batch_status = '待付款' ");
            //ListView_pay.View = View.Details;
            dataTable1.Columns.Add("Sales_lot_order_number");
            dataTable1.Columns.Add("Total_price");
            dataTable1.Columns.Add("Sales_date");
            dataTable1.Columns.Add("Status");
            ListView_pay.DataContext = dataTable1;


        }
    }
}
