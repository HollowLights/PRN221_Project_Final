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
    /// Interaction logic for ProductsPage.xaml
    /// </summary>
    public partial class ProductsPage : Page
    {
        IProductRepository repository;
        private int minPage = 1;
        private int maxPage = 3;
        private int size;
        private int numberOfPage;
        private bool btnMoreLeftOn = false;
        private bool btnMoreRightOn = true;
        private int page = 1;

        private List<dynamic> productList;
        private string category = "";
        private string textSearch = "";
        private string orderBy = "";


        public ProductsPage()
        {
            InitializeComponent();
            repository = new ProductRepository();
            List<dynamic> products = repository.getProductList();
            lvProducts.ItemsSource = products;
            productList = products;
            size = products.Count;
            if (size % 5 == 0)
            {
                numberOfPage = size / 5;
            }
            else { numberOfPage = size / 5 + 1; }
            InitializeStpPagging();
            changePage();
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
            loadLvProductPagging();
        }

        private void loadLvProductPagging()
        {
            int startIndex = 5 * (page - 1);
            int endIndex = startIndex + 5;
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
            productList = repository.getProductByFilter(textSearch, category, orderBy);
            loadProductPage();
        }

        private void loadProductPage()
        {
            page = 1;
            size = productList.Count;
            if (size % 5 == 0)
            {
                numberOfPage = size / 5;
            }
            else { numberOfPage = size / 5 + 1; }
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
            productList = repository.getProductByFilter(textSearch, category, orderBy);
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
            productList = repository.getProductByFilter(textSearch, category, orderBy);
            loadProductPage();
        }

        private void btnAddProduct_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
            mainWindow.Opacity = 0.2;
            AddProductWindow addProductWindow = new AddProductWindow();
            addProductWindow.ShowDialog();
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            int id = Convert.ToInt32(btn.Tag);
            MainWindow mainWindow = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
            mainWindow.Opacity = 0.2;
            EditProductWindow editProductWindow = new EditProductWindow(id);
            editProductWindow.ShowDialog();
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            int id = Convert.ToInt32(btn.Tag);

            if (MessageBox.Show("Do you want delete Product", "Delete Product", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                if (repository.deleteProduct(id))
                {
                    MessageBox.Show("Delete success.", "Delete Product");
                    productList = repository.getProductList();
                    loadProductPage();
                }
            }
        }
    }
}
