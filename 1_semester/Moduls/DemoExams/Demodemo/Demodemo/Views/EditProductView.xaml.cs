//using System.Windows;
//using Demodemo.Models;

//namespace Demodemo.Views
//{
//    /// <summary>
//    /// Логика взаимодействия для EditProductView.xaml
//    /// </summary>
//    public partial class EditProductView : Window
//    {
//        private Product product;
//        private AganichevShoesContext _context;
      
//        public EditProductView(Product product)
//        {
//            InitializeComponent();
//            _context = new AganichevShoesContext();
//            this.product = product;
//            LoadProduct();
//        }

//        private void LoadProduct()
//        {
//            tbID.Text = product.Id.ToString();
//            tbDesc.Text = product.Description;
//            tbDiscount.Text = product.Discount.ToString();
//            tbPrice.Text = product.Price.ToString();
//            tbCountOnStorage.Text = product.CountOnStorage.ToString();  

//            tcName.Text = product.FkProductNameNavigation.Name;
//            cbSupplier.ItemsSource = _context.Suppliers.ToList();
//            cbSupplier.SelectedItem = product.FkSupplierNavigation;

//            cbCategory.ItemsSource = _context.ProductCategories.ToList();
//            cbCategory.SelectedItem = product.FkProductCategoryNavigation;

//            cbManuf.ItemsSource = _context.Manufacturers.ToList();
//            cbManuf.SelectedItem = product.FkManufacturerNavigation;

//            cbUnit.ItemsSource = _context?.Units.ToList();
//        }

//        private void btnSaveProduct_Click(object sender, RoutedEventArgs e)
//        {
//            if (string.IsNullOrWhiteSpace(tbName.Text) ||
//                string.IsNullOrWhiteSpace(tbID.Text) ||
//                string.IsNullOrWhiteSpace(tbDesc.Text) ||
//                string.IsNullOrWhiteSpace(tbDiscount.Text) ||
//                string.IsNullOrWhiteSpace(tbPrice.Text) ||
//                string.IsNullOrWhiteSpace(tbCountOnStorage.Text))
//            {
//                MessageBox.Show("Пожалуйста, заполните все поля.");

//            }
//            if(!decimal.TryParse(tbPrice.Text, out decimal price) || price < 0)
//            {
//                MessageBox.Show("Некорректное значение цены.");
//                return;
//            }
//            if(!int.TryParse(tbDiscount.Text, out int desc) || desc < 0)
//            {
//                MessageBox.Show("Некорректное значение скидки.");
//               return;
//            }
//            if(!int.TryParse(tbCountOnStorage.Text, out int countOnStorage) || countOnStorage < 0)
//            {
//                MessageBox.Show("Некорректное значение количества на складе.");
//                return;
//            }
//            try
//            {
//                product.FkProductNameNavigation = _context.ProductNames.FirstOrDefault(q => q.Name == tbName.Text);
//                product.FkProductCategoryNavigation = _context.ProductCategories.FirstOrDefault(q => q.Name == cbCategory.SelectedItem.ToString());
//                product.Description = tbDesc.Text;
//                product.Price = int.Parse(tbPrice.Text);
//                product.Discount = int.Parse(tbDiscount.Text);
//                product.FkUnitNavigation = _context.Units.FirstOrDefault(q => q.Name == cbUnit.SelectedItem.ToString());
//                product.CountOnStorage = int.Parse(tbCountOnStorage.Text);

//                _context.SaveChanges();
//                MessageBox.Show("Продукт успешно сохранен.");

//                DialogResult = true;
//                this.Close();
//            }
//            catch (Exception ex)
//            {
//                MessageBox.Show("Ошибка при сохранении продукта: " + ex.Message);
//            }
//        }

//        private void btnLoadImage_Click(object sender, RoutedEventArgs e)
//        {

//        }

//        private void btnExit_Click(object sender, RoutedEventArgs e)
//        {
//            var openProduct = new ProductView();
//            this.Close();
//            openProduct.Show();
//        }
//    }
//}
