using Library.DataAccess;
using Library.Respository;
using Microsoft.Win32;
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
using System.IO;

namespace Final_Project_PRN221
{
    /// <summary>
    /// Interaction logic for AddProductWindow.xaml
    /// </summary>
    public partial class AddProductWindow : Window
    {
        IProductRepository productRepository;
        private bool isAdd = false;
        private bool isSelectedImage = false;

        private string sourceFile;
        private string fileName;
        public AddProductWindow()
        {
            InitializeComponent();
            productRepository = new ProductRepository();
            Closed += AddProductWindow_Closed;
        }

        private void AddProductWindow_Closed(object? sender, EventArgs e)
        {
            MainWindow window = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
            if (window != null)
            {
                window.Opacity = 1;
            }
        }

        private void btnImage_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files|*.bmp;*.jpg;*.png";
            openFileDialog.FilterIndex = 1;
            if (openFileDialog.ShowDialog() == true)
            {
                string _fileName = System.IO.Path.GetFileName(openFileDialog.FileName);
                fileName = _fileName;
                sourceFile = openFileDialog.FileName;

                lbImage.Content = "Images: " + _fileName;
                imageView.Visibility = Visibility.Visible;
                imageView.Source = new BitmapImage(new Uri(openFileDialog.FileName));
                if (!isSelectedImage)
                {
                    this.Height += 150;
                    Top -= 75;
                    isSelectedImage = true;
                }
            }
        }


        private void btnCancle_Click(object sender, RoutedEventArgs e)
        {
            if (isAdd)
            {
                this.Close();
            }
            else
            {
                if (MessageBox.Show("Exit?", "Add Employee Account", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    this.Close();
                }
            }
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            string name = txtName.Text;
            string _price = txtUnitPrice.Text;
            string _stock = txtUnitsInStock.Text;
            int category = 1;
            string _category = cbCategory.Text.Trim();
            if (_category.Equals("Drink"))
            {
                category = 1;
            }
            else if (_category.Equals("Food"))
            {
                category = 2;
            }
            else if (_category.Equals("Cues"))
            {
                category = 3;
            }
            else if (_category.Equals("Other"))
            {
                category = 4;
            }

            if (string.IsNullOrEmpty(name)
                ||string.IsNullOrEmpty(_price)
                ||string.IsNullOrEmpty(_stock)
                ||string.IsNullOrEmpty(fileName))
            {
                tbAdd.Text = "All fields cannot be left blank.";
            }
            else
            {
                string resourceUri = "..//..//..//Images//" + fileName;

                if (!System.IO.File.Exists(resourceUri))
                {
                    System.IO.File.Copy(sourceFile, resourceUri, true);
                }

                decimal price = Convert.ToDecimal(txtUnitPrice.Text);
                int stock = Convert.ToInt32(txtUnitsInStock.Text);
                Product product = new Product()
                {
                    Id = 0,
                    Name = name,
                    UnitPrice = price,
                    UnitsInStock = stock,
                    Image = fileName,
                    CategoryId = category
                };

                if (productRepository.addProduct(product))
                {
                    tbAdd.Foreground = new SolidColorBrush(Colors.LightSeaGreen);
                    tbAdd.Text = "Add Product successfully.";
                    MainWindow window = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
                    window.frmMain.Content = new ProductsPage();
                    isAdd = true;
                }
            }
        }
    }
}
