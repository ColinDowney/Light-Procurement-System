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

        public DataView()
        {
            InitializeComponent();
        }

        public void Initialize(bool[] Filters, string TableName, string[] TableColName)
        {
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
    }
}
