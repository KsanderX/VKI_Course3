using System.Windows;
using Lab2_Module2.Models;
using Microsoft.Extensions.DependencyInjection;

namespace Lab2_Module2.Views
{
    /// <summary>
    /// Логика взаимодействия для RegistrationView.xaml
    /// </summary>
    public partial class RegistrationView : Window
    {
        private IServiceProvider _serviceProvider;
        private AganichevMusicEquipmentRentalContext _context;
        public RegistrationView(IServiceProvider service, AganichevMusicEquipmentRentalContext context)
        {
            InitializeComponent();
            _serviceProvider = service;
            _context = context;
        }

        public void Register(string FIO, string login, string password)
        {
            User user = _context.Users.Where(u => u.Login == login).FirstOrDefault();
            if(user != null)
            {
                MessageBox.Show("Пользователь с таким логином уже существует");
            }
            else
            {
                User newUser = new User()
                {
                    Fio = FIO,
                    Login = login,
                    Password = password,
                    FkRole = 3
                };
                _context.Users.Add(newUser);
                _context.SaveChanges();
            }
        }
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            string FIO = tbFIO.Text;
            string login = tbLogin.Text;
            string password = tbPassword.Text;
            if(string.IsNullOrWhiteSpace(FIO) || string.IsNullOrWhiteSpace(login) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Заполните все поля!");
            }
            else if(LogExists(login))
            {
                MessageBox.Show("Пользователь с таким логином уже существует");
            }
            else
            {
                MessageBox.Show("Регистрация прошла успешно!");
                Register(FIO, login, password);
                var authView = _serviceProvider.GetRequiredService<AutorizationView>();
                this.Close();
                authView.Show();
            }
        }

        public bool LogExists(string login)
        {
            return _context.Users.Any(l => l.Login == login);
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            var authView = _serviceProvider.GetRequiredService<AutorizationView>();           
            this.Close();
            authView.Show();
        }
    }
}
