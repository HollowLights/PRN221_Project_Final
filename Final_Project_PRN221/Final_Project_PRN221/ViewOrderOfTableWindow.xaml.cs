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
    /// Interaction logic for ViewOrderOfTable.xaml
    /// </summary>
    public partial class ViewOrderOfTable : Window
    {
        IOrderRepository orderRepository;
        ITableRepository tableRepository;
        private int orderId;
        private int accountId;
        public ViewOrderOfTable(int _orderId)
        {
            InitializeComponent();
            orderId = _orderId;
            orderRepository = new OrderRepository();
            tableRepository = new TableRepository();
            lvOrderDetail.ItemsSource = orderRepository.getOrderDetailById(orderId);
            Closed += OrderDetailWindow_Closed;
            double serviceFee = orderRepository.getServiceFee(orderId);
            lbServiceFee.Content = serviceFee;

            double tableFee = orderRepository.getCurrentTableFee(orderId,DateTime.Now);
            lbTableFee.Content = Math.Round(tableFee,2);

            double total = (serviceFee + tableFee);
            lbTotal.Content = Math.Round(total, 2);
            accountId = MainWindow.account.Id;
        }

        private void OrderDetailWindow_Closed(object? sender, EventArgs e)
        {
            MainWindow mainWindow = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
            if (mainWindow != null)
            {
                mainWindow.Opacity = 1;
            }
        }

        private void btnBacK_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnPay_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Do you want pay?", "Payment", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                Order order =   orderRepository.getOrderById(orderId);
                order.EndTime = DateTime.Now;
                order.Discount = int.Parse(tbDiscount.Text);
                order.OrderBy = accountId;
                if(orderRepository.updateOrder(order))
                {
                    tableRepository.setTableActive(order.TableId, false);
                    this.Close();
                    MainWindow mainWindow = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
                    if (mainWindow != null)
                    {
                        mainWindow.frmMain.Content = new HomePage();
                    }
                }
            }
        }

        private void tbDiscount_LostFocus(object sender, RoutedEventArgs e)
        {
            double discount = 0;
            try
            {
                discount = double.Parse(tbDiscount.Text);

            }catch(Exception ex)
            {
                discount = 0;
            }
            double total = (double.Parse(lbServiceFee.Content.ToString())
                + double.Parse(lbTableFee.Content.ToString())) * (1 - discount/100) ;
            lbTotal.Content = total;
        }

        private void tbDiscount_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            tbDiscount.LostFocus -= tbDiscount_LostFocus;
            if (e.Key == Key.Enter)
            {
                tbDiscount_LostFocus(sender, e);
            }
            tbDiscount.LostFocus += tbDiscount_LostFocus;
        }
    }
}
