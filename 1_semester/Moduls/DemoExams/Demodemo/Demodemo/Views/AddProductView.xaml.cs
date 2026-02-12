using System.IO;
using System.Reflection;
using System.Windows;
using System.Windows.Media.Imaging;
using Demodemo.Models;
using Microsoft.Win32;

namespace Demodemo.Views
{
    /// <summary>
    /// Логика взаимодействия для AddProductView.xaml
    /// </summary>
    public partial class AddProductView : Window
    {
        private string _projePath = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location); 
        private string selectImage;
        private User _currentUser;
        private AganichevShoesContext _context;
        public AddProductView(User user)
        {
            InitializeComponent();
            _currentUser = user;
            _context = new AganichevShoesContext();

            cbCategory.ItemsSource = _context.ProductCategories.ToList();
            cbProductNames.ItemsSource = _context.ProductNames.ToList();
            cbProviders.ItemsSource = _context.Manufacturers.ToList();
            cbSuppliers.ItemsSource = _context.Suppliers.ToList();
            cbUnits.ItemsSource = _context.Units.ToList();
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            var openProductView = new ProductView(_currentUser);
            this.Close();
            openProductView.Show();
        }

        private void btnLoadPhoto(object sender, RoutedEventArgs e)
        {
            OpenFileDialog selectedImage = new OpenFileDialog();

            bool? resultDialog = selectedImage.ShowDialog();
            if (resultDialog == true)
            {
               
                BitmapImage bitmap = new BitmapImage(new Uri(selectedImage.FileName));

                if(bitmap.PixelHeight >= 200 && bitmap.PixelWidth >= 300)
                {
                    MessageBox.Show("неверный формат изображения. Должно быть 200 на 300 пикселей");
                }
                else
                {
                    imageName.Source = bitmap;
                    selectImage = selectedImage.FileName;

                    string soursePath = selectedImage.FileName;

                    string imageFoldPath = System.IO.Path.Combine(_projePath, "Images");

                    string fileName = Path.GetFileName(soursePath);

                    string destPath = Path.Combine(imageFoldPath, fileName);

                    File.Copy(soursePath, destPath, true);
                }

            }            
        }

        private void btnAddProduct(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(tbArticle.Text) ||
               string.IsNullOrWhiteSpace(tbDescription.Text) ||
               string.IsNullOrWhiteSpace(tbDiscount.Text) ||
               string.IsNullOrWhiteSpace(tbPrice.Text) ||
               string.IsNullOrWhiteSpace(tbQuantity.Text))
            {
                MessageBox.Show("Пожалуйста, заполните все поля.");

            }
            if (!double.TryParse(tbPrice.Text, out double price) || price < 0)
            {
                MessageBox.Show("Некорректное значение цены.");
                return;
            }
            if (!int.TryParse(tbDiscount.Text, out int desc) || desc < 0 || desc > 100)
            {
                MessageBox.Show("Некорректное значение скидки.");
                return;
            }
            if (!int.TryParse(tbQuantity.Text, out int countOnStorage) || countOnStorage < 0)
            {
                MessageBox.Show("Некорректное значение количества на складе.");
                return;
            }
            try
            {
                
                Product newProduct = new Product()
                {
                    Id = _context.Products.Max(q => q.Id) + 1,
                    Description = tbDescription.Text,
                    Discount = int.Parse(tbDiscount.Text),
                    Price = int.Parse(tbPrice.Text),
                    CountOnStorage = int.Parse(tbQuantity.Text),
                    FkProductName = ((ProductName)cbProductNames.SelectedItem).Id,
                    FkProductCategory = ((ProductCategory)cbCategory.SelectedItem).Id,
                    FkUnit = ((Unit)cbUnits.SelectedItem).Id,
                    FkManufacturer = ((Manufacturer)cbProviders.SelectedItem).Id,
                    FkSupplier = ((Supplier)cbSuppliers.SelectedItem).Id,
                    Photo = selectImage,
                    Article = new Random().Next(0, 100).ToString()
                };
                _context.Products.Add(newProduct);
                _context.SaveChanges();
                MessageBox.Show("Успешно создано");

                var openProductView = new ProductView(_currentUser);
                this.Close();
                openProductView.Show();
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
