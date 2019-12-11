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

using LPS.Utility;

namespace LPS.Forms.Pages
{
    /// <summary>
    /// CreateAccount_Staff.xaml 的交互逻辑
    /// </summary>
    public partial class CreateAccount_Staff : Page
    {
        private DataTable _dataTable = new DataTable();
        //private string _comboboxDefault = "--请选择--";
        private DateTime dateTime = new System.DateTime();

        public CreateAccount_Staff()
        {
            InitializeComponent();

            //初始化combobox里面的选项
            string command = "SELECT School_name FROM School_information";
            _dataTable = Database.FillDataTable("School_name", command);
            if (_dataTable != null)
            {
                //加载到combobox
                CreateAccount_BatchSchoolCombobox.Items.Clear();
                //CreateAccount_BatchSchoolCombobox.Items.Add(_comboboxDefault);
                for (int i = 0; i < _dataTable.Rows.Count; i++)
                {
                    CreateAccount_BatchSchoolCombobox.Items.Add(_dataTable.Rows[i]["School_name"]);
                }
            }

            dateTime = System.DateTime.Now;
            int currentYear = dateTime.Year;
            List<int> years = new List<int>();
            for (int i = 0; i < 10; ++i)
            {
                years.Add(currentYear--);
            }
            CreateAccount_BatchGradeCombobox.ItemsSource = years;
        }

        private void CreateAccount_StaffAckButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Tools.CheckNumberSequence(CreateAccount_PhoneNumberText.Text) == false)
                    throw new Exception("Unvalid phone number.");
                if (Tools.CheckNumberSequence(CreateAccount_NOText.Text) == false)
                    throw new Exception("Unvalid staff NO.");

                int enrollYear = Convert.ToInt32(CreateAccount_BatchGradeCombobox.SelectedValue.ToString().Trim());
                //输入有效性检查
                if (enrollYear < dateTime.Year - 70 || enrollYear > dateTime.Year + 5)
                //TODO:这里是强制性检查，年份部分可以改成提醒的
                {
                    throw new Exception("Number of class out of range.");
                }

                string schoolName = CreateAccount_BatchSchoolCombobox.SelectedItem.ToString();
                string command = string.Format("SELECT School_id FROM School_information WHERE School_name = '{0}'",
                    schoolName);//查询选中的学院的编号-查询命令
                string schoolID = ((string)Database.Query("School_information", command)).Trim();//查询选中的学院的编号

                string comInsert = "INSERT INTO Customer_information(Customer_id_PK, Customer_contact, Customer_password, College_name)" +
                    "values(@ID, @NAME, @PASSWORD, @COLLEGE)";//插入数据库的命令-（列名）+（参数名-自己起的）
                SqlDbType[] types = { SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar };//数据类型
                string[] keys = { "@ID", "@NAME", "@PASSWORD", "@COLLEGE" };//上面写的参数名

                List<string> values = new List<string>();//用来临时存参数的
                Dictionary<string, List<object>> parameters = new Dictionary<string, List<object>>();//用来传参的
                string temp = String.Empty;
                bool returnVal = false;//判断是否成功执行

                temp = "0" + schoolID + (enrollYear % 100).ToString()
                    + CreateAccount_NOText.Text.Trim().PadLeft(2, '0');
                //第 2、3 位标识职工所在的学院、部门，第 4、5位标识职工入职年份，第 6、7 位标识职工编号。

                values.Add(temp);
                values.Add(CreateAccount_NameText.Text.Trim());
                values.Add(temp);
                values.Add(schoolName);
                for (int j = 0; j < values.Count; ++j)
                {
                    //依次把三个参数放入字典中
                    parameters[keys[j]] = new List<object> { types[j], values[j] };
                }
                returnVal = Database.Insert("Customer_information", parameters, comInsert);

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
            CreateAccount_BatchGradeCombobox.SelectedIndex = -1;
            CreateAccount_BatchSchoolCombobox.SelectedIndex = -1;
            CreateAccount_NameText.Text = String.Empty;
            CreateAccount_NOText.Text = String.Empty;
            CreateAccount_PhoneNumberText.Text = String.Empty;
        }
    }
}
