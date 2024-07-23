using Library.DataAccess;
using Library.Respository;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
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
    /// Interaction logic for OrdersPage.xaml
    /// </summary>
    public partial class OrdersPage : Page
    {
        IOrderRepository repository;
        private int minPage = 1;
        private int maxPage = 3;
        private int size;
        private int numberOfPage;
        private bool btnMoreLeftOn = false;
        private bool btnMoreRightOn = true;
        private int page = 1;
        private int recordOfPage = 10;

        private List<dynamic> orderList;
        private string textSearch = "";
        private bool sortByDate = true;


        public OrdersPage()
        {
            InitializeComponent();
            repository = new OrderRepository();
            List<dynamic> orders = repository.getOrderList();
            lvOrders.ItemsSource = orders;
            orderList = orders;
            loadOrderPage();
            btnPre.IsEnabled = false;
        }

        private void InitializeStpPagging()
        {
            stpPagging.Children.Clear();
            if (numberOfPage <= 3)
            {
                for (int i = 1; i <= numberOfPage; i++)
                {
                    Button btn = new Button();
                    btn.Name = "btnPagging" + i;
                    btn.Content = i;
                    btn.Width = 30;
                    btn.Margin = new Thickness(5, 0, 5, 0);
                    btn.Click += btnPage_Click;
                    stpPagging.Children.Add(btn);
                }
            }
            else
            {
                if (btnMoreLeftOn)
                {
                    Button btnMore = new Button();
                    btnMore.Name = "btnPaggingMoreLeft";
                    btnMore.Content = "...";
                    btnMore.Width = 30;
                    btnMore.Margin = new Thickness(5, 0, 5, 0);
                    btnMore.Click += btnPaggingMoreLeft_Click;
                    stpPagging.Children.Add(btnMore);
                }
                for (int i = minPage; i <= maxPage; i++)
                {
                    Button btn = new Button();
                    btn.Name = "btnPagging" + i;
                    btn.Content = i;
                    btn.Width = 30;
                    btn.Margin = new Thickness(5, 0, 5, 0);
                    btn.Click += btnPage_Click;
                    stpPagging.Children.Add(btn);
                }
                if (btnMoreRightOn)
                {
                    Button btnMore = new Button();
                    btnMore.Name = "btnPaggingMoreRight";
                    btnMore.Content = "...";
                    btnMore.Width = 30;
                    btnMore.Margin = new Thickness(5, 0, 5, 0);
                    btnMore.Click += btnPaggingMoreRight_Click;
                    stpPagging.Children.Add(btnMore);
                }
            }

        }

        private void btnPage_Click(object sender, RoutedEventArgs e)
        {
            foreach (Button btn in stpPagging.Children)
            {
                btn.ClearValue(Button.BackgroundProperty);
            }
            Button thisBtn = sender as Button;
            if (thisBtn != null)
            {
                thisBtn.Background = new SolidColorBrush(Colors.LightBlue);
                page = Convert.ToInt32(thisBtn.Content);
                btnPre.IsEnabled = true;
                btnNext.IsEnabled = true;
                if (page == 1)
                {
                    btnPre.IsEnabled = false;
                }
                else if (page == numberOfPage)
                {
                    btnNext.IsEnabled = false;
                }
            }
            loadLvOrderPagging();
        }

        private void loadLvOrderPagging()
        {
            int startIndex = recordOfPage * (page - 1);
            int endIndex = startIndex + recordOfPage;
            if (endIndex >= orderList.Count) endIndex = orderList.Count;
            List<dynamic> _list = new List<dynamic>();
            for (int i = startIndex; i < endIndex; i++)
            {
                _list.Add(orderList[i]);
            }
            lvOrders.ItemsSource = _list;
        }

        private void btnPaggingMoreRight_Click(object sender, RoutedEventArgs e)
        {
            btnPre.IsEnabled = true;
            btnMoreLeftOn = true;
            if (numberOfPage - maxPage < 3)
            {
                int numberOfLastPage = numberOfPage - maxPage;
                maxPage += numberOfLastPage;
                minPage = maxPage - 2;
                btnMoreRightOn = false;
            }
            else
            {
                maxPage += 3;
                minPage += 3;
            }
            InitializeStpPagging();
            if (page < minPage)
            {
                page = minPage;
            }
            changePage();
        }

        private void btnPaggingMoreLeft_Click(object sender, RoutedEventArgs e)
        {
            btnNext.IsEnabled = true;
            btnMoreRightOn = true;
            if (minPage <= 3)
            {
                int numberOfFirstPage = minPage - 1;
                minPage -= numberOfFirstPage;
                maxPage = minPage + 2;
                btnMoreLeftOn = false;
            }
            else
            {
                maxPage -= 3;
                minPage -= 3;
            }
            InitializeStpPagging();
            if (page > maxPage)
            {
                page = maxPage;
            }
            changePage();
        }

        private void changePage()
        {
            string pageString = page.ToString();

            foreach (Button btn in stpPagging.Children)
            {
                if (btn.Content.ToString() == pageString)
                {
                    btn.Background = new SolidColorBrush(Colors.LightBlue);
                }
                else
                {
                    btn.ClearValue(Button.BackgroundProperty);
                }
            }
            loadLvOrderPagging();
        }

        private void btnPre_Click(object sender, RoutedEventArgs e)
        {
            if (page > 1)
            {
                if (page == 2)
                {
                    btnPre.IsEnabled = false;
                }
                if (page <= (numberOfPage - 2))
                {
                    btnMoreRightOn = true;
                }
                if (minPage == 2)
                {
                    btnMoreLeftOn = false;
                }
                page -= 1;
                if (page < minPage)
                {
                    minPage = page;
                    maxPage = minPage + 2;
                }
                InitializeStpPagging();
                changePage();
                btnNext.IsEnabled = true;
            }
        }

        private void btnNext_Click(object sender, RoutedEventArgs e)
        {
            if (page < numberOfPage)
            {
                if (page == numberOfPage - 1)
                {
                    btnNext.IsEnabled = false;

                }
                if (page >= 3)
                {
                    btnMoreLeftOn = true;
                }
                if (maxPage == (numberOfPage - 1))
                {
                    btnMoreRightOn = false;
                }
                page += 1;
                if (page > maxPage)
                {
                    maxPage = page;
                    minPage = maxPage - 2;
                }
                InitializeStpPagging();
                changePage();
                btnPre.IsEnabled = true;
            }
        }

        private void txtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            textSearch = txtSearch.Text.ToLower();
            orderList = repository.getOrderByFilter(textSearch, sortByDate);
            loadOrderPage();
        }

        private void loadOrderPage()
        {
            page = 1;
            size = orderList.Count;
            if (size % recordOfPage == 0)
            {
                numberOfPage = size / recordOfPage;
            }
            else { numberOfPage = size / recordOfPage + 1; }
            InitializeStpPagging();
            changePage();
            if (numberOfPage > 1)
            {
                btnNext.IsEnabled = true;
            }
            else btnNext.IsEnabled = false;
            btnPre.IsEnabled = false;
        }

        private void cbOrder_DropDownClosed(object sender, EventArgs e)
        {
            ComboBoxItem item = cbSort.SelectedItem as ComboBoxItem;
            if (item.Tag.ToString().Equals("1"))
            {
                sortByDate = true;
            }
            else if (item.Tag.ToString().Equals("0"))
            {
                sortByDate = false;
            }
            orderList = repository.getOrderByFilter(textSearch, sortByDate);
            loadOrderPage();
        }

        private void btnViewDetails_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            int orderId = int.Parse(btn.Tag.ToString());

            OrderDetailWindow orderDetailWindow = new OrderDetailWindow(orderId);
            MainWindow mainWindow = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
            mainWindow.Opacity = 0.2;
            orderDetailWindow.ShowDialog();
        }

        private void btnExportOrders_Click(object sender, RoutedEventArgs e)
        {
            ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
            List<Order> orders = repository.getOrderByStartTime(DateTime.Today);
            if (!orders.Any())
            {
                MessageBox.Show("No orders for today to export.");
                return;
            }

            /*            string fileName = $"Orders_{DateTime.Now:yyyyMMdd}.xlsx";
            */
            string fileName = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), $"Orders_{DateTime.Now:yyyyMMdd}.xlsx");


            // Tạo một file Excel mới
            using (var package = new ExcelPackage())
            {
                // Thêm một worksheet
                var worksheet = package.Workbook.Worksheets.Add("Orders");

                // Thêm tiêu đề cột
                worksheet.Cells[1, 1].Value = "Id";
                worksheet.Cells[1, 2].Value = "Table";
                worksheet.Cells[1, 3].Value = "Discount";
                worksheet.Cells[1, 4].Value = "StartTime";
                worksheet.Cells[1, 5].Value = "EndTime";
                worksheet.Cells[1, 6].Value = "PlayTime";
                worksheet.Cells[1, 7].Value = "Total";
                worksheet.Cells[1, 8].Value = "OrderBy";

                // Thêm dữ liệu
                int row = 2;

                foreach (var order in orders)
                {
                    worksheet.Cells[row, 1].Value = order.Id;
                    worksheet.Cells[row, 2].Value = order.Table.Name;
                    worksheet.Cells[row, 3].Value = order.Discount;
                    worksheet.Cells[row, 4].Value = order.StartTime.ToString("yyyy-MM-dd HH:mm:ss");
                    worksheet.Cells[row, 5].Value = order.EndTime.ToString("yyyy-MM-dd HH:mm:ss");
                    worksheet.Cells[row, 6].Value = getTotalHoursOrder(order.StartTime, order.EndTime);
                    worksheet.Cells[row, 7].Value = (getServiceFee(order.Id) +
                            getTotalHoursOrder(order.StartTime, order.EndTime)
                            * (Convert.ToDouble(order.Table.Type.PricePerHour))) * (1 - order.Discount / 100);
                    worksheet.Cells[row, 8].Value = order.Account.FullName;
                    row++;
                }

                // Lưu file Excel
                var file = new FileInfo(fileName);
                package.SaveAs(file);
            }

            MessageBox.Show($"Orders exported to {fileName}.");
        }

        public double getTotalHoursOrder(DateTime startTime, DateTime endTime)
        {
            double totalHours = (endTime - startTime).TotalHours;
            if (totalHours < 0.25)
            {
                totalHours = 0.25;
            }
            return Math.Round(totalHours, 2);
        }

        public double getServiceFee(int id)
        {
            double total = 0;
            using (FinalProjectPrn221Context context = new FinalProjectPrn221Context())
            {
                try
                {
                    total = (double)context.OrderDetails.Where(o => o.OrderId == id)
                        .Sum(o => o.Product.UnitPrice * o.Quantity);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return Math.Round(total, 2);
        }
    }
}
