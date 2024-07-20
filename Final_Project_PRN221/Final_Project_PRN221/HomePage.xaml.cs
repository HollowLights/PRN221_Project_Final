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
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Final_Project_PRN221
{
    /// <summary>
    /// Interaction logic for HomePage.xaml
    /// </summary>
    public partial class HomePage : Page
    {
        ITableRepository repository;
        IOrderRepository orderRepository;
        private static int numberOfTableInRow { get; set; }
        private static List<Table> tableList;
        private static List<Table> tableListFilter;

        public HomePage()
        {
            repository = new TableRepository();
            orderRepository = new OrderRepository();
            numberOfTableInRow = 4;
            tableList = repository.getListTable();
            tableListFilter = repository.getListTable();
            InitializeComponent();
            InitializeUI(tableList);
        }

        private void InitializeUI(List<Table> tableList)
        {
            if (tableList.Count == 0)
            {
                scrTable.Content = null;
                return;
            }
            //Select item have content = numberTableInRow
            foreach (ComboBoxItem item in cbTableInRow.Items)
            {
                if (item.Content.Equals(numberOfTableInRow.ToString()))
                {
                    cbTableInRow.SelectedItem = item;
                }
            }
            StackPanel outerStackPanel = getOuterStackPanel(tableList);
            scrTable.Content = outerStackPanel;
        }

        private StackPanel getOuterStackPanel(List<Table> tableList)
        {
            StackPanel outerStackPanel = new StackPanel();
            outerStackPanel.Orientation = Orientation.Vertical;
            outerStackPanel.Margin = new Thickness(100, 0, 100, 100);

            int i = 0;
            int tableCount = 0;
            StackPanel stackRow = new StackPanel();
            stackRow.Orientation = Orientation.Horizontal;
            foreach (Table table in tableList)
            {
                StackPanel innerStackPanel = new StackPanel();
                i++;
                tableCount++;
                innerStackPanel.Height = 300;
                innerStackPanel.Width = (System.Windows.SystemParameters.PrimaryScreenWidth - 200) / numberOfTableInRow;

                TextBlock textBlock = new TextBlock();
                textBlock.Text = table.Name;
                textBlock.HorizontalAlignment = HorizontalAlignment.Center;
                textBlock.FontSize = 20;
                textBlock.FontWeight = FontWeights.Bold;
                textBlock.Margin = new Thickness(0, 5, 0, 0);
                innerStackPanel.Children.Add(textBlock);

                Image image = new Image();
                image.Source = new BitmapImage(new Uri("Images/" + table.Image, UriKind.Relative));
                image.Name = "Image" + table.Id;
                image.Width = 200;
                innerStackPanel.Children.Add(image);

                StackPanel buttonPanel = new StackPanel();
                buttonPanel.Orientation = Orientation.Horizontal;
                buttonPanel.HorizontalAlignment = HorizontalAlignment.Center;

                Button btnStart = new Button();
                btnStart.Name = "btnStart";
                btnStart.Content = "Start";
                btnStart.Tag = table.Id;
                btnStart.Padding = new Thickness(5);
                btnStart.MinWidth = 70;
                btnStart.Margin = new Thickness(10, 5, 10, 0);
                buttonPanel.Children.Add(btnStart);

                Button btnOrder = new Button();
                btnOrder.Visibility = Visibility.Collapsed;
                btnOrder.Name = "btnOrder";
                btnOrder.Content = "Orders";
                btnOrder.Tag = table.Id;
                btnOrder.Padding = new Thickness(5);
                btnOrder.MinWidth = 70;
                btnOrder.Margin = new Thickness(10, 5, 10, 0);
                btnOrder.Click += BtnOrder_Click;
                buttonPanel.Children.Add(btnOrder);

                if (table.IsOn == true)
                {
                    btnStart.Background = new SolidColorBrush(Colors.GreenYellow);
                    btnStart.Content = "View";
                    image.Opacity = 0.5;
                    btnOrder.Visibility = Visibility.Visible;
                    btnStart.Click += BtnView_Click;
                }
                else
                {
                    btnStart.Click += BtnStart_Click;
                }

                innerStackPanel.Children.Add(buttonPanel);

                stackRow.Children.Add(innerStackPanel);

                if (i == numberOfTableInRow || tableCount == tableList.Count)
                {
                    outerStackPanel.Children.Add(stackRow);
                    stackRow = new StackPanel();
                    stackRow.Orientation = Orientation.Horizontal;
                    i = 0;
                }
            }
            return outerStackPanel;
        }

        private void BtnView_Click(object sender, RoutedEventArgs e)
        {
            Button btnOrder = (Button)sender;
            int tableId = int.Parse(btnOrder.Tag.ToString());
            int orderId = orderRepository.getOrderOfTableOn(tableId);
            MainWindow mainWindow = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
            if (mainWindow != null)
            {
                mainWindow.Opacity = 0.2;
            }
            ViewOrderOfTable viewOrderOfTable = new ViewOrderOfTable(orderId);
            viewOrderOfTable.ShowDialog();
        }

        private void BtnOrder_Click(object sender, RoutedEventArgs e)
        {
            Button btnOrder = (Button)sender;
            int tableId = int.Parse(btnOrder.Tag.ToString());
            int orderId = orderRepository.getOrderOfTableOn(tableId);
            MainWindow mainWindow = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
            if (mainWindow != null)
            {
                mainWindow.Opacity = 0.2;
            }
            ProductMenuWindow productMenuWindow = new ProductMenuWindow(orderId);
            productMenuWindow.ShowDialog();
        }

        private void BtnStart_Click(object sender, RoutedEventArgs e)
        {
            Button btnStart = sender as Button;
            int tableId = int.Parse(btnStart.Tag.ToString());
            if (orderRepository.createNewOrder(tableId))
            {
                repository.setTableActive(tableId, true);
                StackPanel parentBtn = (StackPanel)btnStart.Parent;
                StackPanel parentBtnSP = (StackPanel)parentBtn.Parent;
                Image image = parentBtnSP.Children.OfType<Image>().FirstOrDefault();
                Button btnOrder;
                foreach (Button btn in parentBtn.Children)
                {
                    if(btn != btnStart)
                    {
                        btnOrder = (Button)btn;
                        btnOrder.Visibility = Visibility.Visible;
                    }
                }
                image.Opacity = 0.5;
                btnStart.Background = new SolidColorBrush(Colors.GreenYellow);
                btnStart.Content = "View";
            }
            btnStart.Click += BtnView_Click;
            btnStart.Click -= BtnStart_Click;
        }

        private void txtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox tbSearch = sender as TextBox;
            string text = tbSearch.Text;
            tableListFilter.Clear();
            foreach (Table table in tableList)
            {
                if (table.Name.ToLower().Contains(text.ToLower()))
                {
                    tableListFilter.Add(table);
                }
            }
            scrTable.Content = getOuterStackPanel(tableListFilter);
        }

        private void cbTableInRow_DropDownClosed(object sender, EventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            ComboBoxItem selectedItem = (ComboBoxItem)comboBox.SelectedItem;
            comboBox.SelectedItem = selectedItem;
            numberOfTableInRow = Int32.Parse(selectedItem.Content.ToString());
            scrTable.Content = getOuterStackPanel(tableListFilter);
        }

        private void cbActive_DropDownClosed(object sender, EventArgs e)
        {
            ComboBoxItem item = (ComboBoxItem)cbActive.SelectedItem;
            string active = item.Tag.ToString();
            if (active.Equals("0"))
            {
                tableList = repository.getListTable();
            }
            else if (active.Equals("1"))
            {
                tableList = repository.getListTable().Where(o => o.IsOn == true).ToList();
            }
            else if (active.Equals("2"))
            {
                tableList = repository.getListTable().Where(o => o.IsOn == false).ToList();
            }

            scrTable.Content = getOuterStackPanel(tableList);

        }
    }
}
