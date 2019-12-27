using LPS.Utility;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    /// DataEntry.xaml 的交互逻辑
    /// </summary>
    public partial class DataEntry : Window
    {
        private ObservableCollection<MyItem> _data { get; set; }
        private ObservableCollection<string> _category { get; set; }

        public DataEntry()
        {
            _data = new ObservableCollection<MyItem>() { };
            _category = new ObservableCollection<string>();
            foreach (string category in ConstantValue.Order_informationCategory)
            {
                _category.Add(category);
            }

            InitializeComponent();

            OrderDataGrid.ItemsSource = _data;
            Category.ItemsSource = _category;
            for(int i = 0; i < ConstantValue.Order_informationColName.Length; ++i)
            {
                OrderDataGrid.Columns[i].Header = ConstantValue.Order_informationColName[i];
            }
        }


        private void OrderCreate_Click(object sender, RoutedEventArgs e)
        {
            //创建订购批次单
            //Customer_id_FK, Create_date, Order_notes, Order_form_status
            DateTime dateTime = System.DateTime.Now;
            int customerID = Database.UNO;
            string orderNotes = OrderNotes.Text.Trim();

            try
            {
                string tableName = "Order_form";
                //插入数据库的命令-（列名）+（参数名-自己起的）
                string comInsert = "INSERT INTO " + 
                    tableName + "(Customer_id_FK, Create_date, Order_notes, Order_form_status) " +
                    " values(@ID, @DATE, @NOTES, @STATE)";                      
                SqlDbType[] types = { SqlDbType.VarChar, SqlDbType.DateTime, SqlDbType.NVarChar, SqlDbType.VarChar };//数据类型
                string[] keys = { "@ID", "@DATE", "@NOTES", "@STATE" };//上面写的参数名

                List<object> values = new List<object>();//用来临时存参数的
                Dictionary<string, List<object>> parameters = new Dictionary<string, List<object>>();//用来传参的
                bool returnVal = false;//判断是否成功执行

                values.Add(customerID);
                values.Add(dateTime);
                values.Add(orderNotes);
                values.Add("未审核");

                for (int j = 0; j < values.Count; ++j)
                {
                    //依次把三个参数放入字典中
                    parameters[keys[j]] = new List<object> { types[j], values[j] };
                 }
                returnVal = Database.Insert(parameters, comInsert);

                if (!returnVal)
                    throw new Exception("Error occur when create the order form.");

                values.Clear();
                parameters.Clear();

                //创建订购细单
                //Order_form_id_FK, Product_category, Product_name, Product_modle, Num_of_product
                //查询之前建立的订购批次单的编号
                string command = string.Format("SELECT MAX(Order_form_id_PK) from {0}",
                    tableName);
                int orderID = (int)Database.Query(command);

                tableName = "Order_information";
                comInsert = "INSERT INTO " +
                    tableName + "(Order_form_id_FK, Product_category, Product_name, Product_modle, Num_of_product)" +
                    "values(@ID, @CATE, @NAME, @MODLE, @NUM)";
                SqlDbType[] itypes = { SqlDbType.Int, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.Int };//数据类型
                string[] ikeys = { "@ID", "@CATE", "@NAME", "@MODLE", "@NUM" };//上面写的参数名

                returnVal = false;//判断是否成功执行

                for(int i = 0; i < _data.Count; ++i)
                {
                    values.Add(orderID);
                    values.Add(_data[i].Category);
                    values.Add(_data[i].Name);
                    values.Add(_data[i].Modle);
                    values.Add(_data[i].Num);

                    for (int j = 0; j < values.Count; ++j)
                    {
                        //依次把参数放入字典中
                        parameters[ikeys[j]] = new List<object> { itypes[j], values[j] };
                    }
                    returnVal = Database.Insert(parameters, comInsert);

                    if (!returnVal)
                        throw new Exception(string.Format("Error occur when create the order information form of order form {0}.", orderID));
                    values.Clear();
                }


                MessageBox.Show("添加成功！");
                OrderNotes.Text = String.Empty;
                _data.Clear();
                OrderDataGrid.ItemsSource = _data;
                //clearInputField();

            }
            catch (FormatException)
            {
                MessageBox.Show("请输入整数。");
            }
            catch (Exception ex)
            {
                //MessageBox.Show("Error: " + ex.Message);
                Tools.ShowMessageBox("Error: " + ex.Message);
            }
        }

        private class MyItem
        {
            public string Category { get; set; }
            public string Name { get; set; }
            public string Modle { get; set; }
            public int Num { get; set; }
        }
    }
}
