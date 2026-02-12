using System.Windows;
using Demodem.Models;
using Demodem.Views;
using Microsoft.EntityFrameworkCore;

namespace Demodem
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class AuthView : Window
    {
        private AganichevShoesContext _context;
        public AuthView()
        {
            InitializeComponent();
            _context = new AganichevShoesContext();
        }
        private void btnOpenProduct_Click(object sender, RoutedEventArgs e)
        {
            var user = _context.Users.FirstOrDefault(q => q.Login == tbLogin.Text && q.Password == tbPass.Text);
            if(user != null)
            {
                var openProduct = new ProductView(user);
                this.Close();
                openProduct.Show();
            }
            else
            {
                MessageBox.Show("Вы ввели неверный логин или пароль");
            }
        }

        private void btnOpenProductsGust_Click(object sender, RoutedEventArgs e)
        {
            var openProduct = new ProductView();
            this.Close();
            openProduct.Show(); 
        }

    }
}