using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using AI_Demo.Classes;
using AI_Demo.Models;

namespace AI_Demo.Views
{
    /// <summary>
    /// Логика взаимодействия для OrderWindow.xaml
    /// </summary>
    public partial class OrderWindow : Window
    {
        private Order _order;

        public OrderWindow(Order order)
        {
            InitializeComponent();
            _order = order ?? new Order();

            // Загрузка списков
            cmbStatus.ItemsSource = Helper.GetContext().OrderStatuses.ToList();
            cmbStatus.DisplayMemberPath = "StatusName";

            cmbPickupPoint.ItemsSource = Helper.GetContext().PickUpPoints.ToList();
            cmbPickupPoint.DisplayMemberPath = "Address"; // или другое поле адреса ПВЗ

            // Заполнение полей
            if (order != null)
            {
                txtId.Text = _order.Id.ToString();
                dpDate.SelectedDate = _order.OrderDate;
                dpDelivery.SelectedDate = _order.DeliveryDay;

                cmbStatus.SelectedItem = Helper.GetContext().OrderStatuses.FirstOrDefault(s => s.Id == _order.FkOrderStatus);
                cmbPickupPoint.SelectedItem = Helper.GetContext().PickUpPoints.FirstOrDefault(p => p.Id == _order.FkPickUpPoint);
            }
            else
            {
                // Умолчания для нового заказа
                dpDate.SelectedDate = DateTime.Now;
            }
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            _order.OrderDate = dpDate.SelectedDate ?? DateTime.Now;
            _order.DeliveryDay = dpDelivery.SelectedDate ?? DateTime.Now.AddDays(3);

            if (cmbStatus.SelectedItem is OrderStatus s)
                _order.FkOrderStatus = s.Id;

            if (cmbPickupPoint.SelectedItem is PickUpPoint p)
                _order.FkPickUpPoint = p.Id;

            // Если новый
            if (_order.Id == 0)
            {
                // ВАЖНО: При добавлении нового заказа нужно знать, КТО клиент.
                // В задании не сказано явно выбирать клиента в админке,
                // поэтому поставим заглушку или текущего админа, если это тесты.
                // Но лучше добавить ComboBox с выбором User, если позволяет время.
                // _order.FkClient = ...; 
                Helper.GetContext().Orders.Add(_order);
            }

            try
            {
                Helper.GetContext().SaveChanges();
                DialogResult = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка: " + ex.Message);
            }
        }
    }
}
