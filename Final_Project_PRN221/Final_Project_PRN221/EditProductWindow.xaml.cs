using Library.DataAccess;
using Library.Respository;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
    /// Interaction logic for EditProductWindow.xaml
    /// </summary>
    public partial class EditProductWindow : Window
    {
        IProductRepository productRepository;
        private bool isUpdate = false;
        private int id;

        private string sourceFile;
        private string fileName;
        public EditProductWindow(int _id)
        {
            InitializeComponent();
            productRepository = new ProductRepository();
            Product product = productRepository.getProductById(_id);
            id = _id;

            txtName.Text = product.Name;
            int categoryId = product.CategoryId;
            string category = "";
            if (categoryId == 1)
            {
                category = "Drink";
            }
            else if (categoryId == 2)
            {
                category = "Food";
            }
            else if (categoryId == 3)
            {
                category = "Cues";
            }
            else if (categoryId == 4)
            {
                category = "Other";
            }

            foreach (ComboBoxItem item in cbCategory.Items)
            {
                if (item.Content.Equals(category))
                {
                    cbCategory.SelectedItem = item;
                }
            }

            fileName = product.Image;
            imageView.Source = new BitmapImage(new Uri("Images/"+product.Image, UriKind.Relative));

            txtUnitPrice.Text = product.UnitPrice.ToString();
            txtUnitsInStock.Text = product.UnitsInStock.ToString();
            Closed += EditProductWindow_Closed;
        }

        private void EditProductWindow_Closed(object? sender, EventArgs e)
        {
            MainWindow window = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
            if (window != null)
            {
                window.Opacity = 1;
            }
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
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
                || string.IsNullOrEmpty(_price)
                || string.IsNullOrEmpty(_stock))
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
                    Id = id,
                    Name = name,
                    UnitPrice = price,
                    UnitsInStock = stock,
                    Image = fileName,
                    CategoryId = category
                };

                if (productRepository.EditProduct(product))
                {
                    tbAdd.Foreground = new SolidColorBrush(Colors.LightSeaGreen);
                    tbAdd.Text = "Update Product successfully.";
                    MainWindow window = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
                    window.frmMain.Content = new ProductsPage();
                    isUpdate = true;
                }
            }
        }

        private void btnCancle_Click(object sender, RoutedEventArgs e)
        {
            if (isUpdate)
            {
                this.Close();
            }
            else
            {
                if (MessageBox.Show("Exit?", "Update Product", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    this.Close();
                }
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
                imageView.Source = new BitmapImage(new Uri(openFileDialog.FileName));
            }
        }
    }
}
