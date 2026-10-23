using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
namespace Lab13{
public class Product{
public string n { get; set; }
public string k { get; set; }
public string m { get; set; }
public string s { get; set; }
public double p { get; set; }
public double p2 { get; set; }
public string ps { get; set; }
public int q { get; set; }
public string qt { get; set; }
public bool f { get; set; }
public bool f2 { get; set; }
public string img { get; set; }
public string fn { get; set; }
}
public partial class MainWindow : Window{
// потом вынесу
double b;int n1;string qwe2;List<Product> arr;
public MainWindow(){
InitializeComponent();
arr = new List<Product>();
Product t1 = new Product();t1.n = "Кроссовки Run";t1.k = "Спорт";t1.m = "Nike";t1.s = "текстиль";t1.p = 8000;t1.q = 12;t1.f = false;t1.fn = "p1.png";arr.Add(t1);
Product t2 = new Product();t2.n = "Кроссовки Air";t2.k = "Спорт";t2.m = "Nike";t2.s = "кожа";t2.p = 10000;t2.q = 3;t2.f = true;t2.fn = "p2.png";arr.Add(t2);
Product t3 = new Product();t3.n = "Туфли Office";t3.k = "Классика";t3.m = "Ralf";t3.s = "кожа";t3.p = 5000;t3.q = 6;t3.f = false;t3.fn = "p3.png";arr.Add(t3);
Product t4 = new Product();t4.n = "Сапоги Winter";t4.k = "Сезон";t4.m = "Ralf";t4.s = "нубук";t4.p = 7500;t4.q = 2;t4.f = true;t4.fn = "p4.png";arr.Add(t4);
Product t5 = new Product();t5.n = "Тапки Home";t5.k = "Дом";t5.m = "NoName";t5.s = "текстиль";t5.p = 800;t5.q = 20;t5.f = false;t5.fn = "p5.png";arr.Add(t5);
Product t6 = new Product();t6.n = "Босоножки Summer";t6.k = "Сезон";t6.m = "Nike";t6.s = "кожа";t6.p = 3200;t6.q = 5;t6.f = true;t6.fn = "p6.png";arr.Add(t6);
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
if (t == "") {if (mm == "все") {ok = true;} else {if (x.m == mm) {ok = true;}}}
else {if (x.n != null) {if (x.n.ToLower().Contains(t.ToLower())) {if (mm == "все") {ok = true;} else {if (x.m == mm) {ok = true;}}}}}
if (ok) {
string dir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Images");string file = x.fn;
if (file == null) {file = "picture.png";} else {if (file == "") {file = "picture.png";}}
string pp = Path.Combine(dir, file);
if (File.Exists(pp)) {x.img = pp;} else {x.img = Path.Combine(dir, "picture.png");}
if (x.f == true) {if (x.p > 0) {x.p2 = x.p - x.p * 0.25;} else {x.p2 = 0;}} else {x.p2 = x.p;}
x.ps = x.p2.ToString("F2") + " руб.";x.f2 = false;
if (x.q > 5) {x.qt = "много";} else {x.qt = "мало";if (x.q <= 3) {if (x.q > 0) {x.f2 = true;}}}
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
if (t == "") {if (mm == "все") {ok = true;} else {if (x.m == mm) {ok = true;}}}
else {if (x.n != null) {if (x.n.ToLower().Contains(t.ToLower())) {if (mm == "все") {ok = true;} else {if (x.m == mm) {ok = true;}}}}}
if (ok) {if (x.f == true) {if (x.p > 0) {k = k + 1;}}}
i = i + 1;
}
tOut.Text = "скидок: " + k.ToString();
}
}
}
