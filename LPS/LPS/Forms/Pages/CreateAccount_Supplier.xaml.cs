using LPS.Utility;
using System;
using System.Collections.Generic;
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

namespace LPS.Forms.Pages
{
    /// <summary>
    /// CreateAccount_Supplier.xaml 的交互逻辑
    /// </summary>
    public partial class CreateAccount_Supplier : Page
    {
        public CreateAccount_Supplier()
        {
            InitializeComponent();
        }

        private void CreateAccount_SupplierAckButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Tools.CheckNumberSequence(CreateAccount_PhoneNumberText.Text) == false)
                    throw new Exception("Unvalid phone number.");
                if (Tools.CheckNumberSequence(CreateAccount_NOText.Text) == false)
                    throw new Exception("Unvalid supplier NO.");

                string comInsert = "INSERT INTO Supplier_information(Supplier_id_PK, Supplier_name, Supplier_phone, Supplier_password)" +
                    "values(@ID, @NAME, @PHONE, @PASSWORD)";//插入数据库的命令-（列名）+（参数名-自己起的）
                SqlDbType[] types = { SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar };//数据类型
                string[] keys = { "@ID", "@NAME", "@PHONE", "@PASSWORD" };//上面写的参数名

                List<string> values = new List<string>();//用来临时存参数的
                Dictionary<string, List<object>> parameters = new Dictionary<string, List<object>>();//用来传参的
                string temp = String.Empty;
                bool returnVal = false;//判断是否成功执行

                values.Add(CreateAccount_NOText.Text.Trim());
                values.Add(CreateAccount_NameText.Text.Trim());
                values.Add(CreateAccount_PhoneNumberText.Text.Trim());
                values.Add(CreateAccount_NOText.Text.Trim());
                for (int j = 0; j < values.Count; ++j)
                {
                    //依次把三个参数放入字典中
                    parameters[keys[j]] = new List<object> { types[j], values[j] };
                }
                returnVal = Database.Insert("Supplier_information", parameters, comInsert);

                if (!returnVal)
                    throw new Exception("Error occur when inserting" + temp);

                MessageBox.Show("添加成功！");
                clearInputField();
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

        private void clearInputField()
        {
            CreateAccount_NameText.Text = String.Empty;
            CreateAccount_NOText.Text = String.Empty;
            CreateAccount_PhoneNumberText.Text = String.Empty;
        }
    }
}
