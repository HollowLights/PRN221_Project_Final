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
    }
}
