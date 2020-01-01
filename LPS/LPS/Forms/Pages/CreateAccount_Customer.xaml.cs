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
    /// CreateAccount_Customer.xaml 的交互逻辑
    /// </summary>
    public partial class CreateAccount_Customer : Page
    {
        private DataTable _dataTable = new DataTable();

        public CreateAccount_Customer()
        {
            InitializeComponent();

            //初始化combobox里面的选项
            string command = "SELECT School_name FROM School_information";
            _dataTable = Database.FillDataTable(command);
            if (_dataTable != null)
            {
                //加载到combobox
                CreateAccount_BatchSchoolCombobox.Items.Clear();
                for (int i = 0; i < _dataTable.Rows.Count; i++)
                {
                    CreateAccount_BatchSchoolCombobox.Items.Add(_dataTable.Rows[i]["School_name"]);
                    //schooldict.Add((string)_dataTable.Rows[i]["School_name"], (int)_dataTable.Rows[i]["School_id"]);
                }
            }
        }

        private void CreateAccount_AckButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //输入检查
                if (String.IsNullOrEmpty(CreateAccount_PasswordText.Password) ||
                    CreateAccount_BatchSchoolCombobox.SelectedIndex == -1 ||
                    String.IsNullOrEmpty(CreateAccount_NameText.Text) ||
                    !Tools.CheckNumberSequence(CreateAccount_PhoneNumberText.Text.Trim()))
                    throw new Exception("Illegal input.");

                //Database.CreateAdminAccount(CreateAccount_NameText.Text.Trim(), CreateAccount_PasswordText.SecurePassword);

                string email, phone;
                email = phone = "";
                if (!String.IsNullOrEmpty(CreateAccount_EmailText.Text))
                    email = CreateAccount_EmailText.Text.Trim();
                if (!String.IsNullOrEmpty(CreateAccount_PhoneNumberText.Text))
                    phone = CreateAccount_PhoneNumberText.Text.Trim();
                string schoolName = CreateAccount_BatchSchoolCombobox.SelectedItem.ToString().Trim();
                string comInsert = "INSERT INTO Customer_information(College_name, Customer_contact, " +
                    "Customer_contact_phone, Customer_contact_email, Customer_password )" +
                    "values(@SCHOOL, @NAME, @PHONE, @EMAIL, @PASSWORD)";//插入数据库的命令-（列名）+（参数名-自己起的）
                SqlDbType[] types = { SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar };//数据类型
                string[] keys = { "@SCHOOL", "@NAME", "@PHONE", "@EMAIL", "@PASSWORD" };//上面写的参数名

                List<string> values = new List<string>();//用来临时存参数的
                Dictionary<string, List<object>> parameters = new Dictionary<string, List<object>>();//用来传参的
                bool returnVal = false;//判断是否成功执行

                values.Add(schoolName);
                values.Add(CreateAccount_NameText.Text.Trim());
                values.Add(phone);
                values.Add(email);
                values.Add(CreateAccount_PasswordText.Password);
                for (int j = 0; j < values.Count; ++j)
                {
                    //依次把参数放入字典中
                    parameters[keys[j]] = new List<object> { types[j], values[j] };
                }
                returnVal = Database.Insert(parameters, comInsert);

                if (!returnVal)
                    throw new Exception("Error occur when inserting.");

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
            CreateAccount_BatchSchoolCombobox.SelectedIndex = -1;
            CreateAccount_NameText.Text = String.Empty;
            CreateAccount_EmailText.Text = String.Empty;
            CreateAccount_PhoneNumberText.Text = String.Empty;
            CreateAccount_PasswordText.Password = String.Empty;
        }
    }
}
