using Library.DataAccess;
using Library.Respository;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using VietQRPaymentAPI;

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

            double tableFee = orderRepository.getCurrentTableFee(orderId, DateTime.Now);
            lbTableFee.Content = Math.Round(tableFee, 2);

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
            double serviceFee = orderRepository.getServiceFee(orderId);
            lbServiceFee.Content = serviceFee;

            double tableFee = orderRepository.getCurrentTableFee(orderId, DateTime.Now);
            lbTableFee.Content = Math.Round(tableFee, 2);

            double total = (serviceFee + tableFee);
            if (MessageBox.Show("Do you want to pay?", "Payment", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                Order order = orderRepository.getOrderById(orderId);
                order.EndTime = DateTime.Now;
                order.Discount = int.Parse(tbDiscount.Text);
                order.OrderBy = accountId;

                if (orderRepository.updateOrder(order))
                {
                    tableRepository.setTableActive(order.TableId, false);

                    // Kiểm tra tổng số tiền
                    if (total > 200000)
                    {
                        if (MessageBox.Show("Bạn có muốn quay vòng quay may mắn không?", "Lucky Spin", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                        {
                            // Hiển thị trang vòng quay may mắn
                            LuckySpinPage luckySpinPage = new LuckySpinPage();
                            NavigationService navService = NavigationService.GetNavigationService(this);
                            if (navService != null)
                            {
                                navService.Navigate(luckySpinPage);
                            }
                            else
                            {
                                // Nếu không thể lấy NavigationService, tạo một NavigationWindow mới
                                NavigationWindow navWindow = new NavigationWindow
                                {
                                    Content = luckySpinPage
                                };
                                navWindow.Show();
                            }
                        }
                    }

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

            }
            catch (Exception ex)
            {
                discount = 0;
            }
            double total = (double.Parse(lbServiceFee.Content.ToString())
                + double.Parse(lbTableFee.Content.ToString())) * (1 - discount / 100);
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

        private void btnQr_Click(object sender, RoutedEventArgs e)
        {
            var apiRequest = new ApiRequest
            {
                acqId = 970418,
                accountNo = 4511024639,
                accountName = "Bida Club",
                amount = Convert.ToInt32(double.Parse(lbTotal.Content.ToString())),
                format = "text",
                template = "compact2",
            };

            var jsonRequest = JsonConvert.SerializeObject(apiRequest);
            var client = new RestClient("https://api.vietqr.io/v2/generate");
            var request = new RestRequest
            {
                Method = Method.Post
            };
            request.AddHeader("Accept", "application/json");
            request.AddParameter("application/json", jsonRequest, ParameterType.RequestBody);

            var response = client.Execute(request);
            var content = response.Content;
            var dataResult = JsonConvert.DeserializeObject<ApiResponse>(content);

            if (dataResult?.data?.qrDataURL != null)
            {
                // Extract base64 string by removing the data URL prefix
                var base64String = dataResult.data.qrDataURL.Replace("data:image/png;base64,", "");

                // Convert base64 string to BitmapImage
                var image = Base64ToBitmapImage(base64String);
                qrImage.Source = image;
            }
        }
        public BitmapImage Base64ToImage(string base64String)
        {
            byte[] imageBytes = Convert.FromBase64String(base64String);
            using (var ms = new MemoryStream(imageBytes))
            {
                var image = new BitmapImage();
                image.BeginInit();
                image.StreamSource = ms;
                image.CacheOption = BitmapCacheOption.OnLoad;
                image.EndInit();
                return image;
            }
        }

        public byte[] Base64ToByteArray(string base64String)
        {
            return Convert.FromBase64String(base64String);
        }

        public BitmapImage ByteArrayToBitmapImage(byte[] byteArray)
        {
            using (var stream = new MemoryStream(byteArray))
            {
                var bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.StreamSource = stream;
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.EndInit();
                bitmapImage.Freeze(); // Freeze to make it cross-thread accessible
                return bitmapImage;
            }
        }

        public BitmapImage Base64ToBitmapImage(string base64String)
        {
            var byteArray = Base64ToByteArray(base64String);
            return ByteArrayToBitmapImage(byteArray);
        }


    }
}

