using Library.DataAccess;
using Library.Respository;
using Microsoft.EntityFrameworkCore.Update;
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
    /// Interaction logic for ChangePassWordWindow.xaml
    /// </summary>
    public partial class ChangePassWordWindow : Window
    {
        private Account account;
        private bool isChangePass = false;
        IAccountRepository accountRepositoryy;
        public ChangePassWordWindow(IAccountRepository _repository, Account _account)
        {
            InitializeComponent();
            accountRepositoryy = _repository;
            account = _account;
            Closed += ChangePassWordWindow_Closed;
        }

        private void ChangePassWordWindow_Closed(object? sender, EventArgs e)
        {
            MainWindow window = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
            if (window != null)
            {
                window.Opacity = 1;
            }
        }

        private void btnCancle_Click(object sender, RoutedEventArgs e)
        {
            if (isChangePass)
            {
                this.Close();
            }
            else
            {
                if (MessageBox.Show("Exit?", "Change Password", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    this.Close();
                }
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            string oldPass = txtOldPass.Password;
            string newPass = txtNewPass.Password;
            string rePass = txtRePass.Password;
            if (string.IsNullOrEmpty(oldPass)
                || string.IsNullOrEmpty(newPass)
                || string.IsNullOrEmpty(rePass))
            {
                tbSave.Text = "All fields cannot be left blank.";
            }
            else
            {
                if (oldPass.Equals(account.Password))
                {
                    if (newPass.Equals(rePass))
                    {
                        if (newPass.Equals(oldPass))
                        {
                            tbSave.Foreground = new SolidColorBrush(Colors.Red);
                            tbSave.Text = "Old password and new password cannot be duplicated.";
                        }
                        else
                        {
                            if (MessageBox.Show("Save change?", "Change Password", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                            {
                                account.Password = newPass;
                                if (accountRepositoryy.ChangePassword(account))
                                {
                                    tbSave.Foreground = new SolidColorBrush(Colors.LightSeaGreen);
                                    tbSave.Text = "Change password successfully.";
                                    isChangePass = true;
                                }
                            }
                        }
                    }
                    else
                    {
                        tbSave.Foreground = new SolidColorBrush(Colors.Red);
                        tbSave.Text = "New password does not match";
                    }
                }
                else
                {
                    tbSave.Foreground = new SolidColorBrush(Colors.Red);
                    tbSave.Text = "Your password incorrect.";
                }
            }
        }
    }
}
