using System.Windows;
using System.Windows.Input;
using AI_Demo.Classes;
using AI_Demo.Models;
using Microsoft.EntityFrameworkCore;

namespace AI_Demo.Views
{
    /// <summary>
    /// Логика взаимодействия для OrderListWindow.xaml
    /// </summary>
    public partial class OrderListWindow : Window
    {
        public OrderListWindow()
        {
            InitializeComponent();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (Helper.User.FkUserRole != 1) // Если не админ
            {
                btnAddOrder.Visibility = Visibility.Collapsed;
            }
            UpdateOrders();
        }

        private void UpdateOrders()
        {
            // Include статусов и клиентов
            var orders = Helper.GetContext().Orders
                .Include(o => o.FkOrderStatusNavigation)
                .Include(o => o.FkClientNavigation)
                .ToList();

            LvOrders.ItemsSource = orders;
        }

        private void BtnAddOrder_Click(object sender, RoutedEventArgs e)
        {
            OrderWindow ow = new OrderWindow(null);
            if (ow.ShowDialog() == true) UpdateOrders();
        }

        private void LvOrders_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (Helper.User.FkUserRole != 1) return; // Только админ редактирует

            var selectedOrder = LvOrders.SelectedItem as Order;
            if (selectedOrder != null)
            {
                OrderWindow ow = new OrderWindow(selectedOrder);
                if (ow.ShowDialog() == true) UpdateOrders();
            }
        }

        private void MenuItemDeleteOrder_Click(object sender, RoutedEventArgs e)
        {
            if (Helper.User.FkUserRole != 1) return;

            var selectedOrder = LvOrders.SelectedItem as Order;
            if (selectedOrder != null && MessageBox.Show("Удалить?", "Подтверждение", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                // Сначала надо удалить содержимое заказа (OrderContents)
                var contents = Helper.GetContext().OrderContents.Where(oc => oc.FkOrder == selectedOrder.Id);
                Helper.GetContext().OrderContents.RemoveRange(contents);

                Helper.GetContext().Orders.Remove(selectedOrder);
                Helper.GetContext().SaveChanges();
                UpdateOrders();
            }
        }
    }
}
