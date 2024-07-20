using Library.DataAccess;
using Library.Respository;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Automation.Peers;
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
    /// Interaction logic for ProductMenuWindow.xaml
    /// </summary>
    public partial class ProductMenuWindow : Window
    {

        IProductRepository repository;
        IOrderRepository orderRepository;
        private int minPage = 1;
        private int maxPage = 3;
        private int size;
        private int numberOfPage;
        private bool btnMoreLeftOn = false;
        private bool btnMoreRightOn = true;
        private int page = 1;
        private int recordOfPage = 4;

        private List<dynamic> productList;
        private string category = "";
        private string textSearch = "";
        private string orderBy = "";

        private int orderId;
        private int quantityOfTextbox;


        public ProductMenuWindow(int _orderId)
        {
            InitializeComponent();
            repository = new ProductRepository();
            orderRepository = new OrderRepository();
            orderId = _orderId;
            List<dynamic> products = repository.getProductListInOrder(orderId);
            lvProducts.ItemsSource = products;
            productList = products;
            loadProductPage();
            btnPre.IsEnabled = false;
            Closed += ProductMenuWindow_Closed;
        }

        private void ProductMenuWindow_Closed(object? sender, EventArgs e)
        {
            MainWindow mainWindow = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
            if (mainWindow != null)
            {
                mainWindow.Opacity = 1;
            }
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
            loadLvProductPagging();
        }

        private void loadLvProductPagging()
        {
            int startIndex = recordOfPage * (page - 1);
            int endIndex = startIndex + recordOfPage;
            if (endIndex >= productList.Count) endIndex = productList.Count;
            List<dynamic> _list = new List<dynamic>();
            for (int i = startIndex; i < endIndex; i++)
            {
                _list.Add(productList[i]);
            }
            lvProducts.ItemsSource = _list;
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
            loadLvProductPagging();
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
            productList = repository.getProductListItemByFilter(textSearch, category, orderBy, orderId);
            loadProductPage();
        }

        private void loadProductPage()
        {
            page = 1;
            size = productList.Count;
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

        private void cbCategory_DropDownClosed(object sender, EventArgs e)
        {
            ComboBoxItem item = cbCategory.SelectedItem as ComboBoxItem;
            if (item.Tag.ToString() != "0")
            {
                category = item.Tag.ToString();
            }
            else
            {
                category = "";
            }
            productList = repository.getProductListItemByFilter(textSearch, category, orderBy, orderId);
            loadProductPage();
        }

        private void cbOrder_DropDownClosed(object sender, EventArgs e)
        {
            ComboBoxItem item = cbOrder.SelectedItem as ComboBoxItem;
            if (item.Tag.ToString() != "0")
            {
                orderBy = item.Tag.ToString();
            }
            else
            {
                orderBy = "";
            }
            productList = repository.getProductListItemByFilter(textSearch, category, orderBy, orderId);
            loadProductPage();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            int productId = int.Parse(btn.Tag.ToString());
            Product product = repository.getProductById(productId);
            StackPanel parentBtn = btn.Parent as StackPanel;
            TextBox tbQuantity = parentBtn.Children.OfType<TextBox>().FirstOrDefault();
            int quantity = int.Parse(tbQuantity.Text);
            quantity += 1;
            if (product.UnitsInStock > 0)
            {
                OrderDetail orderDetail = new OrderDetail
                {
                    OrderId = orderId,
                    ProductId = productId,
                    Quantity = quantity
                };
                if (orderRepository.updateOrderDetail(orderDetail))
                {
                    Product p = repository.getProductById(productId);
                    p.UnitsInStock = p.UnitsInStock - 1;
                    repository.EditProduct(p);
                }
                productList = repository.getProductListItemByFilter(textSearch, category, orderBy, orderId);
                loadLvProductPagging();
            }
        }

        private void btnSub_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            int productId = int.Parse(btn.Tag.ToString());
            Product product = repository.getProductById(productId);
            StackPanel parentBtn = btn.Parent as StackPanel;
            TextBox tbQuantity = parentBtn.Children.OfType<TextBox>().FirstOrDefault();
            int quantity = int.Parse(tbQuantity.Text);
            quantity -= 1;
            if (quantity >= 0)
            {
                OrderDetail orderDetail = new OrderDetail
                {
                    OrderId = orderId,
                    ProductId = productId,
                    Quantity = quantity
                };
                if (orderRepository.updateOrderDetail(orderDetail))
                {
                    product.UnitsInStock = product.UnitsInStock + 1;
                    repository.EditProduct(product);
                }
                productList = repository.getProductListItemByFilter(textSearch, category, orderBy, orderId);
                loadLvProductPagging();
            }
        }

        private void tbQuantity_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox tbQuantity = (TextBox)sender;
            string quantity = tbQuantity.Text;
            quantityOfTextbox = int.Parse(quantity);
        }

        private void tbQuantity_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox tbQuantity = (TextBox)sender;
            int productId = int.Parse(tbQuantity.Tag.ToString());
            Product product = repository.getProductById(productId);
            string quantity = tbQuantity.Text;
            if (!quantity.Trim().Equals(""))
            {
                int newQuantity;
                try
                {
                    newQuantity = int.Parse(quantity);
                }
                catch (Exception ex)
                {
                    newQuantity = quantityOfTextbox;
                }
                int subQuantity = newQuantity - quantityOfTextbox;
                if (newQuantity >= 0 && newQuantity <= (product.UnitsInStock + quantityOfTextbox))
                {
                    OrderDetail orderDetail = new OrderDetail
                    {
                        OrderId = orderId,
                        ProductId = productId,
                        Quantity = newQuantity
                    };
                    if (orderRepository.updateOrderDetail(orderDetail))
                    {
                        product.UnitsInStock = product.UnitsInStock + quantityOfTextbox - newQuantity;
                        repository.EditProduct(product);
                    }
                    productList = repository.getProductListItemByFilter(textSearch, category, orderBy, orderId);
                    loadLvProductPagging();
                }
                else
                {
                    tbQuantity.Text = quantityOfTextbox.ToString();
                }
            }
            else
            {
                tbQuantity.Text = quantityOfTextbox.ToString();
            }
        }

        private void tbQuantity_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            TextBox tbQuantity = (TextBox)sender;
            tbQuantity.LostFocus -= tbQuantity_LostFocus;

            if (e.Key == Key.Enter)
            {
                tbQuantity_LostFocus(sender, e);
            }

            tbQuantity.LostFocus += tbQuantity_LostFocus;
        }
    }
}
