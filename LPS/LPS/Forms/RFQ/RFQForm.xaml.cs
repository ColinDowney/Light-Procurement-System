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

namespace LPS.Forms.RFQ
{
    /// <summary>
    /// RFQForm.xaml 的交互逻辑
    /// </summary>
    public partial class RFQForm : Window
    {
        private Page _create;
        public RFQForm()
        {
            InitializeComponent();
        }

        private void RFQ_CreateButton_Click(object sender, RoutedEventArgs e)
        {
            if (_create == null)
            {
                _create = new CreateRFQ();
            }
            Content.Content = new Frame() { Content = _create };
        }

        private void RFQ_AuditButton_Click(object sender, RoutedEventArgs e)
        {

        }

    }
}
