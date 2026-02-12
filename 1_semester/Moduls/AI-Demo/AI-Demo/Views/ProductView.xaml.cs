using System.Windows;
using System.Windows.Controls;
using AI_Demo.Classes;
using AI_Demo.Models;
using Microsoft.EntityFrameworkCore;

namespace AI_Demo.Views
{
    /// <summary>
    /// Логика взаимодействия для ProductView.xaml
    /// </summary>
    public partial class ProductView : Window
    {
        public ProductView()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // Настройка видимости кнопок (оставляем как было, код ролей верный)
            if (Helper.User != null)
            {
                txtUserName.Text = Helper.User.Fio;
                int? roleId = Helper.User.FkUserRole;

                if (roleId == 1) // Администратор
                {
                    btnAddProduct.Visibility = Visibility.Visible;
                    btnOrders.Visibility = Visibility.Visible;
                }
                else if (roleId == 2) // Менеджер
                {
                    btnOrders.Visibility = Visibility.Visible;
                }
            }
            else
            {
                txtUserName.Text = "Гость";
            }

            // Загрузка производителей
            var manufacturers = Helper.GetContext().Manufacturers.ToList();
            manufacturers.Insert(0, new Manufacturer { Name = "Все производители" });
            cmbFilter.ItemsSource = manufacturers;
            cmbFilter.DisplayMemberPath = "Name";
            cmbFilter.SelectedIndex = 0;

            UpdateProducts();
        }

        private void UpdateProducts()
        {
            // 1. Получаем контекст БД
            var currentContext = Helper.GetContext();

            // 2. Загружаем товары вместе со СВЯЗАННЫМИ таблицами (Include обязательно!)
            // Без Include FkProductNameNavigation будет null -> ошибка при поиске -> 0 товаров
            var products = currentContext.Products
                .Include(p => p.FkManufacturerNavigation)
                .Include(p => p.FkProductNameNavigation)
                .ToList();

            // 3. Поиск (с защитой от ошибок)
            if (!string.IsNullOrWhiteSpace(txtSearch.Text))
            {
                string searchText = txtSearch.Text.ToLower();
                products = products.Where(p =>
                    p.FkProductNameNavigation != null &&
                    p.FkProductNameNavigation.Name != null &&
                    p.FkProductNameNavigation.Name.ToLower().Contains(searchText)
                ).ToList();
            }

            // 4. Фильтрация по производителю
            if (cmbFilter.SelectedIndex > 0)
            {
                var selectedMan = cmbFilter.SelectedItem as Manufacturer;
                if (selectedMan != null)
                {
                    products = products.Where(p => p.FkManufacturer == selectedMan.Id).ToList();
                }
            }

            // 5. Сортировка
            if (cmbSort.SelectedIndex == 1) // Возрастание цены
                products = products.OrderBy(p => p.Price).ToList();
            else if (cmbSort.SelectedIndex == 2) // Убывание цены
                products = products.OrderByDescending(p => p.Price).ToList();

            // 6. ПРИСВОЕНИЕ ИСТОЧНИКА ДАННЫХ (Самый важный шаг)
            LvProducts.ItemsSource = products;

            // 7. Обновление счетчика
            int currentCount = products.Count;
            int totalCount = currentContext.Products.Count();
            txtCount.Text = $"{currentCount} из {totalCount}";
        }

        private void TxtSearch_TextChanged(object sender, TextChangedEventArgs e) => UpdateProducts();
        private void CmbSort_SelectionChanged(object sender, SelectionChangedEventArgs e) => UpdateProducts();
        private void CmbFilter_SelectionChanged(object sender, SelectionChangedEventArgs e) => UpdateProducts();

        private void BtnExit_Click(object sender, RoutedEventArgs e)
        {
            LoginView login = new LoginView();
            login.Show();
            this.Close();
        }

        private void MenuItemDelete_Click(object sender, RoutedEventArgs e)
        {
            // Логика удаления (оставляем, только проверяем связь с OrderContents)
            if (Helper.User?.FkUserRole != 1) return;

            var selectedProduct = LvProducts.SelectedItem as Product;
            if (selectedProduct == null) return;

            // Проверка на наличие в заказах (OrderContents)
            var inOrders = Helper.GetContext().OrderContents.Any(o => o.FkProduct == selectedProduct.Id);

            if (inOrders)
            {
                MessageBox.Show("Товар нельзя удалить, он есть в заказах.");
                return;
            }

            if (MessageBox.Show("Вы уверены?", "Удаление", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                Helper.GetContext().Products.Remove(selectedProduct);
                Helper.GetContext().SaveChanges();
                UpdateProducts();
            }
        }

        private void LvProducts_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (Helper.User?.FkUserRole != 1) return;

            var selectedProduct = LvProducts.SelectedItem as Product;
            if (selectedProduct != null)
            {
                ProductWindow pw = new ProductWindow(selectedProduct);
                if (pw.ShowDialog() == true) UpdateProducts();
            }
        }

        private void BtnAddProduct_Click(object sender, RoutedEventArgs e)
        {
            ProductWindow pw = new ProductWindow(null);
            if (pw.ShowDialog() == true) UpdateProducts();
        }

        private void BtnOrders_Click(object sender, RoutedEventArgs e)
        {
            OrderListWindow orderWin = new OrderListWindow();
            orderWin.Show();
        }
    }
}