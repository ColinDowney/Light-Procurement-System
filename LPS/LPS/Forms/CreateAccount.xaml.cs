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
using System.Windows.Shapes;
using LPS.Utility;

namespace LPS.Forms
{
    /// <summary>
    /// CreateAccount.xaml 的交互逻辑
    /// </summary>
    public partial class CreateAccount : Window
    {
        private DataTable _dataTable = new DataTable();
        //private string _comboboxDefault = "--请选择--";
        private DateTime dateTime = new System.DateTime();

        public CreateAccount()
        {
            InitializeComponent();

            //初始化combobox里面的选项
            string command = "SELECT School_name FROM School_information";
            _dataTable = Database.FillDataSet("School_name", command);
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
            for(int i = 0; i < 4; ++i)
            {
                years.Add(currentYear--);
            }
            CreateAccount_BatchGradeCombobox.ItemsSource = years;
        }

        private void CreateAccount_BatchAckButton_Click(object sender, RoutedEventArgs e)
        {
            int front, end;
            front = end = 0;
            try
            {
                front = Convert.ToInt32(CreateAccount_BatchNoFrontText.Text);
                end = Convert.ToInt32(CreateAccount_BatchNoEndText.Text);
                int enrollYear = Convert.ToInt32(CreateAccount_BatchGradeCombobox.SelectedValue.ToString().Trim());
                //输入有效性检查
                if ((front <= 0 || front > 99) || (end <= 0) || end > 99 || enrollYear < dateTime.Year - 10 || enrollYear > dateTime.Year + 2)
                    //TODO:这里是强制性检查，年份部分可以改成提醒的
                {
                    throw new Exception("Number of class out of range.");
                }
                
                //if(CreateAccount_BatchSchoolCombobox.SelectedItem.ToString()== _comboboxDefault)
                //{
                //    throw new Exception("Hasn't selected school information.");
                //}

                string schoolName = CreateAccount_BatchSchoolCombobox.SelectedItem.ToString();
                string command = string.Format("SELECT School_id FROM School_information WHERE School_name = '{0}'",
                    schoolName);//查询选中的学院的编号-查询命令
                string schoolID = ((string)Database.Query("School_information", command)).Trim();//查询选中的学院的编号

                string comInsert = "INSERT INTO Class_information(Class_id_PK, College_name, Class_password)" +
                    "values(@ID, @NAME, @PASSWORD)";//插入数据库的命令-（列名）+（参数名-自己起的）
                SqlDbType[] types = { SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar };//数据类型
                string[] keys = { "@ID", "@NAME", "@PASSWORD" };//上面写的参数名

                List<string> values = new List<string>();//用来临时存参数的
                Dictionary<string, List<object>> parameters = new Dictionary<string, List<object>>();//用来传参的
                string temp = String.Empty;
                bool returnVal = false;//判断是否成功执行
                for (int i = front; i <= end; ++i)
                {
                    temp = schoolID + (enrollYear % 100).ToString()
                        + i.ToString().PadLeft(2, '0');
                    //第 1、2 位标识班级所在的学院，第 3、4 位标识班级所在的年级，第 5、6 位标识班级编号。

                    values.Add(temp);
                    values.Add(schoolName);
                    values.Add(temp);
                    for(int j = 0;j < 3;++j)
                    {
                        //依次把三个参数放入字典中
                        parameters[keys[j]] = new List<object> { types[j], values[j] };
                    }
                    returnVal = Database.Insert("Class_information", parameters, comInsert);

                    if (!returnVal)
                        throw new Exception("Error occur when inserting" + temp);
                    //清理后进入下一个班级信息的插入
                    values.Clear();
                    parameters.Clear();
                }


            }
            catch (FormatException)
            {
                MessageBox.Show("请输入整数。");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            MessageBox.Show("添加成功！");
        }
    }
}
