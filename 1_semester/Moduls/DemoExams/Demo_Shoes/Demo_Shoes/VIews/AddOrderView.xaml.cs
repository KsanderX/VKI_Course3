using System.Windows;
using Demo_Shoes.Models;

namespace Demo_Shoes.VIews
{
    /// <summary>
    /// Логика взаимодействия для AddOrderView.xaml
    /// </summary>
    public partial class AddOrderView : Window
    {
        private readonly AganichevShoesContext _context;

        public AddOrderView(AganichevShoesContext context)
        {
            InitializeComponent();
            _context = context;
            LoadComboBoxes();

            DpOrderDate.SelectedDate = DateTime.Now;
            DpDeliveryDate.SelectedDate = DateTime.Now.AddDays(3);
        }

        private void LoadComboBoxes()
        {
            CbStatus.ItemsSource = _context.OrderStatuses.ToList();
            CbPickupPoint.ItemsSource = _context.PickUpPoints.ToList();
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TbOrderNumber.Text) ||
                CbStatus.SelectedItem == null ||
                CbPickupPoint.SelectedItem == null ||
                DpOrderDate.SelectedDate == null ||
                DpDeliveryDate.SelectedDate == null)
            {
                MessageBox.Show("Заполните все поля!", "Ошибка");
                return;
            }

            if (!double.TryParse(TbOrderNumber.Text, out double orderNum))
            {
                MessageBox.Show("Номер заказа должен быть числом!", "Ошибка");
                return;
            }

            try
            {
                int newId = 1;
                if (_context.Orders.Any())
                {
                    newId = _context.Orders.Max(o => o.Id) + 1;
                }

                Order newOrder = new Order
                {
                    Id = newId,
                    OrderNumber = orderNum,
                    FkOrderStatus = (int)CbStatus.SelectedValue,
                    FkPickUpPoint = (int)CbPickupPoint.SelectedValue,
                    OrderDate = DpOrderDate.SelectedDate,
                    DeliveryDay = DpDeliveryDate.SelectedDate,
                    CodeToReceive = new Random().Next(100, 999)
                };
                _context.Orders.Add(newOrder);
                _context.SaveChanges();

                MessageBox.Show("Заказ успешно добавлен!");
                DialogResult = true;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка сохранения: " + ex.Message);
            }
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }   
    }
}
