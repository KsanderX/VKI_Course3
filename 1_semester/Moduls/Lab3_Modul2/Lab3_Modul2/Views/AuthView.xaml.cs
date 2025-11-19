using System.Windows;
using Lab3_Modul2.Models;

namespace Lab3_Modul2.Views
{
    /// <summary>
    /// Логика взаимодействия для AuthView.xaml
    /// </summary>
    public partial class AuthView : Window
    {
        public AuthView()
        {
            InitializeComponent();
        }

        private void btnAuth_Click(object sender, RoutedEventArgs e)
        {
            if(!string.IsNullOrWhiteSpace(boxLogin.Text) && !string.IsNullOrWhiteSpace(boxPass.Text))
            {
                User? user = Models.AppContext.Users.FirstOrDefault(x => x.Login == boxLogin.Text && x.Password == boxPass.Text);
                if(user != null)
                {
                    MessageBox.Show($"Добро пожаловать, {user.Name}!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);

                    var startView = new StartView(user);
                    startView.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Неверный логин или пароль.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }               
            }  
        }

        private void btnReg_Click(object sender, RoutedEventArgs e)
        {
            RegView regView = new RegView();
            regView.ShowDialog();

            if(regView.DialogResult == true)
            {
                MessageBox.Show("Регистрация прошла успешно!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Регистрация отменена.", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void btnAuthGuest_Click(object sender, RoutedEventArgs e)
        {
            var startView = new StartView();
            this.Close();
            startView.Show();
        }
    }
}
