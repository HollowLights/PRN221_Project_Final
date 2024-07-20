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
    /// Interaction logic for AddEmployeeWindow.xaml
    /// </summary>
    public partial class AddEmployeeWindow : Window
    {
        IAccountRepository accountRepository;
        private bool isAdd = false;
        public AddEmployeeWindow()
        {
            InitializeComponent();
            Closed += AddEmployeeWindow_Closed;
            accountRepository = new AccountRepository();
        }

        private void AddEmployeeWindow_Closed(object? sender, EventArgs e)
        {
            MainWindow window = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
            if (window != null)
            {
                window.Opacity = 1;
            }
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            string fullName = txtFullName.Text;
            string user = txtUser.Text;
            string password = txtPassword.Text;

            if(string.IsNullOrEmpty(fullName) 
                || string.IsNullOrEmpty(user) 
                || string.IsNullOrEmpty(password))
            {
                tbAdd.Text = "All fields cannot be left blank.";
            }
            else
            {
                if (accountRepository.GetAccountByUser(user) !=null)
                {
                    tbAdd.Text = "User existed.";
                }
                else
                {
                    Account account = new Account()
                    {
                        FullName = fullName,
                        User = user,
                        Password = password,
                        Role = true
                    };
                    if (accountRepository.addEmployeeAccount(account))
                    {
                        tbAdd.Foreground = new SolidColorBrush(Colors.LightSeaGreen);
                        tbAdd.Text = "Add Employee account successfully.";
                        MainWindow window = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
                        window.frmMain.Content = new EmployeePage();
                        isAdd = true;
                    }
                }
            }
        }

        private void btnCancle_Click(object sender, RoutedEventArgs e)
        {
            if (isAdd)
            {
                this.Close();
            }
            else
            {
                if (MessageBox.Show("Exit?", "Add Employee Account", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    this.Close();
                }
            }
        }
    }
}
