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
    /// QuotationForm.xaml 的交互逻辑
    /// </summary>
    public partial class QuotationForm : Window
    {
        private BackgroundWorker _demoBGWorker = new BackgroundWorker();
        private ObservableCollection<MyItem> _data { get; set; }
        private ObservableCollection<Info> _info { get; set; }
        public List<MyItem> _cache { get; set; }

        /// <summary>
        /// 展示RFQ_form绑定的数据模型
        /// </summary>
        public class MyItem
        {
            public MyItem(int nO, DateTime date, string notes, bool isSelected, int order_id)
            {
                NO = nO;
                Date = date;
                Notes = notes;
                this.isSelected = isSelected;
                Order_id = order_id;
            }

            public int NO { get; set; }
            public DateTime Date { get; set; }
            public string Notes { get; set; }
            public bool isSelected { get; set; }
            public int Order_id { get; set; }
        }

        /// <summary>
        /// 展示RFQ_information绑定的数据模型
        /// </summary>
        public class Info
        {
            public Info(string category, string name, string modle, int num, decimal price, int nO)
            {
                Category = category;
                Name = name;
                Modle = modle;
                Num = num;
                Price = price;
                NO = nO;
            }

            public string Category { get; set; }
            public string Name { get; set; }
            public string Modle { get; set; }
            public int Num { get; set; }
            public decimal Price { get; set; }
            public int NO { get; set; }

        }

        private void refresh_data()
        {
            _data.Clear();
            foreach (var i in _cache)
                _data.Add(i);
            QuotationDataGrid.Items.Refresh();
        }

        public QuotationForm()
        {
            _demoBGWorker.DoWork += BGWorker_DoWork;
            _demoBGWorker.RunWorkerCompleted += BGWorker_RunWorkerCompleted;
            _demoBGWorker.RunWorkerAsync();

            InitializeComponent();

            //初始化combobox里面的选项
            string command = "SELECT DISTINCT Product_category FROM " +
                "RFQ_information WHERE RFQ_id_FK in  (SELECT RFQ_id_PK FROM RFQ WHERE RFQ_status='等待')";
            DataTable _dataTable = Database.FillDataTable(command);
            if (_dataTable != null)
            {
                //加载到combobox
                QuotationCategoryCombobox.Items.Clear();
                //CreateAccount_BatchSchoolCombobox.Items.Add(_comboboxDefault);
                for (int i = 0; i < _dataTable.Rows.Count; i++)
                {
                    QuotationCategoryCombobox.Items.Add(_dataTable.Rows[i]["Product_category"]);
                }
            }

            //设置表头标题
            for (int i = 0; i < ConstantValue.Quotation_informationColName.Length; ++i)
            {
                QuotationInfoDataGrid.Columns[i].Header = ConstantValue.Quotation_informationColName[i];
            }

            for (int i = 0; i < ConstantValue.Quotation_formColName.Length; ++i)
            {
                QuotationDataGrid.Columns[i].Header = ConstantValue.Quotation_formColName[i];
            }
        }

        private void BGWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            _cache = new List<MyItem>();//因为BGW不支持多线程 所以只能放这儿 也不太懂为什么
            _info = new ObservableCollection<Info>();
            _data = new ObservableCollection<MyItem>();
            try
            {
                //耗时操作
                //查询等待处理的询价单
                DataTable dataTable = Database.FillDataTable("SELECT * FROM RFQ WHERE RFQ_status='等待'");
                foreach (DataRow row in dataTable.Rows)
                {
                    MyItem tempItem = new MyItem((int)row["RFQ_id_PK"], (DateTime)row["RFQ_createdate"],
                        (string)row["RFQ_notes"], false, (int)row["Order_id_FK"]);
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
        /// 在选中询价单时在下面的DataGrid显示对应的询价详单
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
                    "SELECT Product_category, Product_name, Product_modle, Num_of_product,Order_information_form_id_PK,RFQ_id_PK " +
                    "FROM Order_information INNER JOIN RFQ ON " +
                    "Order_information.Order_form_id_FK=RFQ.Order_id_FK WHERE RFQ_id_PK=" + info.NO);
                foreach (DataRow row in dataTable.Rows)
                {
                    Info tempItem = new Info((string)row["Product_category"], (string)row["Product_name"],
                        (string)row["Product_modle"], (int)row["Num_of_product"], -1m, (int)row["Order_information_form_id_PK"]);
                    _info.Add(tempItem);
                }
            }
            catch (Exception ex)
            {
                Tools.ShowMessageBox(ex.Message);
            }
        }


        /// <summary>
        /// 更新询价单的状态
        /// </summary>
        private void ChangeState()
        {
            //因为直接remove我们要遍历的List的话会影响遍历，
            //所以先把要移除的项记录下来
            List<MyItem> toRemove = new List<MyItem>();
            try
            {
                MyItem item;
                foreach (var i in QuotationDataGrid.Items)
                {
                    item = i as MyItem;
                    if (item.isSelected)
                    {
                        //应该要检查选中的询价单是不是对应的填写了价格的单子
                        //（只不过现在默认为价格-1，不填的话数据库会报错）
                        //检查是否填写了价格
                        //TODO:这里能不能用_info来看
                        foreach(var info in QuotationInfoDataGrid.Items)
                        {
                            if (((Info)info).Price <= 0)
                                throw new Exception("请填写价格。");
                        }

                        //创建报价单
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
                                tableName + "(Quotation_id_FK, Order_information_form_id_FK, Price)" +
                                "values(@ID, @ORDERINFO, @PRICE)";
                            SqlDbType[] itypes = { SqlDbType.Int, SqlDbType.Int, SqlDbType.Money };//数据类型
                            string[] ikeys = { "@ID", "@ORDERINFO", "@PRICE" };//上面写的参数名

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
                        toRemove.Add(item);
                    }
                }
                MessageBox.Show("操作成功！");

                _info.Clear();
                foreach (MyItem i in toRemove)
                {
                    _cache.Remove(i);
                }
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

        private void FilterCancelButton_Click(object sender, RoutedEventArgs e)
        {
            QuotationCategoryCombobox.SelectedIndex = -1;
            refresh_data();
        }

        private void FilterButton_Click(object sender, RoutedEventArgs e)
            //这里没法筛选……那个RFQ里面没有Category信息
        {
            //try
            //{
            //    if (QuotationCategoryCombobox.SelectedIndex == -1)
            //        return;
            //    _data.Clear();
            //    foreach (var item in _cache)
            //    {
            //        if (item. != OrderAuditSchoolCombobox.SelectedValue.ToString().Trim())

            //            _data.Add(item);//这里_data里面的item虽然指向的对象和_cache中的是同一个，但是对程序应该没有影响
            //    }
            //    QuotationDataGrid.Items.Refresh();
            //    if (_data.Count == 0)
            //        MessageBox.Show("无符合要求的表单。");
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show("Error(s) occur when filtering: " + ex.Message);
            //}
        }

        private void Quotation_CreatButton_Click(object sender, RoutedEventArgs e)
        {
            ChangeState();
        }
    }
}
