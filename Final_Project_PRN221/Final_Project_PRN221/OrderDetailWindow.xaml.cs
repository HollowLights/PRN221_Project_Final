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
    /// Interaction logic for OrderDetailWindow.xaml
    /// </summary>
    public partial class OrderDetailWindow : Window
    {
        IOrderRepository orderRepository;
        public OrderDetailWindow(int orderId)
        {
            InitializeComponent();
            orderRepository = new OrderRepository();
            lvOrderDetail.ItemsSource = orderRepository.getOrderDetailById(orderId);
            Closed += OrderDetailWindow_Closed;
            double serviceFee = orderRepository.getServiceFee(orderId);
            lbServiceFee.Content = serviceFee;
            
            double tableFee = orderRepository.getTableFee(orderId);
            lbTableFee.Content = tableFee;

            double discount = orderRepository.getDiscount(orderId);
            lbDiscount.Content = discount.ToString();

            double total = (serviceFee + tableFee) * (1 - discount / 100);
            lbTotal.Content = Math.Round(total,2);

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
    }
}
