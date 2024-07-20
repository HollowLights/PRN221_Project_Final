using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Principal;
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
using Library.DataAccess;
using Library.Respository;
using Microsoft.Extensions.Configuration;

namespace Final_Project_PRN221
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        IAccountRepository AccountRepository = new AccountRepository();
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            string user = txtUser.Text;
            string pass = txtPass.Password;

            if (user.Trim() == "")
            {
                tbLogin.Text = "Enter user.";
            }
            else if (pass.Trim() == "")
            {
                tbLogin.Text = "Enter password.";
            }
            else
            {
                Account account = AccountRepository.GetAccountByUser(user);
                if (account == null || !(user.Equals(account.User) && pass.Equals(account.Password)))
                {
                    tbLogin.Text = "User or password is incorrect.";
                }else if (account !=null && account.Role== false)
                {
                    tbLogin.Text = "Your account has been locked.";
                }
                else
                {
                    MainWindow mainWindow = new MainWindow(account);
                    mainWindow.Show();
                    LoginWindow loginWindow = Application.Current.Windows.OfType<LoginWindow>().FirstOrDefault();
                    loginWindow.Close();
                }
            }
        }
    }
}
