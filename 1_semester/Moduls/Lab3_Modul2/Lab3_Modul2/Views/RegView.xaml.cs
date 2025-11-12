using System.Windows;
using Lab3_Modul2.Models;

namespace Lab3_Modul2.Views
{
    /// <summary>
    /// Логика взаимодействия для RegView.xaml
    /// </summary>
    public partial class RegView : Window
    {
        public RegView()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(boxLogin.Text) &&
                !string.IsNullOrWhiteSpace(boxPass.Text) &&
                !string.IsNullOrWhiteSpace(boxName.Text))
            {
                User? user = Models.AppContext.Users.FirstOrDefault(x => x.Login == boxLogin.Text && x.Password == boxPass.Text);
                if (user != null)
                {
                    MessageBox.Show("Пользователь с таким логином уже существует.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    User newUser = new User()
                    {
                        Name = boxName.Text,
                        Login = boxLogin.Text,
                        Password = boxPass.Text,
                        Role = Models.AppContext.Roles.FirstOrDefault(r => r.Name == "Пользователь")
                    };
                    Models.AppContext.Users.Add(newUser);
                    DialogResult = true;
                }
            }
            else
            { 
                MessageBox.Show("Заполните все поля.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
