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
        public List<MyItem> _cache { get; set; }

        /// <summary>
        /// 展示Order_form绑定的数据模型
        /// </summary>
        public class MyItem
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
        public class Info
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

        private void refresh_data()
        {
            _data.Clear();
            foreach (var i in _cache)
                _data.Add(i);
            AuditDataGrid.Items.Refresh();
        }

        public OrderAudit()
        {
            _demoBGWorker.DoWork += BGWorker_DoWork;
            _demoBGWorker.RunWorkerCompleted += BGWorker_RunWorkerCompleted;
            _demoBGWorker.RunWorkerAsync();


            InitializeComponent();

            //初始化combobox里面的选项
            string command = "SELECT School_name FROM School_information";
            DataTable _dataTable = Database.FillDataTable("School_name", command);
            if (_dataTable != null)
            {
                //加载到combobox
                OrderAuditSchoolCombobox.Items.Clear();
                //CreateAccount_BatchSchoolCombobox.Items.Add(_comboboxDefault);
                for (int i = 0; i < _dataTable.Rows.Count; i++)
                {
                    OrderAuditSchoolCombobox.Items.Add(_dataTable.Rows[i]["School_name"]);
                }
            }

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
            _cache = new List<MyItem>();//因为BGW不支持多线程 所以只能放这儿 也不太懂为什么
            _info = new ObservableCollection<Info>();
            _data = new ObservableCollection<MyItem>();
            try
            {
                //耗时操作
                DataTable dataTable = Database.FillDataTable("Order_form", "SELECT * FROM Order_form WHERE Order_form_status='未审核'");
                string command;
                DataTable tempTable;
                foreach (DataRow row in dataTable.Rows)
                {
                    //查询发起订单的用户信息
                    command = string.Format("SELECT Customer_contact, College_name FROM Customer_information WHERE Customer_id_PK='{0}'", row["Customer_id_FK"]);
                    tempTable = Database.FillDataTable("Customer_information", command);

                    MyItem tempItem = new MyItem((int)row["Order_form_id_PK"], (string)row["Customer_id_FK"], ((string)tempTable.Rows[0].ItemArray[0]).Trim(),
                        ((string)tempTable.Rows[0].ItemArray[1]).Trim(), ((string)row["Order_notes"]).Trim());
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
                        Functions.UpdateOrderStatus(state, item.NO);
                        //string command = string.Format("UPDATE Order_form SET Order_form_status='{0}' WHERE Order_form_id_PK={1}",state , item.NO);
                        //int re = Database.ExecuteSqlCommand(command);
                        //if (re <= 0)
                        //    throw new Exception("Error occur when updating '" + item.NO + "'.");
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
                    _cache.Remove(i);
                }
                refresh_data();
            }
        }

        private void FilterCancelButton_Click(object sender, RoutedEventArgs e)
        {
            OrderAuditNOInput.Text = "";
            OrderAuditUIDInput.Text = "";
            OrderAuditSchoolCombobox.SelectedIndex = -1;
            refresh_data();
        }

        private void FilterButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string no, uid;
                no = OrderAuditNOInput.Text.Trim();
                int _no = 0;
                uid = OrderAuditUIDInput.Text.Trim();
                bool n, u, s, na, ua, sa; ;
                n = u = s = false;
                na = ua = sa = true;
                if (no != "")
                {
                    if (!Tools.CheckNumberSequence(no))
                        throw new Exception("NO is invalid.");
                    n = true;
                    _no = Convert.ToInt32(no);
                }
                if (uid != "")
                {
                    if (!Tools.CheckNumberSequence(uid))
                        throw new Exception("UID is invalid.");
                    u = true;
                }
                if (OrderAuditSchoolCombobox.SelectedIndex != -1)
                {
                    s = true;
                }
                _data.Clear();
                foreach(var item in _cache)
                {
                    if (n)
                        if (item.NO != _no)
                            na = false;
                    if (u)
                        if (item.UID != uid)
                            ua = false;
                    if (s)
                        if (item.College != OrderAuditSchoolCombobox.SelectedValue.ToString().Trim())
                            sa = false;
                    if (na && ua && sa)
                        _data.Add(item);//这里_data里面的item虽然指向的对象和_cache中的是同一个，但是对程序应该没有影响
                    na = ua = sa = true;
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
