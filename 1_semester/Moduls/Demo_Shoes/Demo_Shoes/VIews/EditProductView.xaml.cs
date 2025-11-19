using System.IO;
using System.Windows;
using System.Windows.Media.Imaging;
using Demo_Shoes.Models;
using Microsoft.Win32;
namespace Demo_Shoes.VIews
{
    /// <summary>
    /// Логика взаимодействия для EditProductView.xaml
    /// </summary>
    public partial class EditProductView : Window
    {
        private readonly AganichevShoesContext _context;
        private Product _currentProduct;
        private string _newPhotoPath = null;
        private string _oldPhotoName = null;

        public EditProductView(AganichevShoesContext context)
        {
            InitializeComponent();
            _context = context;
        }

        public void InitializeData(Product product)
        {
            _currentProduct = product;
            LoadComboBoxes();
            FillFields();
        }

        private void LoadComboBoxes()
        {
            CbCategory.ItemsSource = _context.ProductCategories.ToList();
            CbManufacturer.ItemsSource = _context.Manufacturers.ToList();
            CbSupplier.ItemsSource = _context.Suppliers.ToList();
            CbUnit.ItemsSource = _context.Units.ToList();
        }

        private void FillFields()
        {
            TbId.Text = _currentProduct.Id.ToString();

            tbName.Text = _currentProduct.FkProductNameNavigation?.Name;

            CbCategory.SelectedValue = _currentProduct.FkProductCategory;
            CbManufacturer.SelectedValue = _currentProduct.FkManufacturer;
            CbSupplier.SelectedValue = _currentProduct.FkSupplier;
            CbUnit.SelectedValue = _currentProduct.FkUnit;

            TbPrice.Text = _currentProduct.Price?.ToString("N2");
            TbCount.Text = _currentProduct.CountOnStorage?.ToString();
            TbDiscount.Text = _currentProduct.Discount?.ToString();
            TbDescription.Text = _currentProduct.Description;

            _oldPhotoName = _currentProduct.Photo;
            ShowImage(_oldPhotoName);
        }

        private void ShowImage(string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
            {
                fileName = "picture.png";
            }

            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Photos", fileName);
            if (!File.Exists(path))
            {
                path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Photos", "picture.png");
            }

            try
            {
                BitmapImage bmp = new BitmapImage();
                bmp.BeginInit();
                bmp.CacheOption = BitmapCacheOption.OnLoad; 
                bmp.UriSource = new Uri(path);
                bmp.EndInit();
                ImgProduct.Source = bmp;
            }
            catch { ImgProduct.Source = null; }
        }

        private void BtnChangePhoto_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "Images|*.jpg;*.jpeg;*.png";
            if (dlg.ShowDialog() == true)
            {
                var bmp = new BitmapImage(new Uri(dlg.FileName));

                ImgProduct.Source = bmp;
                _newPhotoPath = dlg.FileName; 
            }
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(tbName.Text)) { MessageBox.Show("Введите имя!"); return; }
            if (!decimal.TryParse(TbPrice.Text, out decimal price) || price < 0) { MessageBox.Show("Ошибка цены"); return; }
            if (!double.TryParse(TbCount.Text, out double count) || count < 0) { MessageBox.Show("Ошибка количества"); return; }
            double.TryParse(TbDiscount.Text, out double discount);

            try
            {
                string nameInput = tbName.Text.Trim();
                int productNameId;

                var existingName = _context.ProductNames.FirstOrDefault(n => n.Name.ToLower() == nameInput.ToLower());

                if (existingName != null)
                {
                    productNameId = existingName.Id;
                }
                else
                {
                    ProductName newNameEntity = new ProductName { Name = nameInput };

                    int newNameId = 1;
                    if (_context.ProductNames.Any())
                        newNameId = _context.ProductNames.Max(x => x.Id) + 1;

                    newNameEntity.Id = newNameId;
                    _context.ProductNames.Add(newNameEntity);
                    _context.SaveChanges();
                    productNameId = newNameId;
                }

                _currentProduct.FkProductName = productNameId;

                _currentProduct.FkProductCategory = (int?)CbCategory.SelectedValue;
                _currentProduct.FkManufacturer = (int?)CbManufacturer.SelectedValue;
                _currentProduct.FkSupplier = (int?)CbSupplier.SelectedValue;
                _currentProduct.FkUnit = (int?)CbUnit.SelectedValue;

                _currentProduct.Price = (double)price;
                _currentProduct.CountOnStorage = count;
                _currentProduct.Discount = discount;
                _currentProduct.Description = TbDescription.Text;

                if (_newPhotoPath != null)
                {
                    string folder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Photos");
                    if (!Directory.Exists(folder)) Directory.CreateDirectory(folder);
                    string newName = Guid.NewGuid().ToString() + Path.GetExtension(_newPhotoPath);
                    File.Copy(_newPhotoPath, Path.Combine(folder, newName));
                    _currentProduct.Photo = newName;

                    if (!string.IsNullOrEmpty(_oldPhotoName) && _oldPhotoName != "picture.png")
                    {
                        string oldPath = Path.Combine(folder, _oldPhotoName);
                        if (File.Exists(oldPath)) File.Delete(oldPath);
                    }
                }

                _context.SaveChanges();
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
            MessageBox.Show("Редактирование товара отменено. \nВсе измененные данные не будут сохранены");
            Close();
        }  
    }
}
