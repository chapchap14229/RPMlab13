using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
namespace Lab13{
public class Product{
public string Name { get; set; }
public string Category { get; set; }
public string Manufacturer { get; set; }
public string Material { get; set; }
public double Price { get; set; }
public double PriceWithDiscount { get; set; }
public string PriceText { get; set; }
public int Quantity { get; set; }
public string QuantityText { get; set; }
public bool HasDiscount { get; set; }
public bool IsLowStock { get; set; }
public string ImagePath { get; set; }
public string ImageFileName { get; set; }
}
public partial class MainWindow : Window{

List<Product> _products;

public MainWindow(){
InitializeComponent();
_products = new List<Product>();
Product t1 = new Product();t1.Name = "Кроссовки Run";t1.Category = "Спорт";t1.Manufacturer = "Nike";t1.Material = "текстиль";t1.Price = 8000;t1.Quantity = 12;t1.HasDiscount = false;t1.ImageFileName = "p1.png";_products.Add(t1);
Product t2 = new Product();t2.Name = "Кроссовки Air";t2.Category = "Спорт";t2.Manufacturer = "Nike";t2.Material = "кожа";t2.Price = 10000;t2.Quantity = 3;t2.HasDiscount = true;t2.ImageFileName = "p2.png";_products.Add(t2);
Product t3 = new Product();t3.Name = "Туфли Office";t3.Category = "Классика";t3.Manufacturer = "Ralf";t3.Material = "кожа";t3.Price = 5000;t3.Quantity = 6;t3.HasDiscount = false;t3.ImageFileName = "p3.png";_products.Add(t3);
Product t4 = new Product();t4.Name = "Сапоги Winter";t4.Category = "Сезон";t4.Manufacturer = "Ralf";t4.Material = "нубук";t4.Price = 7500;t4.Quantity = 2;t4.HasDiscount = true;t4.ImageFileName = "p4.png";_products.Add(t4);
Product t5 = new Product();t5.Name = "Тапки Home";t5.Category = "Дом";t5.Manufacturer = "NoName";t5.Material = "текстиль";t5.Price = 800;t5.Quantity = 20;t5.HasDiscount = false;t5.ImageFileName = "p5.png";_products.Add(t5);
Product t6 = new Product();t6.Name = "Босоножки Summer";t6.Category = "Сезон";t6.Manufacturer = "Nike";t6.Material = "кожа";t6.Price = 3200;t6.Quantity = 5;t6.HasDiscount = true;t6.ImageFileName = "p6.png";_products.Add(t6);
string d = AppDomain.CurrentDomain.BaseDirectory;string lp = Path.Combine(d, "Images", "logo.png");
if (File.Exists(lp)) {BitmapImage bi = new BitmapImage();bi.BeginInit();bi.UriSource = new Uri(lp);bi.CacheOption = BitmapCacheOption.OnLoad;bi.EndInit();LogoImage.Source = bi;}
BtnSearch_Click(null, null);
}
public MainWindow(string uu) : this()
{
    if (uu != null) {if (uu != "") {UserTextBlock.Text = uu;}}
}



// Проверка фильтра (поиск + производитель)
private bool Matches(Product product, string searchText, string manufacturer)
{
    bool nameMatches = string.IsNullOrEmpty(searchText) ||
                       (product.Name != null && product.Name.ToLower().Contains(searchText.ToLower()));

    bool manufacturerMatches = (manufacturer == "все") ||
                               (product.Manufacturer == manufacturer);

    return nameMatches && manufacturerMatches;
}

// Подбор картинки / заглушки
private void LoadImagePath(Product product)
{
    string dir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Images");
    string file = product.ImageFileName;

    if (string.IsNullOrEmpty(file))
        file = "picture.png";

    string fullPath = Path.Combine(dir, file);

    if (File.Exists(fullPath))
        product.ImagePath = fullPath;
    else
        product.ImagePath = Path.Combine(dir, "picture.png");
}

// Скидка и текст цены
private void ApplyDiscount(Product product)
{
    if (product.HasDiscount && product.Price > 0)
        product.PriceWithDiscount = product.Price - product.Price * 0.25;
    else
        product.PriceWithDiscount = product.Price;

    product.PriceText = product.PriceWithDiscount.ToString("F2") + " руб.";
}

// Текст остатка
private void UpdateQuantityText(Product product)
{
    product.IsLowStock = false;

    if (product.Quantity > 5)
    {
        product.QuantityText = "много";
    }
    else
    {
        product.QuantityText = "мало";
        if (product.Quantity <= 3 && product.Quantity > 0)
            product.IsLowStock = true;
    }
}

// Поиск
private void BtnSearch_Click(object sender, RoutedEventArgs e)
{
    ComboBoxItem manufacturerItem = ManufacturerComboBox.SelectedItem as ComboBoxItem;
    if (manufacturerItem == null)
    {
        MessageBox.Show("ошибка!!!!!!", "ошибка!!!!!!", MessageBoxButton.OK, MessageBoxImage.Warning);
        return;
    }

    string searchText = SearchTextBox.Text ?? "";
    string manufacturerName = manufacturerItem.Content.ToString();

    ProductsListBox.Items.Clear();
    int foundCount = 0;

    foreach (Product product in _products)
    {
        if (!Matches(product, searchText, manufacturerName)) continue;

        LoadImagePath(product);
        ApplyDiscount(product);
        UpdateQuantityText(product);

        ProductsListBox.Items.Add(product);
        foundCount++;
    }

    StatusTextBlock.Text = "найдено: " + foundCount;

    if (foundCount == 0)
        MessageBox.Show("ничего", "ошибка!!!!!!", MessageBoxButton.OK, MessageBoxImage.Warning);
}

// Скидки
private void BtnDiscounts_Click(object sender, RoutedEventArgs e)
{
    ComboBoxItem manufacturerItem = ManufacturerComboBox.SelectedItem as ComboBoxItem;
    if (manufacturerItem == null)
    {
        MessageBox.Show("ошибка!!!!!!", "ошибка!!!!!!", MessageBoxButton.OK, MessageBoxImage.Warning);
        return;
    }

    string searchText = SearchTextBox.Text ?? "";
    string manufacturerName = manufacturerItem.Content.ToString();

    int discountCount = 0;

    foreach (Product product in _products)
    {
        if (!Matches(product, searchText, manufacturerName)) continue;
        if (product.HasDiscount) discountCount++;
    }

    StatusTextBlock.Text = "скидок: " + discountCount;
}
}
}
