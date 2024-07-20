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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static Account account;
        public MainWindow(Account currentAccount)
        {
            InitializeComponent();
            account = currentAccount;
            btnName.Content = currentAccount.FullName;
            frmMain.Content = new HomePage();
            btnHome.Background = new SolidColorBrush(Colors.Aquamarine);
            if (currentAccount.Role != null)
            {
                btnEmployee.Visibility = Visibility.Hidden;
            }
        }

        private void btnHome_Click(object sender, RoutedEventArgs e)
        {
            frmMain.Content = new HomePage();
            resetBtnBackground();
            btnHome.Background = new SolidColorBrush(Colors.Aquamarine);
        }

        private void btnProducts_Click(object sender, RoutedEventArgs e)
        {
            frmMain.Content = new ProductsPage();
            resetBtnBackground();
            btnProducts.Background = new SolidColorBrush(Colors.Aquamarine);
        }

        private void resetBtnBackground()
        {
            btnEmployee.Background = null;
            btnProducts.Background = null;
            btnOrders.Background = null;
            btnHome.Background = null;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Do you want Logout?", "Logout", 
                MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                LoginWindow loginWindow = new LoginWindow();
                loginWindow.Show();
                MainWindow mainWindow = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
                mainWindow.Close();
            }
        }

        private void btnName_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
            mainWindow.Opacity = 0.2;
            IAccountRepository accountRepository = new AccountRepository();
            ChangePassWordWindow changePassWordWindow = new ChangePassWordWindow(accountRepository,account);
            changePassWordWindow.ShowDialog();
        }

        private void btnEmployee_Click(object sender, RoutedEventArgs e)
        {
            IAccountRepository accountRepository = new AccountRepository();
            frmMain.Content = new EmployeePage();
            resetBtnBackground();
            btnEmployee.Background = new SolidColorBrush(Colors.Aquamarine);
        }

        private void btnOrders_Click(object sender, RoutedEventArgs e)
        {
            frmMain.Content = new OrdersPage();
            resetBtnBackground();
            btnOrders.Background = new SolidColorBrush(Colors.Aquamarine);
        }
    }
}
