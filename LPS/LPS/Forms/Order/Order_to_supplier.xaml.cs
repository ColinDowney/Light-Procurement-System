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
/*2*/
namespace LPS.Forms.Order
{
    /// <summary>
    /// Order_to_supplier.xaml 的交互逻辑
    /// </summary>
    public partial class Order_to_supplier : Window
    {
        private BackgroundWorker _demoBGWorker = new BackgroundWorker();
        private ObservableCollection<MyItem> _data { get; set; }
        private ObservableCollection<Info> _info { get; set; }
        private int Order_information_id;
        public Order_to_supplier()
        {
            _demoBGWorker.DoWork += BGWorker_DoWork;
            _demoBGWorker.RunWorkerCompleted += BGWorker_RunWorkerCompleted;
            _demoBGWorker.RunWorkerAsync();
            InitializeComponent();
            for (int i = 0; i < ConstantValue.Order_formColName.Length; ++i)
            {
                OrderSupplierDataGrid.Columns[i].Header = ConstantValue.Sale_formColName[i];
            }
            for (int i = 0; i < ConstantValue.Order_informationColName.Length; ++i)
            {
                OrderSupplierInfoDataGrid.Columns[i].Header = ConstantValue.Sale_informationColName[i];
            }
        }

        /// <summary>
        /// 展示Sale_form绑定的数据模型
        /// </summary>
        private class MyItem
        {
            public int Sale_NO { get; set; }
            public int CID { get; set; }
            public int OID { get; set; }
            public string Product_Source { get; set; }
            public int MID { get; set; }
            public double Total_Price { get; set; }
            public int SID { get; set; }
            public string Date { get; set; }
            public string Sale_State { get; set; }
            public string Sale_Note { get; set; }
            public bool isSelected { get; set; }


            public MyItem(int nO, int cID, int oID, string product_Source,
                int mID, double total_Price, int sID, string date, string sale_State, string sale_Note)
            {
                Sale_NO = nO;
                CID = cID;
                OID = oID;
                Product_Source = product_Source;
                MID = mID;
                Total_Price = total_Price;
                SID = sID;
                Date = date;
                Sale_State = sale_State;
                Sale_Note = sale_Note;
                isSelected = false;
            }

            public void Set(int nO, int cID, int oID, string product_Source,
                int mID, double total_Price, int sID, string date, string sale_State, string sale_Note)
            {
                Sale_NO = nO;
                CID = cID;
                OID = oID;
                Product_Source = product_Source;
                MID = mID;
                Total_Price = total_Price;
                SID = sID;
                Date = date;
                Sale_State = sale_State;
                Sale_Note = sale_Note;
            }
        }

        /// <summary>
        /// 展示Sale_information绑定的数据模型
        /// </summary>
        private class Info
        {
            public int Sale_info_No { get; set; }
            public int Product_No { get; set; }
            public string PC { get; set; }
            public string PN { get; set; }
            public string PM { get; set; }
            public int Num { get; set; }
            public double Price { get; set; }

            public Info(int sale_info_No, int product_No, string pc, string pn, string pm, int num, double price)
            {
                Sale_info_No = sale_info_No;
                Product_No = product_No;
                PC = pc;
                PN = pn;
                PM = pm;
                Num = num;
                Price = price;
            }
        }

        private void BGWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            _data = new ObservableCollection<MyItem>();//因为BGW不支持多线程 所以只能放这儿 也不太懂为什么
            _info = new ObservableCollection<Info>();
            try
            {
                //耗时操作
                DataTable dataTable = Database.FillDataTable("SELECT * FROM Purchase_notice INNER JOIN Sales_batch ON Sales_batch.Sales_batch_id_PK=Purchase_notice.Sales_batch_id_FK WHERE Purchase_notice_status='待进货'");
                foreach (DataRow row in dataTable.Rows)
                {
                    MyItem tempItem = new MyItem((int)row["Sales_batch_id_PK"], (int)row["Customer_id_FK"], (int)row["Order_form_id_FK"],
                        ((string)row["Source_of_goods"]).Trim(), (int)row["Admin_id_FK"], (double)row["Price_of_all"], (int)row["Supplier_id_FK"], ((string)row["createdate"]).Trim(),
                        ((string)row["Sales_batch_status"]).Trim(), ((string)row["Sales_batch_notes"]).Trim());
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
            OrderSupplierDataGrid.ItemsSource = _data;//因为BGW不支持多线程 所以只能放这儿 也不太懂为什么
            OrderSupplierInfoDataGrid.ItemsSource = _info;
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
                string command;
                DataTable tempTable;
                DataTable dataTable = Database.FillDataTable(
                    "SELECT Sales_order_id_PK,Product_id_FK, Price_of_product, Num FROM Sales_order WHERE Sales_batch_id_FK=" + info.Sale_NO);
                foreach (DataRow row in dataTable.Rows)
                {
                    command = string.Format("SELECT Product_category,Product_name,Product_modle FROM Product_information WHERE Product_id_PK={0}", row["Product_id_FK"]);
                    tempTable = Database.FillDataTable(command);
                    Info tempItem = new Info((int)row["Sales_order_id_PK"], (int)row["Product_id_FK"], ((string)tempTable.Rows[0].ItemArray[0]).Trim(),
                        ((string)tempTable.Rows[0].ItemArray[1]).Trim(), ((string)tempTable.Rows[0].ItemArray[2]).Trim(), (int)row["Num"], (double)row["Price_of_product"]);
                    _info.Add(tempItem);
                }
            }
            catch (Exception ex)
            {
                Tools.ShowMessageBox(ex.Message);
            }
        }


        private void OrderFromSupplier_Click(object sender, RoutedEventArgs e)
        {
            List<object> values = new List<object>();//用来临时存参数的
            Dictionary<string, List<Object>> parameters1 = new Dictionary<string, List<object>>();
            Dictionary<string, List<Object>> parameters2 = new Dictionary<string, List<object>>();
            List<MyItem> toRemove = new List<MyItem>();
            try
            {
                foreach (var i in OrderSupplierDataGrid.Items)
                {
                    MyItem item;
                    item = i as MyItem;
                    if (item.isSelected)
                    {
                        string com = string.Format("SELECT * FROM Sales_order WHERE Sales_order_id_PK={0}", item.Sale_NO);
                        DataTable OrderSupplierInfoTable = Database.FillDataTable( com);
                        Info tempItem = null;
                        DataTable tempTable;
                        foreach (DataRow row in OrderSupplierInfoTable.Rows)
                        {
                            com = string.Format("SELECT Product_category,Product_name,Product_modle FROM Product_information WHERE Product_id_PK={0}", row["Product_id_FK"]);
                            tempTable = Database.FillDataTable( com);
                            tempItem = new Info((int)row["Sales_order_id_PK"], (int)row["Product_id_FK"], ((string)tempTable.Rows[0].ItemArray[0]).Trim(),
                                ((string)tempTable.Rows[0].ItemArray[1]).Trim(), ((string)tempTable.Rows[0].ItemArray[2]).Trim(), (int)row["Num_of_product"], (double)row["Price_of_product"]);
                        }
                        string tableName = "Order_to_supplier";
                        com = string.Format("SELECT MAX(Order_form_to_supplier_id_PK) from {0}", tableName);
                        int orderID = (int)Database.Query(com);
                        double price_of_all = tempItem.Price * tempItem.Num;
                        try
                        {
                            string comInsert = "INSERT INTO" + tableName + "(Order_form_to_supplier_id_PK,Supplier_id_FK," +
                                "Price_of_all,Order_form_to_supplier_createdate,Order_form_to_supplier_notes)" +
                                "value(@ID,@SID,@PRICEALL,@DATE,@NOTES)";
                            SqlDbType[] types = { SqlDbType.Int, SqlDbType.Int, SqlDbType.Money, SqlDbType.DateTime, SqlDbType.Char };//数据类型
                            string[] keys = { "@ID", "@SID", "@PRICEALL", "@DATE", "@NOTES" };//上面写的参数名

                            values.Add(orderID);
                            values.Add(item.SID);
                            values.Add(price_of_all);
                            DateTime date = new DateTime();
                            values.Add(date);
                            values.Add(OrderToSupplierNotes.Text);
                            bool returnVal1 = false;
                            for (int j = 0; j < values.Count; ++j)
                            {
                                //依次把三个参数放入字典中
                                parameters1[keys[j]] = new List<object> { types[j], values[j] };
                            }
                            returnVal1 = Database.Insert(parameters1, comInsert);

                            if (!returnVal1)
                                throw new Exception("Error occur when create the order form.");


                        }
                        catch (FormatException)
                        {
                            MessageBox.Show("请输入整数。");
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Error: " + ex.Message);
                        }
                        tableName = "Order_to_supplier_information";
                        com = string.Format("SELECT MAX(Order_form_to_supplier_information_id_PK) from {0}", tableName);
                        int supplierID = (int)Database.Query(com);
                        values.Clear();
                        try
                        {
                            string comInsert = "INSERT INTO" + tableName + "(Order_form_to_supplier_information_id_PK,Order_form_to_supplier_id_FK,Product_category," +
                                "Product_name,Product_modle,Num_of_product,Price_of_product)" +
                                "value(@INFOID,@ID,@PC,@PN,@PM,@NP,@PP)";
                            SqlDbType[] types = { SqlDbType.Int, SqlDbType.Int, SqlDbType.Char, SqlDbType.Char, SqlDbType.Char, SqlDbType.Int, SqlDbType.Money };//数据类型
                            string[] keys = { "@INFOID", "@ID", "@PC", "@PN", "@PM", "@NP", "@PP" };//上面写的参数名
                            values.Add(supplierID);
                            values.Add(orderID);
                            values.Add(tempItem.PC);
                            values.Add(tempItem.PN);
                            values.Add(tempItem.PM);
                            values.Add(tempItem.Num);
                            values.Add(tempItem.Price);
                            bool returnVal2 = false;
                            for (int j = 0; j < values.Count; ++j)
                            {
                                //依次把三个参数放入字典中
                                parameters2[keys[j]] = new List<object> { types[j], values[j] };
                            }
                            returnVal2 = Database.Insert(parameters2, comInsert);

                            if (!returnVal2)
                                throw new Exception("Error occur when create the order form.");

                            toRemove.Add(item);


                        }
                        catch (FormatException)
                        {
                            MessageBox.Show("请输入整数。");
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Error: " + ex.Message);
                        }
                    }
                }
            }
            finally
            {
                _info.Clear();
                foreach (MyItem i in toRemove)
                {
                    _data.Remove(i);
                }
            }
        }

        private void OrderCancel_Click(object sender, RoutedEventArgs e)
        {
            return;
        }
    }
}
