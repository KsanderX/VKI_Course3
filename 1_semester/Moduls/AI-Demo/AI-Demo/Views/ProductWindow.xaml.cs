using System.Windows;
using AI_Demo.Classes;
using AI_Demo.Models;

namespace AI_Demo.Views
{
    /// <summary>
    /// Логика взаимодействия для ProductWindow.xaml
    /// </summary>
    public partial class ProductWindow : Window
    {
        private Product _product;

        public ProductWindow(Product product)
        {
            InitializeComponent();
            _product = product ?? new Product();

            cmbManuf.ItemsSource = Helper.GetContext().Manufacturers.ToList();
            cmbManuf.DisplayMemberPath = "Name";

            // Если редактирование
            if (product != null)
            {
                // Имя берем из связанной таблицы, если оно есть
                if (_product.FkProductNameNavigation != null)
                    txtName.Text = _product.FkProductNameNavigation.Name;

                txtCost.Text = _product.Price.ToString();

                cmbManuf.SelectedItem = Helper.GetContext().Manufacturers
                    .FirstOrDefault(m => m.Id == _product.FkManufacturer);
            }
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show("Введите название");
                return;
            }

            // --- ЛОГИКА ДЛЯ ИМЕНИ ТОВАРА ---
            // 1. Ищем, есть ли уже такое имя в таблице ProductName
            string inputName = txtName.Text;
            var nameEntity = Helper.GetContext().ProductNames
                                .FirstOrDefault(n => n.Name == inputName);

            // 2. Если нет, создаем новое
            if (nameEntity == null)
            {
                nameEntity = new ProductName { Name = inputName };
                Helper.GetContext().ProductNames.Add(nameEntity);
                Helper.GetContext().SaveChanges(); // Сохраняем, чтобы получить ID
            }

            // 3. Присваиваем ID имени продукту
            _product.FkProductName = nameEntity.Id;
            // -------------------------------

            if (double.TryParse(txtCost.Text, out double cost))
                _product.Price = cost; // Поле называется Price (double)

            if (cmbManuf.SelectedItem is Manufacturer m)
                _product.FkManufacturer = m.Id;

            if (_product.Id == 0)
            {
                Helper.GetContext().Products.Add(_product);
            }

            try
            {
                Helper.GetContext().SaveChanges();
                MessageBox.Show("Сохранено");
                DialogResult = true;
            }
            catch
            {
                MessageBox.Show("Ошибка сохранения");
            }
        }
    }
}
