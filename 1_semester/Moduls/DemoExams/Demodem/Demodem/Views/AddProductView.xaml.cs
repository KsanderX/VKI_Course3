using System.IO;
using System.Windows;
using System.Windows.Media.Imaging;
using Demodem.Models;
using Microsoft.Win32;

namespace Demodem.Views
{
    /// <summary>
    /// Логика взаимодействия для AddProductView.xaml
    /// </summary>
    public partial class AddProductView : Window
    {
        private AganichevShoesContext _context;
        private User _currentUser;
        private string _imagePath = AppDomain.CurrentDomain.BaseDirectory;
        private string selectImage;
        public AddProductView(User user)
        {
            InitializeComponent();
            _context = new AganichevShoesContext();
            _currentUser = user;

            cbCategory.ItemsSource = _context.ProductCategories.ToList();
            cbManuf.ItemsSource = _context.Manufacturers.ToList();
            cbName.ItemsSource = _context.ProductNames.ToList();
            cbSupplier.ItemsSource = _context.Suppliers.ToList();
            cbUnit.ItemsSource = _context.Units.ToList();

        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            if(string.IsNullOrWhiteSpace(tbArticle.Text) ||
                string.IsNullOrWhiteSpace(tbCountOnStorage.Text) ||
                string.IsNullOrWhiteSpace(tbDesc.Text) ||
                string.IsNullOrWhiteSpace(tbDisc.Text) ||
                string.IsNullOrWhiteSpace(tbPrice.Text))
            {
                MessageBox.Show("Поля не могут быть пустыми, заполните их!");
                return;
            }
            if (!double.TryParse(tbPrice.Text, out var price) && price < 0)
            {
                MessageBox.Show("Некорректное значение цены");
                return;
            }
            if (!double.TryParse(tbDisc.Text, out var disc) && disc < 0 || disc > 100)
            {
                MessageBox.Show("Некорректное значение скидки");
                return;
            }
            if (!double.TryParse(tbCountOnStorage.Text, out var count) && count < 0)
            {
                MessageBox.Show("Некорректное значение кол-во на складе");
                return;
            }

            try
            {
                Product newProduct = new Product()
                {
                    Id = _context.Products.Max(q => q.Id) + 1,
                    Description = tbDesc.Text,
                    Discount = int.Parse(tbDisc.Text),
                    CountOnStorage = int.Parse(tbCountOnStorage.Text),
                    Price = int.Parse(tbPrice.Text),
                    FkManufacturer = ((Manufacturer)cbManuf.SelectedItem).Id,
                    FkProductCategory = ((ProductCategory)cbCategory.SelectedItem).Id,
                    FkProductName = ((ProductName)cbName.SelectedItem).Id,
                    FkSupplier = ((Supplier)cbSupplier.SelectedItem).Id,
                    FkUnit = ((Unit)cbUnit.SelectedItem).Id,
                    Photo = selectImage,
                    Article = new Random().Next(100, 999).ToString(),
                };
                 _context.Products.Add(newProduct);
                _context.SaveChanges();
                MessageBox.Show("Создание продукта прошло успешно");
                var openProductView = new ProductView(_currentUser);
                this.Close();
                openProductView.Show();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            var openProductView = new ProductView(_currentUser);
            this.Close();
            openProductView.Show();
        }

        private void btnAddImg_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog selectedImage = new OpenFileDialog();
            bool? resultDialog = selectedImage.ShowDialog();

            if (resultDialog == true)
            {
                BitmapImage bitmap = new BitmapImage(new Uri(selectedImage.FileName));
                if (bitmap.PixelHeight >= 200 && bitmap.PixelWidth >= 300)
                {
                    MessageBox.Show("Фотография должна быть форматом 200 на 300 пикселей!");
                }
                else
                {
                    imgPhoto.Source = bitmap;

                    selectImage = selectedImage.FileName;

                    string soursePath = selectedImage.FileName;

                    string imageFolderPath = Path.Combine(_imagePath, "Images");

                    string fileName = Path.GetFileName(soursePath);

                    string destPath = Path.Combine(imageFolderPath, fileName);

                    File.Copy(soursePath, destPath, true);
                }
            }
        }
    }
}
