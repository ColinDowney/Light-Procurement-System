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
using static LPS.Manager.Functions;

namespace LPS
{

    /// <summary>
    /// OrderAudit.xaml 的交互逻辑
    /// </summary>
    public partial class OrderView : Window
    {
        private BackgroundWorker _demoBGWorker = new BackgroundWorker();
        private ObservableCollection<MyItem> _data { get; set; }
        private ObservableCollection<Info> _info { get; set; }
        public List<MyItem> _cache { get; set; }

        /// <summary>
        /// 展示Order_form绑定的数据模型
        /// </summary>
        public class MyItem
        {
            public MyItem(int nO, int uID, decimal price, string date, string notes)
            {
                NO = nO;
                UID = uID;
                Price = price;
                Date = date;
                Notes = notes;
            }

            public int NO { get; set; }
            public int UID { get; set; }
            public decimal Price { get; set; }
            public string Date { get; set; }
            public string Notes { get; set; }

        }

        /// <summary>
        /// 展示Order_information绑定的数据模型
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
            AuditDataGrid.Items.Refresh();
        }

        public OrderView()
        {
            _demoBGWorker.DoWork += BGWorker_DoWork;
            _demoBGWorker.RunWorkerCompleted += BGWorker_RunWorkerCompleted;
            _demoBGWorker.RunWorkerAsync();


            InitializeComponent();

            //设置表头标题
            for (int i = 0; i < ConstantValue.OrderViewColName.Length; ++i)
            {
                AuditInfoDataGrid.Columns[i].Header = ConstantValue.OrderViewColName[i];
            }

            for (int i = 0; i < ConstantValue.OrderViewInfoColName.Length; ++i)
            {
                AuditDataGrid.Columns[i].Header = ConstantValue.OrderViewInfoColName[i];
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
                DataTable dataTable = null;

                    dataTable = Database.FillDataTable("SELECT * FROM Order_to_supplier WHERE Supplier_id_FK=" + Database.UNO);
                string command;
                DataTable tempTable;
                foreach (DataRow row in dataTable.Rows)
                {
                    MyItem tempItem = new MyItem((int)row["Order_form_to_supplier_id_PK"],(int)row["Supplier_id_FK"],
                        (decimal)row["Price_of_all"],(string)row["Order_form_to_supplier_createdate"],
                        (string)row["Order_form_to_supplier_notes"]);
                    _cache.Add(tempItem);
                    //_data.Add(tempItem);
                }
                //_cache.OrderByDescending<MyItem>
                //_data = _cache;//这样赋值不知道有没有问题
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
            AuditDataGrid.ItemsSource = _data;//因为BGW不支持多线程 所以只能放这儿 也不太懂为什么
            AuditInfoDataGrid.ItemsSource = _info;
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
                    "SELECT Product_category, Product_name, Product_modle, Num_of_product,Price_of_product FROM Order_to_supplier_information WHERE Order_form_to_supplier_id_FK=" + info.NO);
                foreach (DataRow row in dataTable.Rows)
                {
                    Info tempItem = new Info((string)row["Product_category"], (string)row["Product_name"], (string)row["Product_modle"], (int)row["Num_of_product"], (decimal)row["Price_of_product"]);
                    _info.Add(tempItem);
                }
            }
            catch (Exception ex)
            {
                Tools.ShowMessageBox(ex.Message);
            }
        }




        private void FilterCancelButton_Click(object sender, RoutedEventArgs e)
        {
            OrderAuditNOInput.Text = "";

            refresh_data();
        }

        private void FilterButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string no = OrderAuditNOInput.Text.Trim();
                int _no = -1;
                if (no != "")
                {
                    if (!Tools.CheckNumberSequence(no))
                        throw new Exception("NO is invalid.");
                    _no = Convert.ToInt32(no);
                }

                _data.Clear();
                foreach(var item in _cache)
                {
                    if(item.NO==_no)
                        _data.Add(item);//这里_data里面的item虽然指向的对象和_cache中的是同一个，但是对程序应该没有影响
                }
                AuditDataGrid.Items.Refresh();
                if (_data.Count == 0)
                    MessageBox.Show("无符合要求的表单。");
            }catch(Exception ex)
            {
                MessageBox.Show("Error(s) occur when filtering: " + ex.Message);
            }
        }
    }
}
