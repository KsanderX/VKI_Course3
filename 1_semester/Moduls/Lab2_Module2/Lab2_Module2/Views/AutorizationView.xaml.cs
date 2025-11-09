using System.Windows;
using Lab2_Module2.Models;
using Microsoft.Extensions.DependencyInjection;

namespace Lab2_Module2.Views
{
    /// <summary>
    /// Логика взаимодействия для AutorizationView.xaml
    /// </summary>
    public partial class AutorizationView : Window
    {
        private IServiceProvider _serviceProvider;
        private AganichevMusicEquipmentRentalContext _context;
        public User CurrentUser { get; set; }
        public AutorizationView(IServiceProvider service, AganichevMusicEquipmentRentalContext context)
        {
            InitializeComponent();
            _serviceProvider = service;
            _context = context;
        }
        public bool Auth(string login, string password)
        {
            User user = _context.Users.Where(u => u.Login == login && u.Password == password).FirstOrDefault();
            if (user != null)
            {
                CurrentUser = user;
                return true;
            }
            else
            {
                return false;
            }
        }
        private void btnAuth_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string login = tbLogin.Text; 
                string password = tbPassword.Text; 

                if (string.IsNullOrWhiteSpace(login) || string.IsNullOrWhiteSpace(password)) 
                {
                    MessageBox.Show("Пожалуйста, введите логин и пароль.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return; 
                }

                bool resultAuth = Auth(login, password); 

                if (resultAuth) 
                {
                    if (CurrentUser.FkRole == 1)
                    {
                        var adminView = _serviceProvider.GetRequiredService<MainWindow>();
                        this.Close();
                        adminView.Show();
                    }
                    else if (CurrentUser.FkRole == 2) 
                    {
                        var adminView = _serviceProvider.GetRequiredService<MainWindow>();
                        this.Close();
                        adminView.Show();
                    }
                    else if (CurrentUser.FkRole == 3)  
                    {
                        var adminView = _serviceProvider.GetRequiredService<MainWindow>();
                        this.Close();
                        adminView.Show();
                    }
                    else
                    {
                        MessageBox.Show("У вашей учетной записи нет прав для доступа к этому разделу.", "Ошибка доступа", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Неверный логин или пароль.", "Ошибка авторизации", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла критическая ошибка при подключении к базе данных:\n{ex.Message}", "Ошибка подключения", MessageBoxButton.OK, MessageBoxImage.Stop);
            }
        }

        private void btnOpenRegistration_Click(object sender, RoutedEventArgs e)
        {
            var registrView = _serviceProvider.GetRequiredService<RegistrationView>();
            this.Close();
            registrView.Show();
        }

        private void btnAuthGuest_Click(object sender, RoutedEventArgs e)
        {
            var productView = _serviceProvider.GetRequiredService<MainWindow>();
            this.Close();
            productView.Show();
        }
    }
}
