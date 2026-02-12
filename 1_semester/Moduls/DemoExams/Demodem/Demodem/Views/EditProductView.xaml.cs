using System.IO;
using System.Windows;
using System.Windows.Media.Imaging;
using Demodem.Models;
using Microsoft.Win32;

namespace Demodem.Views
{
    /// <summary>
    /// Логика взаимодействия для EditProductView.xaml
    /// </summary>
    public partial class EditProductView : Window
    {
        private AganichevShoesContext _context;
        private User _currentUser;
        private string _imagePath = AppDomain.CurrentDomain.BaseDirectory;
        private string selectImage;
        public EditProductView(AganichevShoesContext context, User user)
        {
            InitializeComponent();
            _context = context;
            _currentUser = user;

            cbCategory.ItemsSource = _context.ProductCategories.ToList();
            cbManuf.ItemsSource = _context.Manufacturers.ToList();
            cbSupplier.ItemsSource = _context.Suppliers.ToList();
            cbName.ItemsSource = _context.ProductNames.ToList();
            cbUnit.ItemsSource = _context.Units.ToList();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(tbArticle.Text) ||
               string.IsNullOrWhiteSpace(tbCountOnStorage.Text) ||
               string.IsNullOrWhiteSpace(tbDesc.Text) ||
               string.IsNullOrWhiteSpace(tbDisc.Text) ||
               string.IsNullOrWhiteSpace(tbPrice.Text)||
               string.IsNullOrWhiteSpace(cbUnit.Text) ||
               string.IsNullOrWhiteSpace(cbSupplier.Text)||
               string.IsNullOrWhiteSpace(cbName.Text) ||
               string.IsNullOrWhiteSpace(cbManuf.Text) ||
               string.IsNullOrWhiteSpace(cbCategory.Text))
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
            if(!double.TryParse(tbCountOnStorage.Text, out var count) && count < 0)
            {
                MessageBox.Show("Некорректное значение кол-во на складе");
                return;
            }

            _context.SaveChanges();
            MessageBox.Show("Изменения сохранены успешно!");

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

        private void btnAddImg_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog selectedImage = new OpenFileDialog();
            bool? resultDialog = selectedImage.ShowDialog();

            if (resultDialog == true)
            {
                BitmapImage bitmap = new BitmapImage(new Uri(selectedImage.FileName));
                if(bitmap.PixelHeight >= 200 && bitmap.PixelWidth >= 300)
                {
                    MessageBox.Show("Фотография должна быть форматом 200 на 300 пикселей!");
                }
                else
                {
                    imgPhoto.Source = bitmap;  

                    selectImage = selectedImage.FileName;

                    string soursePath = selectedImage.FileName;                       

                    string imageFolderPath  = Path.Combine(_imagePath, "Images");
                    
                    string fileName  = Path.GetFileName(soursePath);

                    string destPath = Path.Combine(imageFolderPath, fileName);
                    
                    File.Copy(soursePath, destPath, true);
                }
            }
        }
    }
}
