using System.Windows;
using Demodemo.Models;
using Demodemo.Views;

namespace Demodemo
{
    /// <summary>
    /// Логика взаимодействия для AuthView.xaml
    /// </summary>
    public partial class AuthView : Window
    {
        private AganichevShoesContext _context;
        public AuthView()
        {
            InitializeComponent();
            _context = new AganichevShoesContext();
        }
        private void btnAuth_Click(object sender, RoutedEventArgs e)
        {
            var user = _context.Users.FirstOrDefault(u => u.Login == tbLogin.Text && u.Password == tbPass.Text);
            if (user != null)
            {
                var mainView = new ProductView(user);
                this.Close();
                mainView.Show();    
            }
            else
            {
                MessageBox.Show("Неверный логин или пароль");
            }
        }
        private void btnGuesAuth_Click(object sender, RoutedEventArgs e)
        {
            var mainView = new ProductView();
            this.Close();
            mainView.Show();
        }

    }
}
