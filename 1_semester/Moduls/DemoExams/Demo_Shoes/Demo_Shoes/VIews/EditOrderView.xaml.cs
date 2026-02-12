using System.Windows;
using Demo_Shoes.Models;

namespace Demo_Shoes.VIews
{
    /// <summary>
    /// Логика взаимодействия для EditOrderView.xaml
    /// </summary>
    public partial class EditOrderView : Window
    {
        private readonly AganichevShoesContext _context;
        private Order _currentOrder;

        public EditOrderView(AganichevShoesContext context)
        {
            InitializeComponent();
            _context = context;
        }

        public void InitializeData(Order order)
        {
            _currentOrder = order;
            LoadComboBoxes();
            FillFields();
        }

        private void LoadComboBoxes()
        {
            CbStatus.ItemsSource = _context.OrderStatuses.ToList();
            CbPickupPoint.ItemsSource = _context.PickUpPoints.ToList();
        }

        private void FillFields()
        {
            TbOrderNumber.Text = _currentOrder.OrderNumber.ToString();
            CbStatus.SelectedValue = _currentOrder.FkOrderStatus;
            CbPickupPoint.SelectedValue = _currentOrder.FkPickUpPoint;
            DpOrderDate.SelectedDate = _currentOrder.OrderDate;
            DpDeliveryDate.SelectedDate = _currentOrder.DeliveryDay;
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TbOrderNumber.Text) ||
                CbStatus.SelectedItem == null ||
                CbPickupPoint.SelectedItem == null)
            {
                MessageBox.Show("Заполните обязательные поля!");
                return;
            }

            if (!double.TryParse(TbOrderNumber.Text, out double orderNum))
            {
                MessageBox.Show("Некорректный номер заказа!");
                return;
            }

            try
            {
                _currentOrder.OrderNumber = orderNum;
                _currentOrder.FkOrderStatus = (int)CbStatus.SelectedValue;
                _currentOrder.FkPickUpPoint = (int)CbPickupPoint.SelectedValue;
                _currentOrder.OrderDate = DpOrderDate.SelectedDate;
                _currentOrder.DeliveryDay = DpDeliveryDate.SelectedDate;

                _context.SaveChanges();

                MessageBox.Show("Изменения сохранены!");
                DialogResult = true;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка БД: " + ex.Message);
            }
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}

