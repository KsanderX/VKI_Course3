using System.Windows;
using AI_Demo.Classes;
using AI_Demo.Views;

namespace AI_Demo
{
    /// <summary>
    /// Логика взаимодействия для LoginView.xaml
    /// </summary>
    public partial class LoginView : Window
    {
        public LoginView()
        {
            InitializeComponent();
        }

        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            string login = txtLogin.Text;
            string password = psbPassword.Password;

            // Поиск пользователя в БД
            var user = Helper.GetContext().Users
                .FirstOrDefault(u => u.Login == login && u.Password == password);

            if (user != null)
            {
                Helper.User = user; // Сохраняем пользователя
                ProductView mainWindow = new ProductView();
                mainWindow.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Неверный логин или пароль!");
            }
        }

        private void BtnGuest_Click(object sender, RoutedEventArgs e)
        {
            Helper.User = null; // Гость
            ProductView mainWindow = new ProductView();
            mainWindow.Show();
            this.Close();
        }
    }
}