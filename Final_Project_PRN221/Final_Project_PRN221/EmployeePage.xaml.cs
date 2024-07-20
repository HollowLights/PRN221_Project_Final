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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Final_Project_PRN221
{
    /// <summary>
    /// Interaction logic for EmployeePage.xaml
    /// </summary>
    public partial class EmployeePage : Page
    {
        IAccountRepository accountRepository;
        private bool updateOn = true;
        public EmployeePage()
        {
            InitializeComponent();
            accountRepository = new AccountRepository();
            loadLvEmployee();
        }

        public void loadLvEmployee()
        {
            lvEmployee.ItemsSource = accountRepository.getEmployeeAccount();
        }

        private void txtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            string text = txtSearch.Text.Trim().ToLower();
            lvEmployee.ItemsSource = accountRepository.getEmployeeAccount()
                .Where(o=> o.FullName.ToLower().Contains(text) 
                || o.User.ToLower().Contains(text));
        }

        private void cbActive_DropDownClosed(object sender, EventArgs e)
        {
            string text = cbActive.Text.Trim().ToLower();
            if(!string.IsNullOrEmpty(text))
            {
                if (text.Equals("all"))
                {
                    loadLvEmployee();
                }else if(text.Equals("active"))
                {
                    lvEmployee.ItemsSource = accountRepository.getEmployeeAccount()
                        .Where(o=>o.Role==true);
                }
                else if(text.Equals("inactive"))
                {
                    lvEmployee.ItemsSource = accountRepository.getEmployeeAccount()
                        .Where(o => o.Role == false);
                }
            }
        }

        private void CheckBox_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            CheckBox checkBox = sender as CheckBox;
            if(checkBox.IsChecked == true)
            {
                checkBox.IsChecked = false;
            }else checkBox.IsChecked = true;
        }

        private void CheckBox_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            if (updateOn)
            {
                updateOn = false;
                CheckBox checkBox = sender as CheckBox;
                int id = Convert.ToInt32(checkBox.Tag);
                bool role = (bool)accountRepository.getAccountById(id).Role;
                string active = role ? "InActive" : "Active";

                if (MessageBox.Show($"Change to {active}", "Change role", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    if (accountRepository.setAccountRole(id, !role))
                    {
                        MessageBox.Show("Update success.", "Change role");
                    }
                }
                else
                {
                    checkBox.IsChecked = role;
                }
                updateOn = true;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
            mainWindow.Opacity = 0.2;
            AddEmployeeWindow addEmployeeWindow = new AddEmployeeWindow();
            addEmployeeWindow.ShowDialog();
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
            mainWindow.Opacity = 0.2;
            Button btn = sender as Button;
            int id = Convert.ToInt32(btn.Tag);
            Account account = accountRepository.getAccountById(id);
            if (account != null)
            {
                EditEmployeeWindow editEmployeeWindow = new EditEmployeeWindow(id);
                editEmployeeWindow.ShowDialog();
            }

        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            int id = Convert.ToInt32(btn.Tag);

            if (MessageBox.Show("Do you want delete Employee Account", "Delete Employee Account", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                if (accountRepository.removeEmployeeAccount(id))
                {
                    MessageBox.Show("Delete success.", "Delete Employee Account");
                    loadLvEmployee();
                }
            }
        }
    }
}
