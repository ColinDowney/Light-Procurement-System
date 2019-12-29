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
using LPS.Utility;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;

namespace LPS.Forms.DataView
{
    /// <summary>
    /// PickUp.xaml 的交互逻辑
    /// </summary>
    public partial class PickUp : Window
    {
        private BackgroundWorker _demoBGWorker = new BackgroundWorker();
        private ObservableCollection<MyItem> _data { get; set; }
        private ObservableCollection<Info> _info { get; set; }
        public PickUp()
        {
            _demoBGWorker.DoWork += BGWorker_DoWork;
            _demoBGWorker.RunWorkerCompleted += BGWorker_RunWorkerCompleted;
            _demoBGWorker.RunWorkerAsync();

            InitializeComponent();
            //设置表头标题
            for (int i = 0; i < ConstantValue.Order_formColName.Length; ++i)
            {
                PickUpDataGrid.Columns[i].Header = ConstantValue.Sale_formColName[i];
            }
            for (int i = 0; i < ConstantValue.Order_informationColName.Length; ++i)
            {
                PickUpInfoDataGrid.Columns[i].Header = ConstantValue.Sale_informationColName[i];
            }

        }

        /// <summary>
        /// 展示Sale_form绑定的数据模型
        /// </summary>
        private class MyItem
        {
            public int Sale_NO { get; set; }
            public string UID { get; set; }
            public string Purchase_NO { get; set; }
            public string Product_Source { get; set; }
            public string MID { get; set; }
            public double Total_Price { get; set; }
            public string Date { get; set; }
            public string Sale_State { get; set; }
            public string Sale_Note { get; set; }
            public bool isSelected { get; set; }


            public MyItem(int nO, string uID, string purchase_NO, string product_Source, 
                string mID,double total_Price,string date,string sale_State,string sale_Note)
            {
                Sale_NO = nO;
                UID = uID;
                Purchase_NO = purchase_NO;
                Product_Source = product_Source;
                MID = mID;
                Total_Price = total_Price;
                Date = date;
                Sale_State = sale_State;
                Sale_Note = sale_Note;
                isSelected = false;
            }

            public void Set(int nO, string uID, string purchase_NO, string product_Source,
                string mID, double total_Price, string date, string sale_State, string sale_Note)
            {
                Sale_NO = nO;
                UID = uID;
                Purchase_NO = purchase_NO;
                Product_Source = product_Source;
                MID = mID;
                Total_Price = total_Price;
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
            public string Sale_info_No { get; set; }
            public string Product_No { get; set; }
            public int Num { get; set; }
            public double Price { get; set; }

            public Info(string sale_info_No,string product_No, int num,double price)
            {
                Sale_info_No = sale_info_No;
                Product_No = product_No;
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
                DataTable dataTable = Database.FillDataTable("Sales_batch", "SELECT * FROM Sales_batch WHERE Sales_batch_status='待取货'");
                foreach (DataRow row in dataTable.Rows)
                {
                    //查询发起订单的用户信息
                    MyItem tempItem = new MyItem((int)row["Sales_batch_id_PK"], (string)row["Customer_id_FK"], ((string)row["Order_form_id_FK"]).Trim(),
                        ((string)row["Source_of_goods"]).Trim(), ((string)row["Admin_id_FK"]).Trim(), (double)row["Price_of_all"], ((string)row["createdate"]).Trim(), ((string)row["Sales_batch_status"]).Trim(), ((string)row["Sales_batch_notes"]).Trim());
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
            PickUpDataGrid.ItemsSource = _data;//因为BGW不支持多线程 所以只能放这儿 也不太懂为什么
            PickUpInfoDataGrid.ItemsSource = _info;
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

                DataTable dataTable = Database.FillDataTable("Sales_order",
                    "SELECT Product_id_FK, Price_of_product, Num_of_product FROM Sales_order WHERE Sales_batch_id_FK=" + info.Sale_NO);
                foreach (DataRow row in dataTable.Rows)
                {
                    Info tempItem = new Info((string)row["Sales_order_id_PK"], (string)row["Product_id_FK"], (int)row["Num_of_product"], (double)row["Price_of_product"]);
                    _info.Add(tempItem);
                }
            }
            catch (Exception ex)
            {
                Tools.ShowMessageBox(ex.Message);
            }
        }
        

        /// <summary>
        /// 更新销售单的状态
        /// </summary>
        /// <param name="state"></param>
        private void ChangeState(string state)
        {
            //因为直接remove我们要遍历的List的话会影响遍历，
            //所以先把要移除的项记录下来
            List<MyItem> toRemove = new List<MyItem>();
            try
            {
                MyItem item;
                foreach (var i in PickUpDataGrid.Items)
                {
                    item = i as MyItem;
                    if (item.isSelected)
                    {
                        string command = string.Format("UPDATE Sales_batch SET Sales_batch_status='{0}' WHERE Sales_batch_id_PK={1}", state, item.Sale_NO);
                        int re = Database.ExecuteSqlCommand(command);
                        if (re <= 0)
                            throw new Exception("Error occur when updating '" + item.Sale_NO + "'.");
                        toRemove.Add(item);
                    }
                }
                MessageBox.Show("操作成功！");
            }
            catch (Exception ex)
            {
                Tools.ShowMessageBox(ex.Message);
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
        private void ACK_Click(object sender, RoutedEventArgs e)
        {

            //获取选中行数据
            List<object> values = new List<object>();//用来临时存参数的
            Dictionary<string, List<Object>> parameters1 = new Dictionary<string, List<object>>();
            Dictionary<string, List<Object>> parameters2 = new Dictionary<string, List<object>>();
            List<MyItem> toRemove = new List<MyItem>();
            //查询之前建立的订购批次单的编号
            string tableName = "Pick_up_order";
            string command = string.Format("SELECT MAX(Pick_up_order_id_PK) from {0}", tableName);
            int pickupID = (int)Database.Query(command);
            try
            {
                foreach (var i in PickUpDataGrid.Items)
                {
                    MyItem item;
                    item = i as MyItem;
                    if (item.isSelected)
                    {
                        try
                        {
                            string comInsert = "INSERT INTO" + tableName + "(Pick_up_order_id_PK,Customer_id_FK,Pick_up_order_createdate)" +
                                "value(@ID,@SID,@DATE)";
                            SqlDbType[] types = { SqlDbType.Int, SqlDbType.Int,SqlDbType.DateTime };//数据类型
                            string[] keys = { "@ID", "@SID","@DATE"};//上面写的参数名

                            values.Add(pickupID);
                            values.Add(item.UID);
                            DateTime date = new DateTime();
                            values.Add(date);
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
                        tableName = "Pick_up_order_information";
                        string com = string.Format("SELECT MAX(Pick_up_order_informatin_id_PK) from {0}", tableName);
                        int pickupInfoID = (int)Database.Query(com);
                        com = string.Format("SELECT Product_id_FK FROM Sales_order WHERE Sales_batch_id_FK={0}", item.Sale_NO);
                        int pid = (int)Database.Query(com);
                        com = string.Format("SELECT Num_of_product FROM Sales_order WHERE Sales_batch_id_FK={0}", item.Sale_NO);
                        int np = (int)Database.Query(com);
                        com = string.Format("SELECT Product_category from Product_information WHERE Product_id_FK={0}",pid);
                        char pc = (char)Database.Query(com);
                        com = string.Format("SELECT Product_name from Product_information WHERE Product_id_FK={0}", pid);
                        char pn = (char)Database.Query(com);
                        com = string.Format("SELECT Product_modle from Product_information WHERE Product_id_FK={0}", pid);
                        char pm = (char)Database.Query(com);
                        values.Clear();
                        try
                        {
                            string comInsert = "INSERT INTO" + tableName + "(Pick_up_order_informatin_id_PK,Pick_up_order_id_FK,Product_id_FK," +
                                "Product_category,Product_name,Product_modle,Num_of_product)" +
                                "value(@INFOID,@ID,@PID,@PC,@PN,@PM,@NP)";
                            SqlDbType[] types = { SqlDbType.Int, SqlDbType.Int, SqlDbType.Char, SqlDbType.Char, SqlDbType.Char,SqlDbType.Char, SqlDbType.Int};//数据类型
                            string[] keys = { "@INFOID", "@ID", "@PID","@PC", "@PN", "@PM", "@NP" };//上面写的参数名
                            values.Add(pickupInfoID);
                            values.Add(pickupID);
                            values.Add(pid);
                            values.Add(pn);
                            values.Add(pm);
                            values.Add(np);
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
            ChangeState("完成");

        }

        private void PickUpCancel_Click(object sender, RoutedEventArgs e)
        {
            return;
        }
    }
}
