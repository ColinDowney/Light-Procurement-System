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
    /// OrderAudit.xaml 的交互逻辑
    /// </summary>
    public partial class OrderAudit : Window
    {
        private BackgroundWorker _demoBGWorker = new BackgroundWorker();
        private ObservableCollection<MyItem> _data { get; set; }
        private ObservableCollection<Info> _info { get; set; }

        /// <summary>
        /// 展示Order_form绑定的数据模型
        /// </summary>
        private class MyItem
        {
            public int NO { get; set; }
            public string UID { get; set; }
            public string User { get; set; }
            public string College { get; set; }
            public string Notes { get; set; }
            public bool isSelected { get; set; }

            public MyItem(int nO, string uID, string user, string college, string notes)
            {
                NO = nO;
                UID = uID;
                User = user;
                College = college;
                Notes = notes;
                isSelected = false;
            }

            public void Set(int nO, string uID, string user, string college, string notes)
            {
                NO = nO;
                UID = uID;
                User = user;
                College = college;
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

            public Info(string category, string name, string modle, int num)
            {
                Category = category;
                Name = name;
                Modle = modle;
                Num = num;
            }
        }

        public OrderAudit()
        {
            _demoBGWorker.DoWork += BGWorker_DoWork;
            _demoBGWorker.RunWorkerCompleted += BGWorker_RunWorkerCompleted;
            _demoBGWorker.RunWorkerAsync();

            InitializeComponent();

            //设置表头标题
            for (int i = 0; i < ConstantValue.Order_informationColName.Length; ++i)
            {
                AuditInfoDataGrid.Columns[i].Header = ConstantValue.Order_informationColName[i];
            }

            for (int i = 0; i < ConstantValue.Order_formColName.Length; ++i)
            {
                AuditDataGrid.Columns[i].Header = ConstantValue.Order_formColName[i];
            }
        }

        private void BGWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            _data = new ObservableCollection<MyItem>();//因为BGW不支持多线程 所以只能放这儿 也不太懂为什么
            _info = new ObservableCollection<Info>();
            try
            {
                //耗时操作
                DataTable dataTable = Database.FillDataTable("Order_form", "SELECT * FROM Order_form WHERE Order_form_status='未审核'");
                string command;
                DataTable tempTable;
                foreach (DataRow row in dataTable.Rows)
                {
                    //查询发起订单的用户信息
                    command = string.Format("SELECT Staff_name, Staff_college FROM Staff_information WHERE Staff_id_PK={0}", row["Customer_id_FK"]);
                    tempTable = Database.FillDataTable("Staff_information", command);
                    MyItem tempItem = new MyItem((int)row["Order_form_id_PK"], (string)row["Customer_id_FK"], ((string)tempTable.Rows[0].ItemArray[0]).Trim(),
                        ((string)tempTable.Rows[0].ItemArray[1]).Trim(), ((string)row["Order_notes"]).Trim());
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

                DataTable dataTable = Database.FillDataTable("Order_information",
                    "SELECT Product_category, Product_name, Product_modle, Num_of_product FROM Order_information WHERE Order_form_id_FK=" + info.NO);
                foreach (DataRow row in dataTable.Rows)
                {
                    Info tempItem = new Info((string)row["Product_category"], (string)row["Product_name"], (string)row["Product_modle"], (int)row["Num_of_product"]);
                    _info.Add(tempItem);
                }
            }
            catch (Exception ex)
            {
                Tools.ShowMessageBox(ex.Message);
            }
        }


        private void OrderPass_Click(object sender, RoutedEventArgs e)
        {
            ChangeState("审核通过");
        }

        private void OrderFail_Click(object sender, RoutedEventArgs e)
        {
            ChangeState("审核不通过");
        }

        /// <summary>
        /// 更新订购单的状态
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
                foreach (var i in AuditDataGrid.Items)
                {
                    item = i as MyItem;
                    if (item.isSelected)
                    {
                        string command = string.Format("UPDATE Order_form SET Order_form_status='{0}' WHERE Order_form_id_PK={1}",state , item.NO);
                        int re = Database.ExecuteSqlCommand(command);
                        if (re <= 0)
                            throw new Exception("Error occur when updating '" + item.NO + "'.");
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

    }
}
