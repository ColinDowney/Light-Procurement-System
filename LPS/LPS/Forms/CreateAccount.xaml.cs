using System.Windows;
using System.Windows.Controls;
using LPS.Forms.Pages;

namespace LPS.Forms
{
    /// <summary>
    /// CreateAccount.xaml 的交互逻辑
    /// </summary>
    public partial class CreateAccount : Window
    {
        private Page _customerPage;
        //private Page _classPage;
        //private Page _staffPage;
        private Page _supplierPage;
        private Page _adminPage;

        public CreateAccount()
        {
            InitializeComponent();
        }

        private void ClassButton_Click(object sender, RoutedEventArgs e)
        {
            if (_customerPage == null)
            {
                _customerPage = new CreateAccount_Customer();
            }
            Content.Content = new Frame() { Content = _customerPage };
            //if (_classPage == null)
            //{
            //    _classPage = new CreateAccount_ClassPage();
            //}
            //Content.Content = new Frame() { Content = _classPage };
        }

        private void StaffButton_Click(object sender, RoutedEventArgs e)
        {
            if (_customerPage == null)
            {
                _customerPage = new CreateAccount_Customer();
            }
            Content.Content = new Frame() { Content = _customerPage };
            //if (_staffPage == null)
            //{
            //    _staffPage = new CreateAccount_Staff();
            //}
            //Content.Content = new Frame() { Content = _staffPage };
        }

        private void Supplier_Button_Click(object sender, RoutedEventArgs e)
        {
            if (_supplierPage == null)
            {
                _supplierPage = new CreateAccount_Supplier();
            }
            Content.Content = new Frame() { Content = _supplierPage };
        }

        private void Admin_Button_Click(object sender, RoutedEventArgs e)
        {
            if (_adminPage == null)
            {
                _adminPage = new CreateAccount_Admin();
            }
            Content.Content = new Frame() { Content = _adminPage };
        }
    }
}
