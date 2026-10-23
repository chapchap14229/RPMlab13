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
// потом вынесу
double b;int n1;string qwe2;List<Product> arr;
public MainWindow(){
InitializeComponent();
arr = new List<Product>();
Product t1 = new Product();t1.Name = "Кроссовки Run";t1.Category = "Спорт";t1.Manufacturer = "Nike";t1.Material = "текстиль";t1.Price = 8000;t1.Quantity = 12;t1.HasDiscount = false;t1.ImageFileName = "p1.png";arr.Add(t1);
Product t2 = new Product();t2.Name = "Кроссовки Air";t2.Category = "Спорт";t2.Manufacturer = "Nike";t2.Material = "кожа";t2.Price = 10000;t2.Quantity = 3;t2.HasDiscount = true;t2.ImageFileName = "p2.png";arr.Add(t2);
Product t3 = new Product();t3.Name = "Туфли Office";t3.Category = "Классика";t3.Manufacturer = "Ralf";t3.Material = "кожа";t3.Price = 5000;t3.Quantity = 6;t3.HasDiscount = false;t3.ImageFileName = "p3.png";arr.Add(t3);
Product t4 = new Product();t4.Name = "Сапоги Winter";t4.Category = "Сезон";t4.Manufacturer = "Ralf";t4.Material = "нубук";t4.Price = 7500;t4.Quantity = 2;t4.HasDiscount = true;t4.ImageFileName = "p4.png";arr.Add(t4);
Product t5 = new Product();t5.Name = "Тапки Home";t5.Category = "Дом";t5.Manufacturer = "NoName";t5.Material = "текстиль";t5.Price = 800;t5.Quantity = 20;t5.HasDiscount = false;t5.ImageFileName = "p5.png";arr.Add(t5);
Product t6 = new Product();t6.Name = "Босоножки Summer";t6.Category = "Сезон";t6.Manufacturer = "Nike";t6.Material = "кожа";t6.Price = 3200;t6.Quantity = 5;t6.HasDiscount = true;t6.ImageFileName = "p6.png";arr.Add(t6);
string d = AppDomain.CurrentDomain.BaseDirectory;string lp = Path.Combine(d, "Images", "logo.png");
if (File.Exists(lp)) {BitmapImage bi = new BitmapImage();bi.BeginInit();bi.UriSource = new Uri(lp);bi.CacheOption = BitmapCacheOption.OnLoad;bi.EndInit();im1.Source = bi;}
doIt(null, null);
}
public MainWindow(string uu) : this(){
if (uu != null) {if (uu != "") {tUser.Text = uu;}}
}
// старый вариант не трогать
// private void Button_Click(object sender, RoutedEventArgs e) { }
private void doIt(object sender, RoutedEventArgs e){
string t = tb1.Text;ComboBoxItem c1 = cb1.SelectedItem as ComboBoxItem;
if (c1 != null) {
string mm = c1.Content.ToString();lst1.Items.Clear();int c = 0;int i = 0;
while (i < arr.Count) {
Product x = arr[i];bool ok = false;
if (t == "") {if (mm == "все") {ok = true;} else {if (x.Manufacturer == mm) {ok = true;}}}
else {if (x.Name != null) {if (x.Name.ToLower().Contains(t.ToLower())) {if (mm == "все") {ok = true;} else {if (x.Manufacturer == mm) {ok = true;}}}}}
if (ok) {
string dir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Images");string file = x.ImageFileName;
if (file == null) {file = "picture.png";} else {if (file == "") {file = "picture.png";}}
string pp = Path.Combine(dir, file);
if (File.Exists(pp)) {x.ImagePath = pp;} else {x.ImagePath = Path.Combine(dir, "picture.png");}
if (x.HasDiscount == true) {if (x.Price > 0) {x.PriceWithDiscount = x.Price - x.Price * 0.25;} else {x.PriceWithDiscount = 0;}} else {x.PriceWithDiscount = x.Price;}
x.PriceText = x.PriceWithDiscount.ToString("F2") + " руб.";x.IsLowStock = false;
if (x.Quantity > 5) {x.QuantityText = "много";} else {x.QuantityText = "мало";if (x.Quantity <= 3) {if (x.Quantity > 0) {x.IsLowStock = true;}}}
lst1.Items.Add(x);c = c + 1;
}
i = i + 1;
}
tOut.Text = "найдено: " + c.ToString();
if (c == 0) {MessageBox.Show("ничего", "ошибка!!!!!!", MessageBoxButton.OK, MessageBoxImage.Warning);}
} else {MessageBox.Show("ошибка!!!!!!", "ошибка!!!!!!", MessageBoxButton.OK, MessageBoxImage.Warning);}
}
// почти то же самое
private void xx(object sender, RoutedEventArgs e){
string t = tb1.Text;ComboBoxItem c1 = cb1.SelectedItem as ComboBoxItem;
if (c1 == null) {MessageBox.Show("ошибка!!!!!!", "ошибка!!!!!!", MessageBoxButton.OK, MessageBoxImage.Warning);return;}
string mm = c1.Content.ToString();int k = 0;int i = 0;
while (i < arr.Count) {
Product x = arr[i];bool ok = false;
if (t == "") {if (mm == "все") {ok = true;} else {if (x.Manufacturer == mm) {ok = true;}}}
else {if (x.Name != null) {if (x.Name.ToLower().Contains(t.ToLower())) {if (mm == "все") {ok = true;} else {if (x.Manufacturer == mm) {ok = true;}}}}}
if (ok) {if (x.HasDiscount == true) {if (x.Price > 0) {k = k + 1;}}}
i = i + 1;
}
tOut.Text = "скидок: " + k.ToString();
}
}
}
