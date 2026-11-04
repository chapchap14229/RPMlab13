using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Lab13
{
    public partial class LoginWindow : Window
    {
        private const string ErrorMessage = "ошибка!!!!!!";
        private const string WrongLoginMessage = "не тот логин";
        private const string NoFileMessage = "нет файла";
        private const string GuestName = "гость";

        private LoginService _loginService;

        public object LogoImage { get; private set; }
        public object LoginTextBox { get; private set; }

        public LoginWindow()
        {
            InitializeComponent();
            _loginService = new LoginService();

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
        }

        private void InitializeComponent()
        {
            throw new NotImplementedException();
        }

        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            string login = LoginTextBox.Text;
            string password = PasswordBox.Password;

            if (string.IsNullOrEmpty(login))
            {
                MessageBox.Show(ErrorMessage, ErrorMessage, MessageBoxButton.OK, MessageBoxImage.Warning);
                LoginTextBox.Focus();
                return;
            }

            string userName = _loginService.Authenticate(login, password);

            if (userName == null)
            {
                MessageBox.Show(WrongLoginMessage, ErrorMessage, MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            MainWindow mainWindow = new MainWindow(userName);
            mainWindow.Show();
            Close();
        }

        private void BtnGuest_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow(GuestName);
            mainWindow.Show();
            Close();
        }
    }
}