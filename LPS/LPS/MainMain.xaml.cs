using System;
using System.Collections.Generic;
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

namespace LPS
{
    /// <summary>
    /// MainMain.xaml 的交互逻辑
    /// </summary>
    public partial class MainMain : Window
    {
        public MainMain()
        {
            InitializeComponent();
        }

        private void Customer_Click(object sender, RoutedEventArgs e)
        {
            Login l = new Login(1);
            l.Show();
        }

        private void Supplier_Click(object sender, RoutedEventArgs e)
        {
            Login l = new Login(2);
            l.Show();
        }

        private void Admin_Click(object sender, RoutedEventArgs e)
        {
            Login l = new Login(3);
            l.Show();
        }
    }
}
