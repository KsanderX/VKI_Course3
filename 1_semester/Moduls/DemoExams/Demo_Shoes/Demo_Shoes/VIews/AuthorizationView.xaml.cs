using System.Windows;
using Demo_Shoes.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Demo_Shoes.VIews
{
    /// <summary>
    /// Логика взаимодействия для AuthorizationView.xaml
    /// </summary>
    public partial class AuthorizationView : Window
    {
        private readonly AganichevShoesContext _context;
        private readonly IServiceProvider _serviceProvider;
        public AuthorizationView(IServiceProvider serviceProvider, AganichevShoesContext context)
        {
            InitializeComponent();
            _context = context;
            _serviceProvider = serviceProvider;
        }

        private void btnAuth_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(boxLogin.Text) && !string.IsNullOrWhiteSpace(boxPass.Text))
            {
                User user = _context.Users
                    .Include(u => u.FkUserRoleNavigation)
                    .FirstOrDefault(u => u.Login == boxLogin.Text && u.Password == boxPass.Text);
                if (user != null)
                {
                    MessageBox.Show("Авторизация успешна");

                    var openProductView = _serviceProvider.GetRequiredService<ProductView>();
                    openProductView.SetCurrentUser(user);
                    this.Close();
                    openProductView.Show();
                }
                else
                {
                    MessageBox.Show("Неверно введен логин или пароль!");
                }
            }
            else
            {
                MessageBox.Show("Пожалуйста, заполните все поля!");
            }
        }

        private void btnAuthGuest_Click(object sender, RoutedEventArgs e)
        {
            var openProductView = _serviceProvider.GetRequiredService<ProductView>();
            openProductView.SetCurrentUser(null);
            this.Close();
            openProductView.Show();
        }
    }
}
