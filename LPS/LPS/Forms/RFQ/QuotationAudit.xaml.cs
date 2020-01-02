using LPS.Manager;
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

namespace LPS.Forms.RFQ
{
    /// <summary>
    /// QuotationAudit.xaml 的交互逻辑
    /// </summary>
    public partial class QuotationAudit : Window
    {
        private BackgroundWorker _demoBGWorker = new BackgroundWorker();
        private ObservableCollection<RFQItem> _data { get; set; }
        private ObservableCollection<QuoItem> _quo { get; set; }
        private ObservableCollection<Info> _info { get; set; }

        public List<RFQItem> _cache { get; set; }

        /// <summary>
        /// 展示RFQ绑定的数据模型
        /// </summary>
        public class RFQItem
        {
            public RFQItem(int nO, DateTime date, string notes, int orderID)
            {
                NO = nO;
                Date = date;
                Notes = notes;
                OrderID = orderID;
            }

            public int NO { get; set; }
            public DateTime Date { get; set; }
            public string Notes { get; set; }
            public int OrderID { get; set; }
        }

        /// <summary>
        /// 展示Quotation绑定的数据模型
        /// </summary>
        public class QuoItem
        {
            public QuoItem(int nO, string source, int supplier, DateTime date, string notes, bool isSelected, int rFQID)
            {
                NO = nO;
                Source = source;
                Supplier = supplier;
                Date = date;
                Notes = notes;
                this.isSelected = isSelected;
                RFQID = rFQID;
            }

            public int NO { get; set; }
            public string Source { get; set; }
            public int Supplier { get; set; }
            public DateTime Date { get; set; }
            public string Notes { get; set; }
            public bool isSelected { get; set; }
            public int RFQID { get; set; }

        }

        /// <summary>
        /// 展示OrderInfo绑定的数据模型
        /// </summary>
        public class Info
        {
            public Info(string category, string name, string modle, int num, decimal price)
            {
                Category = category;
                Name = name;
                Modle = modle;
                Num = num;
                Price = price;
            }

            public string Category { get; set; }
            public string Name { get; set; }
            public string Modle { get; set; }
            public int Num { get; set; }
            public decimal Price { get; set; }
        }

        private void refresh_data()
        {
            _data.Clear();
            foreach (var i in _cache)
                _data.Add(i);
            if(QuotationDataGrid!=null)
            QuotationDataGrid.Items.Refresh();
        }

        public QuotationAudit()
        {
            _demoBGWorker.DoWork += BGWorker_DoWork;
            _demoBGWorker.RunWorkerCompleted += BGWorker_RunWorkerCompleted;
            _demoBGWorker.RunWorkerAsync();

            InitializeComponent();

            //设置表头标题
            for (int i = 0; i < ConstantValue.Quotation_informationColName.Length; ++i)
            {
                QuotationInfoDataGrid.Columns[i].Header = ConstantValue.Quotation_informationColName[i];
            }

            for (int i = 0; i < ConstantValue.QuotationAudit_formColName.Length; ++i)
            {
                QuotationDataGrid.Columns[i].Header = ConstantValue.QuotationAudit_formColName[i];
            }

            for (int i = 0; i < ConstantValue.RFQColName.Length; ++i)
            {
                RFQDataGrid.Columns[i].Header = ConstantValue.RFQColName[i];
            }
        }


        private void BGWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            _cache = new List<RFQItem>();//因为BGW不支持多线程 所以只能放这儿 也不太懂为什么
            _quo = new ObservableCollection<QuoItem>();
            _data = new ObservableCollection<RFQItem>();
            _info = new ObservableCollection<Info>();
            try
            {
                //耗时操作
                //查询等待处理的询价单
                DataTable dataTable = Database.FillDataTable("SELECT * FROM RFQ WHERE RFQ_status='询价中'");
                foreach (DataRow row in dataTable.Rows)
                {
                    RFQItem tempItem = new RFQItem((int)row["RFQ_id_PK"], (DateTime)row["RFQ_createdate"],
                        (string)row["RFQ_notes"], (int)row["Order_form_id_FK"]);
                    _cache.Add(tempItem);
                }
                //_cache.OrderByDescending<MyItem>
                refresh_data();
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
            RFQDataGrid.ItemsSource = _data;//因为BGW不支持多线程 所以只能放这儿 也不太懂为什么
            QuotationDataGrid.ItemsSource = _quo;
            QuotationInfoDataGrid.ItemsSource = _info;
            //计算过程中的异常会被抓住，在这里可以进行处理。
            if (e.Error != null)
            {
                Type errorType = e.Error.GetType();
                MessageBox.Show("Error(s) occur when initialize the datagrid:" + e.Error.Message);
            }
        }

        /// <summary>
        /// 在选中询价单时在右边的DataGrid显示对应的报价单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DataGridRow_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                //先清空之前的信息
                _quo.Clear();

                //获取选中的数据行
                RFQItem info = null;
                if (sender != null)
                {
                    DataGridRow grid = sender as DataGridRow;
                    info = grid.Item as RFQItem;
                }
                DataTable dataTable = Database.FillDataTable(
"SELECT Quotation_id_PK, Quotation_source, Supplier_id_FK, Quotation_createdate,Quotation_notes " +
"FROM Quotation WHERE RFQ_id_FK=" + info.NO);
                foreach (DataRow row in dataTable.Rows)
                {
                    QuoItem tempItem = new QuoItem((int)row["Quotation_id_PK"], (string)row["Quotation_source"],
                        (int)row["Supplier_id_FK"], (DateTime)row["Quotation_createdate"],
                        (string)row["Quotation_notes"], false, info.NO);
                    _quo.Add(tempItem);
                }
            }
            catch (Exception ex)
            {
                Tools.ShowMessageBox(ex.Message);
            }
        }

        /// <summary>
        /// 在选中报价单时在下面的DataGrid显示对应的具体报价
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void QuoDataGridRow_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

            //先清空之前的信息
            _info.Clear();

            //获取选中的数据行
            QuoItem info = null;
            if (sender != null)
            {
                DataGridRow grid = sender as DataGridRow;
                info = grid.Item as QuoItem;
            }
            FillInfo(info);
        }

        public void FillInfo(QuoItem info)
        {
            try
            {
                DataTable dataTable = Database.FillDataTable(
       "SELECT Product_category, Product_name, " +
       "Product_modle, Num_of_product, Price FROM " +
       "Order_information INNER JOIN Quotation_information " +
       "ON Order_information.Order_information_form_id_PK=Quotation_information.Order_information_form_id_FK" +
       " WHERE Quotation_information.Quotation_id_FK=" + info.NO);
                foreach (DataRow row in dataTable.Rows)
                {
                    Info tempItem = new Info((string)row["Product_category"], (string)row["Product_name"],
                        (string)row["Product_modle"], (int)row["Num_of_product"],
                        (decimal)row["Price"]);
                    _info.Add(tempItem);
                }
            }
            catch (Exception ex)
            {
                Tools.ShowMessageBox(ex.Message);
            }
        }

        private void Quotation_CreatButton_Click(object sender, RoutedEventArgs e)
        {
            //
            CreateSaleAndUpdate();
        }

        private void FilterCancelButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void FilterButton_Click(object sender, RoutedEventArgs e)
        {

        }


        private void CreateSaleAndUpdate()
        {

            // List<MyItem> toRemove = new List<MyItem>();
            try
            {
                Functions.InsertGoodsInOrderToInventory();

                QuoItem item;
                RFQItem toRemove = null;
                foreach (var i in QuotationDataGrid.Items)
                {
                    item = i as QuoItem;
                    if (item.isSelected)
                    {
                        //更新RFQ的状态
                        Database.ExecuteSqlCommand("UPDATE RFQ SET RFQ_status='询价完成' WHERE RFQ_id_PK=" + item.RFQID);

                        if (_info == null || _info.Count <= 0)
                        {
                            FillInfo(item);
                        }

                        //更新Order_form的状态
                        int orderid = -1;
                        foreach (var or in _data)
                        {
                            if (or.NO == item.RFQID)
                            {
                                toRemove = or;
                                orderid = or.OrderID;
                                break;
                            }
                        }
                        Database.ExecuteSqlCommand("UPDATE Order_form SET Order_form_status='进入销售' " +
                            "WHERE Order_form_id_PK=" + orderid);

                        //计算商品总价
                        DataTable orderinfo = Database.FillDataTable("SELECT " +
                            "Product_id_PK, Num_of_product, Price " +
                            "FROM (Order_information INNER JOIN Quotation_information ON " +
                            "Order_information.Order_Information_form_id_PK=Quotation_information.Order_information_form_id_FK) " +
                            "INNER JOIN Product_information ON " +
                            "Order_information.Product_category=Product_information.Product_category AND " +
                            "Order_information.Product_name=Product_information.Product_name AND " +
                            "Order_information.Product_modle=Product_information.Product_modle " +
                            " WHERE Quotation_id_FK=" + item.NO);
                        decimal Total = 0m;
                        foreach (DataRow oir in orderinfo.Rows)
                        {
                            Total += (decimal)oir["Price"] * (int)oir["Num_of_product"];
                        }

                        //创建销售单
                        string Notes = "";
                        if (!String.IsNullOrEmpty(QuotationNotesTextBox.Text))
                            Notes = QuotationNotesTextBox.Text;
                        string tableName = "Sales_batch";
                        string comInsert = "INSERT INTO " +
                            tableName + "(Customer_id_FK, Order_form_id_FK, Source_of_goods, Admin_id_FK, Price_of_all, " +
                            "createdate, Sales_batch_status, Sales_batch_notes,Supplier_id_FK)" +
                            "values(@CUSTOMER, @ORDER, @SOURCE, @ADMIN, @PRICE, @DATE, @STATUS, @NOTES,@SUPPLIER)";
                        SqlDbType[] types = { SqlDbType.Int, SqlDbType.Int, SqlDbType.VarChar, SqlDbType.Int, SqlDbType.Money,
                                    SqlDbType.DateTime, SqlDbType.VarChar, SqlDbType.VarChar,SqlDbType.Int,};//数据类型
                        string[] keys = { "@CUSTOMER", "@ORDER", "@SOURCE", "@ADMIN", "@PRICE",
                                    "@DATE", "@STATUS", "@NOTES","@SUPPLIER" };//上面写的参数名

                        List<object> values = new List<object>();//用来临时存参数的
                        Dictionary<string, List<object>> parameters = new Dictionary<string, List<object>>();//用来传参的
                        bool returnVal = false;//判断是否成功执行
                        int cid = (int)Database.Query("SELECT Customer_id_FK FROM Order_form WHERE Order_form_id_PK=" + orderid);
                        values.Add(cid);
                        values.Add(orderid);
                        values.Add("供应商");
                        values.Add(Database.UNO);
                        values.Add(Total);
                        values.Add(System.DateTime.Now);
                        values.Add("未付款");
                        values.Add(Notes);
                        values.Add(item.Supplier);

                        for (int j = 0; j < values.Count; ++j)
                        {
                            //依次把参数放入字典中
                            parameters[keys[j]] = new List<object> { types[j], values[j] };
                        }
                        returnVal = Database.Insert(parameters, comInsert);
                        if (!returnVal)
                            throw new Exception("Error occur when inserting the Sales form.");

                        //创建销售详单
                        int salesID = (int)Database.Query(string.Format("SELECT MAX(Sales_batch_id_PK) from {0}",
     tableName));
                        tableName = "Sales_order";
                        foreach (DataRow oir in orderinfo.Rows)
                        {
                            values.Clear();
                            comInsert = "INSERT INTO " +
                                tableName + "(Sales_batch_id_FK, Product_id_FK, Num, Price_of_product)" +
                                "values(@SALE, @PID, @NUM, @PRICE)";
                            SqlDbType[] itypes = { SqlDbType.Int, SqlDbType.Int, SqlDbType.Int, SqlDbType.Money };//数据类型
                            string[] ikeys = { "@SALE", "@PID", "@NUM", "@PRICE" };//上面写的参数名

                            Dictionary<string, List<object>> iparameters = new Dictionary<string, List<object>>();//用来传参的
                            returnVal = false;//判断是否成功执行

                            values.Add(salesID);
                            values.Add((int)oir["Product_id_PK"]);
                            values.Add((int)oir["Num_of_product"]);
                            values.Add((decimal)oir["Price"]);

                            for (int j = 0; j < values.Count; ++j)
                            {
                                //依次把参数放入字典中
                                iparameters[ikeys[j]] = new List<object> { itypes[j], values[j] };
                            }
                            returnVal = Database.Insert(iparameters, comInsert);
                            if (!returnVal)
                                throw new Exception(
                                    "Error occur when inserting the Sales_order form. ProductID:" +
                                    (int)oir["Product_id_PK"] + "in Sales:" + salesID);
                        }
                        MessageBox.Show("操作成功！");
                        break;//只取第一个打勾的报价单
                    }
                }
                _info.Clear();
                _quo.Clear();
                _cache.Remove(toRemove);

                refresh_data();
            }
            catch (Exception ex)
            {
                Tools.ShowMessageBox(ex.Message);
            }
            finally
            {

            }
        }
    }
}

/*
//更新Order_information的状态
foreach(var it in _info)
{
    Database.ExecuteSqlCommand(String.Format("UPDATE Order_information SET Order_information_Status=3" +
" WHERE Order_information_Status=2 AND Product_category='{0}' AND Product_name='{1}' AND Product_modle='{2}'" +
" AND Num_of_product='{3}'", it.Category, it.Name, it.Modle, it.Num));
}*/

/*
//更新Order_form的状态
DataTable orderdt = Database.FillDataTable("SELECT Order_form_id_PK FROM Order_form " +
"WHERE Order_form_status='审核通过'");
DataTable oinfo;
foreach (DataRow or in orderdt.Rows)
{
oinfo = Database.FillDataTable("SELECT Order_information_Status FROM Order_information " +
    "WHERE Order_form_id_FK=" + (int)or["Order_form_id_PK"]);
bool shouldUpdate = true;
foreach(DataRow ir in oinfo.Rows)
{
    if ((int)ir["Order_information_Status"] != 3)
    {
        shouldUpdate = false;
        break;
    }
}
if (shouldUpdate)
{
    Database.ExecuteSqlCommand("UPDATE Order_form SET Order_form_status='进入销售' " +
        "WHERE Order_information_form_id_PK=" + (int)or["Order_form_id_PK"]);
}
}*/

/*
                          string Notes = "";
                      if (QuotationNotesTextBox.Text != null)
                          Notes = QuotationNotesTextBox.Text.Trim();
                      string tableName = "Quotation";
                      string comInsert = "INSERT INTO " +
                          tableName + "(Quotation_source, Supplier_id_FK, Quotation_createdate, Quotation_notes, RFQ_id_FK)" +
                          "values(@SOURCE, @UID, @DATE, @NOTES, @RFQ)";
                      SqlDbType[] types = { SqlDbType.VarChar, SqlDbType.Int, SqlDbType.DateTime,
                              SqlDbType.VarChar, SqlDbType.Int };//数据类型
                      string[] keys = { "@SOURCE", "@UID", "@DATE", "@NOTES", "@RFQ" };//上面写的参数名

                      List<object> values = new List<object>();//用来临时存参数的
                      Dictionary<string, List<object>> parameters = new Dictionary<string, List<object>>();//用来传参的
                      bool returnVal = false;//判断是否成功执行

                      values.Add("供货商");
                      values.Add(Database.UNO);
                      values.Add(System.DateTime.Now);
                      values.Add(Notes);
                      values.Add(item.NO);

                      for (int j = 0; j < values.Count; ++j)
                      {
                          //依次把参数放入字典中
                          parameters[keys[j]] = new List<object> { types[j], values[j] };
                      }
                      returnVal = Database.Insert(parameters, comInsert);
                      if (!returnVal)
                          throw new Exception("Error occur when inserting the Quotation form.");

                      //获取这条记录在Quotation的编号
                      int quoID = (int)Database.Query(string.Format("SELECT MAX(Quotation_id_PK) from {0}",
                          tableName));

                      Info tempi;
                      //插入报价细单
                      foreach (var info in QuotationInfoDataGrid.Items)
                      {
                          tempi = (Info)info;
                          values.Clear();
                          tableName = "Quotation_information";
                          comInsert = "INSERT INTO " +
                              tableName + "(Quotation_id_FK, RFQ_information_id_FK, Price_of_product)" +
                              "values(@ID, @RFQ, @PRICE)";
                          SqlDbType[] itypes = { SqlDbType.Int, SqlDbType.Int, SqlDbType.Money };//数据类型
                          string[] ikeys = { "@ID", "@RFQ", "@PRICE" };//上面写的参数名

                          Dictionary<string, List<object>> iparameters = new Dictionary<string, List<object>>();//用来传参的
                          returnVal = false;//判断是否成功执行

                          values.Add(quoID);
                          values.Add(tempi.NO);
                          values.Add(tempi.Price);

                          for (int j = 0; j < values.Count; ++j)
                          {
                              //依次把参数放入字典中
                              iparameters[ikeys[j]] = new List<object> { itypes[j], values[j] };
                          }
                          returnVal = Database.Insert(iparameters, comInsert);
                          if (!returnVal)
                              throw new Exception("Error occur when inserting the Quotation_information form.");
                      }
                      toRemove.Add(item);*/
