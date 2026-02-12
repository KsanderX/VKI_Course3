using System.IO;
using System.Windows;
using System.Windows.Media.Imaging;
using Demodemo.Models;
using Demodemo.ViewModel;
using Microsoft.Win32;

namespace Demodemo.Views
{
    /// <summary>
    /// Логика взаимодействия для ProductEditView.xaml
    /// </summary>
    public partial class ProductEditView : Window
    {
        private AganichevShoesContext _context;
        private User _currentUser;
        private string _projePath = AppDomain.CurrentDomain.BaseDirectory;
        private string selectImage;
        public ProductEditView(AganichevShoesContext context, User currentUser)
        {
            InitializeComponent();
            _context = context;
            _currentUser = currentUser;

            cbCategory.ItemsSource = _context.ProductCategories.ToList();
            cbProductNames.ItemsSource = _context.ProductNames.ToList();
            cbProviders.ItemsSource = _context.Manufacturers.ToList();
            cbSuppliers.ItemsSource = _context.Suppliers.ToList();
            cbUnits.ItemsSource = _context.Units.ToList();
        }
      
        private void btnSaveProduct(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(tbArticle.Text) ||
              string.IsNullOrWhiteSpace(tbQuantity.Text) ||
              string.IsNullOrWhiteSpace(tbDescription.Text) ||
              string.IsNullOrWhiteSpace(tbDiscount.Text) ||
              string.IsNullOrWhiteSpace(tbPrice.Text))

            {
                MessageBox.Show("Поля не могут быть пустыми, заполните их!");
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

            _context.SaveChanges();
            MessageBox.Show("Изменение успешно сохранео");

            var openProductView = new ProductView(_currentUser);
            this.Close();
            openProductView.Show();
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            var openProductView = new ProductView(_currentUser);
            this.Close();
            openProductView.Show();
        }
        private void btnLoadPhoto_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog selectedImage = new OpenFileDialog();


            if (selectedImage.ShowDialog() == true)
            {

                BitmapImage bitmap = new BitmapImage(new Uri(selectedImage.FileName));

                if (bitmap.PixelHeight >= 200 && bitmap.PixelWidth >= 300)
                {
                    MessageBox.Show("неверный формат изображения. Должно быть 200 на 300 пикселей");
                }
                else
                {
                    imageName.Source = bitmap;
                    selectImage = selectedImage.FileName;

                    string soursePath = selectedImage.FileName;

                    string imageFoldPath = Path.Combine(_projePath, "Images");

                    string fileName = Path.GetFileName(soursePath);

                    string destPath = Path.Combine(imageFoldPath, fileName);

                    File.Copy(soursePath, destPath, true);
                }
            }
        }        
    }
}

//Scaffold-DbContext "Server=server;DataBase= DB;TrustServerCertificate=True; IntegratedSecurity=True;" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models 

//Scaffold-DbContext "Server=server;DataBase=DB;TrustServerCertificate=True;IntegratedSecurity=True;" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models