using Library.DataAccess;
using Library.Respository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Final_Project_PRN221
{
    /// <summary>
    /// Interaction logic for EditEmployeeWindow.xaml
    /// </summary>
    public partial class EditEmployeeWindow : Window
    {
        IAccountRepository accountRepository;
        private bool isUpdate = false;
        public EditEmployeeWindow(int id)
        {
            InitializeComponent();
            accountRepository = new AccountRepository();
            Account account = accountRepository.getAccountById(id);
            Closed += EditEmployeeWindow_Closed;
            txtId.Text = account.Id.ToString();
            txtFullName.Text = account.FullName;
            txtUser.Text = account.User;
            txtPassword.Text = account.Password;
        }

        private void EditEmployeeWindow_Closed(object? sender, EventArgs e)
        {
            MainWindow window = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
            if (window != null)
            {
                window.Opacity = 1;
            }
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            string fullName = txtFullName.Text;
            string user = txtUser.Text;
            string password = txtPassword.Text;
            string id = txtId.Text;

            if (string.IsNullOrEmpty(fullName)
                || string.IsNullOrEmpty(user)
                || string.IsNullOrEmpty(password))
            {
                tbUpdate.Text = "All fields cannot be left blank.";
            }
            else
            {
                Account account = new Account()
                {
                    Id = Convert.ToInt32(id),
                    FullName = fullName,
                    User = user,
                    Password = password
                };
                if (accountRepository.updateEmployeeAccount(account))
                {
                    tbUpdate.Foreground = new SolidColorBrush(Colors.LightSeaGreen);
                    tbUpdate.Text = "Update Employee account successfully.";
                    MainWindow window = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
                    window.frmMain.Content = new EmployeePage();
                    isUpdate = true;
                }
            }
        }

        private void btnCancle_Click(object sender, RoutedEventArgs e)
        {
            if (isUpdate)
            {
                this.Close();
            }
            else
            {
                if (MessageBox.Show("Exit?", "Update Employee Account", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    this.Close();
                }
            }
        }
    }
}
