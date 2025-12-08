using System.Windows;
using System.Windows.Input; 
using Demo_Shoes.Controllers;
using Demo_Shoes.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Demo_Shoes.VIews
{
    public partial class OrderView : Window
    {
        private readonly IServiceProvider _service;
        private readonly AganichevShoesContext _context;
        private User _currentUser;

        public OrderView(AganichevShoesContext context, IServiceProvider service)
        {
            InitializeComponent();
            _context = context;
            _service = service;

            LoadOrders();
        }
        public void SetCurrentUser(User user)
        {
            _currentUser = user;

            BoxOrder.MouseDoubleClick -= BoxOrder_MouseDoubleClick;

            if (_currentUser != null && _currentUser.FkUserRoleNavigation != null)
            {
                if (_currentUser.FkUserRoleNavigation.Name == "Администратор")
                {
                    panelCRUD.Visibility = Visibility.Visible;
                    BoxOrder.MouseDoubleClick += BoxOrder_MouseDoubleClick;
                }
            }
        }

        private void LoadOrders()
        {
            BoxOrder.Items.Clear();

            var orders = _context.Orders
                .Include(o => o.FkOrderStatusNavigation)
                .Include(o => o.FkPickUpPointNavigation)
                .Include(o => o.OrderContents)
                .OrderByDescending(o => o.OrderDate)
                .ToList();

            foreach (var item in orders)
            {
                OrderItemController orderController = new OrderItemController();
                orderController.DataContext = item;
                BoxOrder.Items.Add(orderController);
            }
        }
        private void BoxOrder_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var selectedController = BoxOrder.SelectedItem as OrderItemController;
            if (selectedController != null && selectedController.DataContext is Order selectedOrder)
            {
                var editView = _service.GetRequiredService<EditOrderView>();
                editView.InitializeData(selectedOrder);
                if (editView.ShowDialog() == true)
                {
                    LoadOrders();
                }
            }
        }

        private void btnAddProduct_Click(object sender, RoutedEventArgs e)
        {
            var addOrderView = _service.GetRequiredService<AddOrderView>();
            if (addOrderView.ShowDialog() == true)
            {
                LoadOrders();
            }

        }
        private void btnDeleteOrder_Click(object sender, RoutedEventArgs e)
        {
            var selectedController = BoxOrder.SelectedItem as OrderItemController;

            if (selectedController == null)
            {
                MessageBox.Show("Пожалуйста, выберите заказ для удаления!", "Внимание");
                return;
            }

            if (selectedController.DataContext is Order orderToDelete)
            {
                if (MessageBox.Show($"Удалить заказ №{orderToDelete.OrderNumber}?", "Подтверждение", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    try
                    {
                        if (orderToDelete.OrderContents.Any())
                        {
                            _context.OrderContents.RemoveRange(orderToDelete.OrderContents);
                        }
                        _context.Orders.Remove(orderToDelete);
                        _context.SaveChanges();
                        MessageBox.Show("Заказ удален!");
                        LoadOrders();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Ошибка: " + ex.Message);
                    }
                }
            }
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            var productView = _service.GetRequiredService<ProductView>();
            productView.SetCurrentUser(_currentUser);
            this.Close();
            productView.Show();
        }
    }
}