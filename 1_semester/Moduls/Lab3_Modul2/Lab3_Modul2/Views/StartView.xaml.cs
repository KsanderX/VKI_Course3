using System.Windows;
using System.Windows.Controls;
using Lab3_Modul2.Models;

namespace Lab3_Modul2.Views
{
    /// <summary>
    /// Логика взаимодействия для StartView.xaml
    /// </summary>
    public partial class StartView : Window
    {
        public StartView(User user = null)
        {
            InitializeComponent();
            if(user == null)
            {
                RunNameUser.Text = "Гость";
                RunRoleUser.Text = "Гость";
            }
            else
            {
                RunNameUser.Text = user.Name;
                RunRoleUser.Text = user.Role.Name;
                
                switch (user.Role.Name)
                {
                    case "Администратор":
                        panelCRUD.Visibility = Visibility.Visible;
                        panelFind.Visibility = Visibility.Visible;
                        panelRequst.Visibility = Visibility.Visible;

                        BoxUsers.MouseDoubleClick += DoubleClickUserEdit;
                        break;

                    case "Менеджер":
                        panelFind.Visibility = Visibility.Visible;
                        panelRequst.Visibility = Visibility.Visible;
                        break;
                }
            }           
            BoxUsers.ItemsSource = Models.AppContext.Users;

           
        }

        private void ButtonExit_Click(object sender, RoutedEventArgs e)
        {
            AuthView authView = new AuthView();
            authView.Show();
            MessageBox.Show("Вы вышли из системы.", "Выход", MessageBoxButton.OK, MessageBoxImage.Information);
            this.Close();
        }

        private void btnOpenRequest_Click(object sender, RoutedEventArgs e)
        {
            var orderView = new OrderView();
            orderView.ShowDialog();
        }

        private void DoubleClickUserEdit(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            ListBox boxUsers = sender as ListBox;
            User user = boxUsers.SelectedItem as User;

            MessageBox.Show($"Редактирование пользователя: {user.Name}", "Редактирование", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}
