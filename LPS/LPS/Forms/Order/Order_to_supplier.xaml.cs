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
    /// Order_to_supplier.xaml 的交互逻辑
    /// </summary>
    public partial class Order_to_supplier : Window
    {
        private BackgroundWorker _demoBGWorker = new BackgroundWorker();
        private ObservableCollection<MyItem> _data { get; set; }
        private ObservableCollection<Info> _info { get; set; }
        public Order_to_supplier()
        {
            _demoBGWorker.DoWork += BGWorker_DoWork;
            _demoBGWorker.RunWorkerCompleted += BGWorker_RunWorkerCompleted;
            _demoBGWorker.RunWorkerAsync();
            InitializeComponent();
            for (int i = 0; i < ConstantValue.Order_formColName.Length; ++i)
            {
                QuotationDataGrid.Columns[i].Header = ConstantValue.Sale_formColName[i];
            }
            for (int i = 0; i < ConstantValue.Order_informationColName.Length; ++i)
            {
                QuotationInfoDataGrid.Columns[i].Header = ConstantValue.Sale_informationColName[i];
            }
        }

        /// <summary>
        /// 展示Order_form绑定的数据模型
        /// </summary>
        private class MyItem
        {
            public int NO { get; set; }
            public string QS { get; set; }
            public string SID { get; set; }
            public string Notes { get; set; }
            public bool isSelected { get; set; }

            public MyItem(int nO, string qs, string sid, string notes)
            {
                NO = nO;
                QS = qs;
                SID = sid;
                Notes = notes;
                isSelected = false;
            }

            public void Set(int nO, string qs, string sid, string notes)
            {
                NO = nO;
                QS = qs;
                SID = sid;
                Notes = notes;
            }
        }

        /// <summary>
        /// 展示Order_information绑定的数据模型
        /// </summary>
        private class Info
        {
            public string Category { get; set; }
            public string Name { get; set; }
            public string Modle { get; set; }
            public int Num { get; set; }
            public double Price { get; set; }

            public Info(string category, string name, string modle, int num,double price)
            {
                Category = category;
                Name = name;
                Modle = modle;
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

                //从进货通知中获取待进货的信息
                int order_form_id = (int)Database.Query("SELECT Order_form_id_FK FROM Purchase_notice WHERE Purchase_notice_status='待进货'");
                //获取到Order_form_id_FK报价单编号
                string command = string.Format("SELECT * FROM Quotation WHERE Quotation_id_FK={0}", order_form_id);
                //获取报价单和报价单单号
                DataTable quotationTable = Database.FillDataTable("Quotation", command);
                int quotation_id = (int)Database.Query("SELECT Quotation_id_FK FROM Quotation");
                //耗时操作
                foreach (DataRow row in quotationTable.Rows)
                {
                    MyItem tempItem = new MyItem((int)row["Quotation_id_PK"], (string)row["Quotation_source"],(string)row["Supplier_id_FK"], ((string)row["Quotation_notes"]).Trim());
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
            QuotationDataGrid.ItemsSource = _data;//因为BGW不支持多线程 所以只能放这儿 也不太懂为什么
            QuotationInfoDataGrid.ItemsSource = _info;
            //计算过程中的异常会被抓住，在这里可以进行处理。
            if (e.Error != null)
            {
                Type errorType = e.Error.GetType();
                MessageBox.Show("Error(s) occur when initialize the datagrid:" + e.Error.Message);
            }
        }

        /// <summary>
        /// 在选中Quotation时在下面的DataGrid显示对应的订购详单
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
                //获取报价详细单
                string command = string.Format("SELECT * FROM Quotation_information WHERE Quotation_id_FK={0}",info.NO);
                DataTable quotationInfoTable = Database.FillDataTable("Quation_information", command);
                foreach (DataRow row in quotationInfoTable.Rows)
                {
                    Info tempItem = new Info((string)row["Product_category"], (string)row["Product_name"], (string)row["Product_modle"], (int)row["Num_of_product"],(double)row["Price_of_product"]);
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
            Info info;
            List<object> values = new List<object>();//用来临时存参数的
            Dictionary<string, List<Object>> parameters1 = new Dictionary<string, List<object>>();
            Dictionary<string, List<Object>> parameters2 = new Dictionary<string, List<object>>();
            List<MyItem> toRemove = new List<MyItem>();
            try
            {
                foreach (var i in QuotationDataGrid.Items)
                {
                    MyItem item;
                    item = i as MyItem;
                    if (item.isSelected)
                    {//获取报价详细单
                        string com = string.Format("SELECT * FROM Quotation_information WHERE Quotation_id_FK={0}", item.NO);
                        DataTable quotationInfoTable = Database.FillDataTable("Quation_information", com);
                        Info tempItem = null;
                        foreach (DataRow row in quotationInfoTable.Rows)
                        {
                            tempItem = new Info((string)row["Product_category"], (string)row["Product_name"], (string)row["Product_modle"], (int)row["Num_of_product"], (double)row["Price_of_product"]);
                            _info.Add(tempItem);
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
                            item.Notes = OrderToSupplierNotes.Text;
                            values.Add(item.Notes);
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
                            values.Add(tempItem.Category);
                            values.Add(tempItem.Name);
                            values.Add(tempItem.Modle);
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


                            //MessageBox.Show("添加成功！");
                            //clearInputField();

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
