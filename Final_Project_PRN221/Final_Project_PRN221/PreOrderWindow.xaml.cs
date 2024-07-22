using Library.DataAccess;
using Library.Respository;
using System;
using System.Linq;
using System.Windows;

namespace Final_Project_PRN221
{
    /// <summary>
    /// Interaction logic for PreOrderWindow.xaml
    /// </summary>
    public partial class PreOrderWindow : Window
    {
        private int tableId;
        private int accountId;

        public PreOrderWindow(int tableId)
        {
            InitializeComponent();
            this.tableId = tableId;
            accountId = MainWindow.account.Id;
            LoadTableDetails();

        }

        private void LoadTableDetails()
        {
            ITableRepository tableRepository = new TableRepository();
            Table table = tableRepository.getTableById(tableId);
            lblTableName.Content = table.Name;
            lbTableFee.Content = table.Type.PricePerHour.ToString("0/H");

            // Load other details if the table is pre-ordered
            if (tableRepository.IsPreOrder(tableId))
            {
                lvPreOrder.ItemsSource = tableRepository.getPreOrderListByTableId(tableId);
            }

            if (tableRepository.checkPreOrder(tableId, DateTime.Now))
            {
                btnStart.IsEnabled = true; // Disable the button if there is a pre-order
            }
            else
            {
                btnStart.IsEnabled = false; // Enable the button if no pre-order exists
            }
        }

        private void btnConfirm_Click(object sender, RoutedEventArgs e)
        {
            // Confirm pre-order logic
            ITableRepository tableRepository = new TableRepository();
            DateTime orderDate;

            try
            {
                orderDate = DateTime.Parse(dpPreOrder.Text);
            }
            catch (FormatException)
            {
                MessageBox.Show("Invalid date format. Please enter a valid date.");
                return;
            }

            // Check if the selected date is in the past
            if (orderDate < DateTime.Now.Date)
            {
                MessageBox.Show("You cannot create a pre-order for a past date.");
                return;
            }

            if (tableRepository.checkPreOrder(tableId, orderDate))
            {
                MessageBox.Show("The table has already been pre-ordered for that day");
                return;
            }


            PreOrder preOrder = new PreOrder
            {
                TableId = tableId,
                AccountId = accountId,
                Customer = txtCustomer.Text,
                OrderDate = orderDate
            };

            try
            {
                if (tableRepository.createPreOrder(tableId, preOrder))
                {
                    MessageBox.Show("Pre-order added successfully");
                }
                else
                {
                    MessageBox.Show("Failed to add pre-order");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while adding the pre-order: {ex.Message}");
            }
            finally
            {
                this.Close();
                MainWindow mainWindow = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
                if (mainWindow != null)
                {
                    mainWindow.frmMain.Content = new HomePage();
                }
            }
        }


        private void btnBacK_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            MainWindow mainWindow = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
            if (mainWindow != null)
            {
                mainWindow.frmMain.Content = new HomePage();
            }
        }

        private void btnStart_Click(object sender, RoutedEventArgs e)
        {
            ITableRepository tableRepository = new TableRepository();
            IOrderRepository orderRepository = new OrderRepository();
            if (orderRepository.createNewOrder(tableId))
            {
                tableRepository.setTableActive(tableId, true);
                tableRepository.deletePreOrder(tableId, DateTime.Now);
                this.Close();
                MainWindow mainWindow = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
                if (mainWindow != null)
                {
                    mainWindow.frmMain.Content = new HomePage();
                }
            }
        }

        private void btnCancelPreOrder_Click(object sender, RoutedEventArgs e)
        {
            if (lvPreOrder.SelectedItem == null)
            {
                MessageBox.Show("Please select a pre-order to cancel.");
                return;
            }

            var preOrderId = Int32.Parse(Id.Text);
            if (preOrderId == 0 || preOrderId == null)
            {
                MessageBox.Show("Please select a pre-order to cancel.");
                return;
            }

            ITableRepository tableRepository = new TableRepository();
            if (MessageBox.Show("Do you want to cancel Pre-Order", "Cancel Pre-Order", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                if (tableRepository.deletePreOrder(preOrderId))
                {
                    MessageBox.Show("Cancel Pre-Order success.", "Cancel Pre-Order");
                    LoadTableDetails();
                    tableRepository.setTableActive(tableId, false);
                }
                else
                {
                    MessageBox.Show("Failed to cancel Pre-Order.");
                }
            }
        }

        private void lvPreOrder_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
        }
    }
}
