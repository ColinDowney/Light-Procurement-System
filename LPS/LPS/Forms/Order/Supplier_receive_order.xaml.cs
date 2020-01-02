using LPS.Utility;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
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
namespace LPS.Forms.Order
{
    /// <summary>
    /// Supplier_receive_order.xaml 的交互逻辑
    /// </summary>
    public partial class Supplier_receive_order : Window
    {/*
        public static string[] Order_to_supplier = {"订货单号","供货商编号","总价","创建日期","订货备注" };
        public static string[] Order_to_supplier_information = {"货品类型","货品名称","货品型号","数量","单价" };*/
        private BackgroundWorker _demoBGWorker = new BackgroundWorker();
        private ObservableCollection<MyItem> _data { get; set; }
        private ObservableCollection<Info> _info { get; set; }
        public Supplier_receive_order()
        {
            InitializeComponent();
            for (int i = 0; i < ConstantValue.Order_formColName.Length; ++i)
            {
                ReceiveOrderDataGrid.Columns[i].Header = ConstantValue.Order_to_supplier[i];
            }
            for (int i = 0; i < ConstantValue.Order_informationColName.Length; ++i)
            {
                ReceiveOrderInfoDataGrid.Columns[i].Header = ConstantValue.Order_to_supplier_information[i];
            }
        }
        /// <summary>
        /// 展示Sale_form绑定的数据模型
        /// </summary>
        private class MyItem
        {

            public int OID { get; set; }
            public int SID { get; set; }
            public double PriceALL { get; set; }
            public string Date { get; set; }
            public string Notes { get; set; }
            public bool isSelected { get; set; }

            public MyItem(int oid, int sID, double priceALL, string date, string notes)
            {
                OID = oid;
                SID = sID;
                PriceALL = priceALL;
                Date = date;
                Notes = notes;
                isSelected = false;
            }


            public void Set(int oid, int sID, double priceALL, string notes)
            {
                OID = oid;
                SID = sID;
                PriceALL = priceALL;
                Notes = notes;
            }
        }

        /// <summary>
        /// 展示Sale_information绑定的数据模型
        /// </summary>
        private class Info
        {
            public string PC { get; set; }
            public string PN { get; set; }
            public string PM { get; set; }
            public int NP { get; set; }
            public double PP { get; set; }

            public Info(string pc, string pn, string pm, int np, double pp)
            {
                PC = pc;
                PN = pn;
                PM = pm;
                NP = np;
                PP = pp;
            }
        }


        private void BGWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            _data = new ObservableCollection<MyItem>();//因为BGW不支持多线程 所以只能放这儿 也不太懂为什么
            _info = new ObservableCollection<Info>();
            try
            {
                //耗时操作
                DataTable dataTable = Database.FillDataTable("SELECT * FROM Sales_batch JOIN Purchase_notice ON Sales_batch.Sales_batch_id_PK=Purchase_notice.Sales_batch_id_FK");
                foreach (DataRow row in dataTable.Rows)
                {
                    MyItem tempItem = new MyItem((int)row["Order_form_to_supplier_id_PK"], (int)row["Supplier_id_FK"], (double)row["Price_of_all"],
                        ((string)row["Order_to_supplier_createdate"]), ((string)row["Order_to_supplier_notes"]).Trim());
                    _data.Add(tempItem);
                }
            }
            catch (Exception ex)
            {
                Tools.ShowMessageBox(ex.Message);
            }
        }


        /// <summary>
        /// 耗时操作（加载数据）完成后对UI进行赋值
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BGWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            ReceiveOrderDataGrid.ItemsSource = _data;//因为BGW不支持多线程 所以只能放这儿 也不太懂为什么
            ReceiveOrderInfoDataGrid.ItemsSource = _info;
            //计算过程中的异常会被抓住，在这里可以进行处理。
            if (e.Error != null)
            {
                Type errorType = e.Error.GetType();
                MessageBox.Show("Error(s) occur when initialize the datagrid:" + e.Error.Message);
            }
        }

        /// <summary>
        /// 在选中订购单时在下面的DataGrid显示对应的订购详单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DataGridRow_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                //先清空之前的信息
                _info.Clear();

                //获取选中的数据行
                MyItem info = null;
                if (sender != null)
                {
                    DataGridRow grid = sender as DataGridRow;
                    info = grid.Item as MyItem;
                }

                DataTable dataTable = Database.FillDataTable(
                    "SELECT Product_category,Product_name,Product_modle,Num_of_product, Price_of_product  FROM Order_to_supplier_information WHERE Order_form_to_supplier_id_FK=" + info.OID);
                foreach (DataRow row in dataTable.Rows)
                {
                    Info tempItem = new Info(((string)row["Product_category"]).Trim(), ((string)row["Product_name"]).Trim(), ((string)row["Product_modle"]).Trim(), (int)row["Num_of_product"], (double)row["Price_of_product"]);
                    _info.Add(tempItem);
                }
            }
            catch (Exception ex)
            {
                Tools.ShowMessageBox(ex.Message);
            }
        }

        private void ACK_Click(object sender, RoutedEventArgs e)
        {
            return;
        }
    }
}
