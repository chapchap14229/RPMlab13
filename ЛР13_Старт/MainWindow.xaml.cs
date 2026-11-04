using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Lab13
{
    public partial class MainWindow : Window
    {
        private CatalogService _catalog;

        private const string FoundPrefix = "найдено: ";
        private const string DiscountsPrefix = "скидок: ";
        private const string NothingFound = "ничего";
        private const string ErrorMessage = "ошибка!!!!!!";

        public MainWindow()
        {
            InitializeComponent();

            _catalog = new CatalogService();
            _catalog.LoadSampleData();

            string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string logoPath = Path.Combine(baseDirectory, "Images", "logo.png");
            if (File.Exists(logoPath))
            {
                BitmapImage logoBitmap = new BitmapImage();
                logoBitmap.BeginInit();
                logoBitmap.UriSource = new Uri(logoPath);
                logoBitmap.CacheOption = BitmapCacheOption.OnLoad;
                logoBitmap.EndInit();
                LogoImage.Source = logoBitmap;
            }

            BtnSearch_Click(null, null);
        }

        public MainWindow(string UserName) : this()
        {
            if (!string.IsNullOrEmpty(UserName))
                UserTextBlock.Text = UserName;
        }

        private void BtnSearch_Click(object sender, RoutedEventArgs e)
        {
            ComboBoxItem manufacturerItem = ManufacturerComboBox.SelectedItem as ComboBoxItem;
            if (manufacturerItem == null)
            {
                MessageBox.Show(ErrorMessage, ErrorMessage, MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            string searchText = SearchTextBox.Text ?? "";
            string manufacturerName = manufacturerItem.Content.ToString();

            List<Product> found = _catalog.Find(searchText, manufacturerName);

            ProductsListBox.Items.Clear();
            foreach (Product product in found)
                ProductsListBox.Items.Add(product);

            StatusTextBlock.Text = FoundPrefix + found.Count;

            if (found.Count == 0)
                MessageBox.Show(NothingFound, ErrorMessage, MessageBoxButton.OK, MessageBoxImage.Warning);
        }

        private void BtnDiscounts_Click(object sender, RoutedEventArgs e)
        {
            ComboBoxItem manufacturerItem = ManufacturerComboBox.SelectedItem as ComboBoxItem;
            if (manufacturerItem == null)
            {
                MessageBox.Show(ErrorMessage, ErrorMessage, MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            string searchText = SearchTextBox.Text ?? "";
            string manufacturerName = manufacturerItem.Content.ToString();

            int count = _catalog.CountDiscounts(searchText, manufacturerName);
            StatusTextBlock.Text = DiscountsPrefix + count;
        }
    }
}