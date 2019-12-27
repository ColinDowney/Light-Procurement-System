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
using System.Windows.Navigation;
using System.Windows.Shapes;
using static LPS.Manager.Functions;

namespace LPS.Forms.RFQ
{
    /// <summary>
    /// CreateRFQ.xaml 的交互逻辑
    /// </summary>
    public partial class CreateRFQ : Page
    {
        private BackgroundWorker _demoBGWorker = new BackgroundWorker();
        private ObservableCollection<MyItem> _data { get; set; }
        private List<MyItem> _cache { get; set; }

        /// <summary>
        /// 展示Backorder绑定的数据模型
        /// </summary>
        public class MyItem
        {
            public string Category { get; set; }
            public string Name { get; set; }
            public string Modle { get; set; }
            public int Num { get; set; }
            public bool isSelected { get; set; }
            public int ID { get; set; }

            public MyItem(string category, string name, string modle, int num, bool isSelected, int id)
            {
                Category = category;
                Name = name;
                Modle = modle;
                Num = num;
                this.isSelected = isSelected;
                ID = id;
            }

            public void Set(string category, string name, string modle, int num, bool isSelected, int id)
            {
                Category = category;
                Name = name;
                Modle = modle;
                Num = num;
                this.isSelected = isSelected;
                ID = id;
            }
        }


        public CreateRFQ()
        {
            _demoBGWorker.DoWork += BGWorker_DoWork;
            _demoBGWorker.RunWorkerCompleted += BGWorker_RunWorkerCompleted;
            _demoBGWorker.RunWorkerAsync();

            InitializeComponent();

            //初始化combobox里面的选项
            string command = "SELECT DISTINCT Product_category FROM Product_information";
            DataTable _dataTable = Database.FillDataTable(command);
            if (_dataTable != null)
            {
                //加载到combobox
                RFQCategoryCombobox.Items.Clear();
                for (int i = 0; i < _dataTable.Rows.Count; i++)
                {
                    RFQCategoryCombobox.Items.Add(_dataTable.Rows[i]["Product_category"]);
                }
            }

            //设置表头标题
            for (int i = 0; i < ConstantValue.Order_informationColName.Length; ++i)
            {
                RFQDataGrid.Columns[i].Header = ConstantValue.Order_informationColName[i];
            }

        }

        /// <summary>
        /// 获取需要订货的货品单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BGWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            //因为BGW不支持多线程 所以只能放这儿 也不太懂为什么
            _cache = new List<MyItem>();
            try
            {
                InsertGoodsInOrderToInventory();
                List<product> backList = checkInventory();
                BackProduct(backList);
                foreach(var item in backList)
                {
                    _cache.Add(new MyItem(item.Product_category, item.Product_name, item.Product_modle, item.Num, false, item.Product_ID));
                }
                _data = new ObservableCollection<MyItem>(_cache);
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
            //计算过程中的异常会被抓住，在这里可以进行处理。
            if (e.Error != null)
            {
                Type errorType = e.Error.GetType();
                MessageBox.Show("Error(s) occur when initialize the datagrid:" + e.Error.Message);
            }
        }

        private void RFQ_CreatButton_Click(object sender, RoutedEventArgs e)
        {
            //因为直接remove我们要遍历的List的话会影响遍历，
            //所以先把要移除的项记录下来
            List<MyItem> toRemove = new List<MyItem>();
            List<product> products = new List<product>();
            try
            {
                MyItem item;
                foreach (var i in RFQDataGrid.Items)
                {
                    item = i as MyItem;
                    if (item.isSelected)
                    {
                        products.Add(new product(item.ID, item.Num, item.Name.Trim(), item.Category.Trim(), item.Modle.Trim()));
                        toRemove.Add(item);
                    }
                }
                //创建询价单
                CreateRFQ(products, RFQNotesTextBox.Text.Trim());
                foreach (MyItem i in toRemove)
                {
                    _cache.Remove(i);
                }
                MessageBox.Show("操作成功！");
            }
            catch (Exception ex)
            {
                Tools.ShowMessageBox(ex.Message);
            }
            finally
            {
                _data.Clear();
                foreach (var i in _cache)
                    _data.Add(i);
                RFQDataGrid.Items.Refresh();
            }

        }

        private void FilterCancelButton_Click(object sender, RoutedEventArgs e)
        {
            _data.Clear();
            foreach (var i in _cache)
                _data.Add(i);
            RFQDataGrid.Items.Refresh();
        }

        private void FilterButton_Click(object sender, RoutedEventArgs e)
        {
            if (RFQCategoryCombobox.SelectedIndex != -1)
            {
                _data.Clear();
                //这里_data里面的item虽然指向的对象和_cache中的是同一个，但是对程序应该没有影响
                foreach(var i in _cache)
                {
                    if (i.Category.Trim() == RFQCategoryCombobox.SelectedValue.ToString().Trim())
                    {
                        _data.Add(i);
                    }
                }
                RFQDataGrid.Items.Refresh();
            }
        }
    }
}
