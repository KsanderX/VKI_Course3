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
        private List<User> users;
        private List<string> roles = new() { "все роли" };

        private string sortParam;
        private string filtParam = "все роли";
        public StartView(User user = null)
        {
            InitializeComponent();
            users = Models.AppContext.Users;
            roles.AddRange(Models.AppContext.Roles.Select(r => r.Name));

            BoxUsers.ItemsSource = users;
            BoxRole.ItemsSource = roles;

            if (user == null)
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
        public void Find()
        {
            users = Models.AppContext.Users;
            users.Where(u => u.Name.Contains(BoxFind.Text, StringComparison.OrdinalIgnoreCase) || //Find
                u.Login.Contains(BoxFind.Text, StringComparison.OrdinalIgnoreCase)) //Filter
                .Where(q => q.Role.Name == filtParam
                 || filtParam == "все роли");
            if (sortParam == "По возрастанию")
            {
                users.OrderBy(q => q.ID); // Sort
            }
            else if (sortParam == "По убыванию")
            {
                //users.OrderByDescending(u => u.ID).ToList();
                users.Reverse();
            }
            if (BoxUsers != null)
            {
                BoxUsers.ItemsSource = users;
                BoxUsers.Items.Refresh();
            }
        }
        //Find
        private void BoxFind_TextChanged(object sender, TextChangedEventArgs e)
        {
            Find();
        }
        //Sort
        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            RadioButton radio = sender as RadioButton;
            if (radio.Content.ToString() == "По возрастанию")
            {
                sortParam = "По возрастанию";
            }
            else if (radio.Content.ToString() == "По убыванию")
            {
                sortParam = "По убыванию";
                Find();
            }
        }
        //Filter
        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = sender as ComboBox;
            filtParam = comboBox.SelectedItem.ToString();
            Find();
        }
    }
}
