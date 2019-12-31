using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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
//using System.Windows.Forms;

using LPS.Utility;


namespace LPS.Forms.Order
{
    /// <summary>
    /// Purchase_input.xaml 的交互逻辑
    /// </summary>
    public partial class Purchase_input : Window
    {


        //获取已经到货的进货单编号
        private void Purchased_button_Click(object sender, RoutedEventArgs e)
        {
            String RFQ_input_id = TextBox_Purchase.Text.ToString();
            //修改订购单状态
            String Order_form_id1;//订购单ID
            Order_form_id1 = (String)Database.Query("select Order_form_id_FK from RFQ where RFQ_id_PK="+ RFQ_input_id);
            Database.changeSql("Order_form", Order_form_id1, "Order_form_status", "待取货");

            //修改销售批次单状态
            String Sales_batch_id1;
            Sales_batch_id1 = (String)Database.Query("select Sales_batch_id_PK from Sales_batch where Order_form_id_FK ="+ Order_form_id1);
            Database.changeSql("Sales_batch", Sales_batch_id1, "Sales_batch_status", "待取货");

            //修改进货通知单状态
            String Purchase_notice_id1;
            Purchase_notice_id1 = (String)Database.Query("select Purchase_notice_id_PK from Purchase_notice where Order_form_id ="+ Order_form_id1);
            Database.changeSql("Purchase_notice", Purchase_notice_id1, "Purchase_notice_status", "已到货");

            //新增进货单
            String Order_from_input_id;
            String Supplier_id;
            String Price_all;
            String Notes;
            String CreateDate;
            Order_from_input_id = Database.Query("select Order_form_id_FK from RFQ where RFQ_id_PK =" + RFQ_input_id).ToString();
            Supplier_id = Supplier.Text.ToString();
            Price_all = Prices.Text.ToString();
            Notes = notes.Text.ToString();
            CreateDate = DateTime.Now.ToLongDateString().ToString();

            try
            {
                /*
                if (Tools.CheckNumberSequence(Supplier.Text) == false)
                    throw new Exception("Unvalid Supplier.");
                if (Tools.CheckNumberSequence(Prices.Text) == false)
                    throw new Exception("Unvalid Prices.");
                if (Tools.CheckNumberSequence(notes.Text) == false)
                    throw new Exception("Unvalid notes.");
                    */

                /*int enrollYear = Convert.ToInt32(CreateAccount_BatchGradeCombobox.SelectedValue.ToString().Trim());
                //输入有效性检查
                

                string schoolName = CreateAccount_BatchSchoolCombobox.SelectedItem.ToString();
                
                string command = string.Format("SELECT School_id FROM School_information WHERE School_name = '{0}'",
                    schoolName);//查询选中的学院的编号-查询命令
                string schoolID = ((string)Database.Query(command)).Trim();//查询选中的学院的编号
                */
                string comInsert = "INSERT INTO Purchase_form(Purchase_form_id_PK, Supplier_id_FK, Order_form_id_FK, Purchase_form_createdate,Price_of_all,Purchase_form_notes)" +
                    "values(@ID1,@ID2,@ID3, @DATE, @MONEY, @NOTES)";//插入数据库的命令-（列名）+（参数名-自己起的）
                SqlDbType[] types = { SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar };//数据类型
                string[] keys = { "@ID1", "@ID2", "@ID3", "@DATE", "@MONEY", "@NOTES" };//上面写的参数名

                List<string> values = new List<string>();//用来临时存参数的
                Dictionary<string, List<object>> parameters = new Dictionary<string, List<object>>();//用来传参的
                string temp = String.Empty;
                bool returnVal = false;//判断是否成功执行

                temp = Database.Query("SELECT Purchase_form_id_PK FROM Purchase_form ORDER BY col_name ASC LIMIT 1;").ToString();
                //temp = values(temp) + 1;
                //temp = temp;
                int temp1 = Convert.ToInt32(temp) + 1;
                temp = temp1.ToString();
                //SELECT * FROM table_name ORDER BY col_name ASC LIMIT 1; # 升序，默认
                /*Order_from_input_id;
            String Supplier_id;
            String Price_all;
            String Notes;
            String CreateDate;*/
                values.Add(temp);
                values.Add(Supplier_id);
                values.Add(Order_from_input_id);
                values.Add(CreateDate);
                values.Add(Price_all);
                values.Add(Notes);
                
              
                for (int j = 0; j < values.Count; ++j)
                {
                    //依次把三个参数放入字典中
                    parameters[keys[j]] = new List<object> { types[j], values[j] };
                }
                returnVal = Database.Insert(parameters, comInsert);





                if (!returnVal)
                    throw new Exception("Error occur when inserting" + temp);

                MessageBox.Show("添加成功！");
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

        private void Notes_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
