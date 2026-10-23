using System;
using System.IO;
using System.Windows;
using System.Windows.Media.Imaging;
namespace Lab13{
public partial class w1 : Window{
// потом вынесу
string zz;
public w1(){
InitializeComponent();
string d = AppDomain.CurrentDomain.BaseDirectory;string lp = Path.Combine(d, "Images", "logo.png");
if (File.Exists(lp)) {BitmapImage bi = new BitmapImage();bi.BeginInit();bi.UriSource = new Uri(lp);bi.CacheOption = BitmapCacheOption.OnLoad;bi.EndInit();im1.Source = bi;}
}
private void go(object sender, RoutedEventArgs e){
string u = tb1.Text;string p = tb2.Password;
if (u != "") {
string d = AppDomain.CurrentDomain.BaseDirectory;string f = Path.Combine(d, "users.txt");
if (File.Exists(f)) {
string[] a = File.ReadAllLines(f);int i = 0;bool ok = false;string nm = "";
while (i < a.Length) {
string s = a[i];
if (s != null) {if (s != "") {if (s.StartsWith("#") == false) {string[] t = s.Split(';');if (t.Length >= 3) {if (t[0] == u) {if (t[1] == p) {ok = true;nm = t[2];}}}}}}
i = i + 1;
}
if (ok) {if (nm == "") {nm = u;}MainWindow w = new MainWindow(nm);w.Show();Close();}
else {MessageBox.Show("не тот логин", "ошибка!!!!!!", MessageBoxButton.OK, MessageBoxImage.Warning);}
} else {MessageBox.Show("нет файла", "ошибка!!!!!!", MessageBoxButton.OK, MessageBoxImage.Warning);}
} else {MessageBox.Show("ошибка!!!!!!", "ошибка!!!!!!", MessageBoxButton.OK, MessageBoxImage.Warning);tb1.Focus();}
}
private void g2(object sender, RoutedEventArgs e){
MainWindow w = new MainWindow("гость");w.Show();Close();
}
}
}
