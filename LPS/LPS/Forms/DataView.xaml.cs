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
using System.Windows.Shapes;

namespace LPS.Forms
{
    /// <summary>
    /// DataView.xaml 的交互逻辑
    /// </summary>
    public partial class DataView : Window
    {
        private DataTable source;
        private string TableName;

        public DataView()
        {
            InitializeComponent();
        }

        public void Initialize(bool[] Filters, string TableName, string[] TableColName)
        {
            this.TableName = TableName;
            if (Filters.Length != 10)
                throw new Exception("The size of filters is wrong.");
            NoComboBox.IsEnabled = Filters[0];
            AdminNoComboBox.IsEnabled = Filters[1];
            AdminNameComboBox.IsEnabled = Filters[2];
            StatusComboBox.IsEnabled = Filters[3];
            UserIDComboBox.IsEnabled = Filters[4];
            UserNameComboBox.IsEnabled = Filters[5];
            NotesTextBox.IsEnabled = Filters[6];
            SupplierNoComboBox.IsEnabled = Filters[7];
            SupplierNameComboBox.IsEnabled = Filters[8];
            CalendarS.IsEnabled = Filters[9];

            source = Database.FillDataTable(String.Format("SELECT * FROM {0}", TableName));

            //设置表头标题
            for (int i = 0; i < TableColName.Length; ++i)
            {
                source.Columns[i].ColumnName = TableColName[i];
            }

            DataGridmy.ItemsSource = source.DefaultView;
        }

        private void Filter_Click(object sender, RoutedEventArgs e)
        {
            //if(source.TableName == "Order_form")
            //{
            //    if()
            //}
        }

        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            source = Database.FillDataTable(String.Format("SELECT * FROM {0}", TableName));
        }

        private void Excel_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Configure open file dialog box
                Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
                dlg.FileName = Database.AddTimeStamp(TableName); // Default file name
                dlg.DefaultExt = ".xls"; // Default file extension
                dlg.Filter = "Excel files(*.xls)|*.xls"; // Filter files by extension

                // Show open file dialog box
                Nullable<bool> result = dlg.ShowDialog();


                // Process open file dialog box results
                if (result == true)
                {
                    // Open document
                    // string filename = dlg.FileName;
                    Tools.DataTableToExcel(source, dlg.FileName);
                    MessageBox.Show("保存成功~！");
                }
            }catch(Exception ex)
            {
                MessageBox.Show("保存失败哦。");
            }

        }
    }
}
