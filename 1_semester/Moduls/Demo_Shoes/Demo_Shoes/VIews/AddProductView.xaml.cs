using System.IO;
using System.Windows;
using System.Windows.Media.Imaging;
using Demo_Shoes.Models;
using Microsoft.Win32;

namespace Demo_Shoes.VIews
{
    /// <summary>
    /// Логика взаимодействия для AddProductView.xaml
    /// </summary>
    public partial class AddProductView : Window
    {
        private readonly AganichevShoesContext _context;
        private string _selectedImageInfo = null;

        public AddProductView(AganichevShoesContext context)
        {
            InitializeComponent();
            _context = context;
            LoadComboBoxes();
        }

        private void LoadComboBoxes()
        {
            CbCategory.ItemsSource = _context.ProductCategories.ToList();
            CbManufacturer.ItemsSource = _context.Manufacturers.ToList();
            CbSupplier.ItemsSource = _context.Suppliers.ToList();
            CbUnit.ItemsSource = _context.Units.ToList();
        }

        private void BtnSelectImage_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files (*.png;*.jpg;*.jpeg)|*.png;*.jpg;*.jpeg";

            if (openFileDialog.ShowDialog() == true)
            {
                BitmapImage bitmap = new BitmapImage(new Uri(openFileDialog.FileName));

                _selectedImageInfo = openFileDialog.FileName;
            }
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TbName.Text) ||
                CbCategory.SelectedItem == null ||
                CbManufacturer.SelectedItem == null ||
                CbUnit.SelectedItem == null)
            {
                MessageBox.Show("Заполните наименование и выберите значения из списков!");
                return;
            }

            if (!decimal.TryParse(TbPrice.Text, out decimal price) || price < 0) { MessageBox.Show("Некорректная цена!"); return; }
            if (!double.TryParse(TbCount.Text, out double count) || count < 0) { MessageBox.Show("Некорректное количество!"); return; }
            double.TryParse(TbDiscount.Text, out double discount);

            try
            {
                string nameInput = TbName.Text.Trim();
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
                int newProductId = 1;
                if (_context.Products.Any())
                    newProductId = _context.Products.Max(p => p.Id) + 1;

               
                string finalPhotoName = null;
                if (_selectedImageInfo != null)
                {
                    string appFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Photos");
                    if (!Directory.Exists(appFolder)) Directory.CreateDirectory(appFolder);
                    finalPhotoName = Guid.NewGuid().ToString() + Path.GetExtension(_selectedImageInfo);
                    File.Copy(_selectedImageInfo, Path.Combine(appFolder, finalPhotoName));
                }

                Product newProduct = new Product
                {
                    Id = newProductId,
                    FkProductName = productNameId, 
                    FkProductCategory = (int)CbCategory.SelectedValue,
                    FkManufacturer = (int)CbManufacturer.SelectedValue,
                    FkSupplier = (int?)CbSupplier.SelectedValue,
                    FkUnit = (int)CbUnit.SelectedValue,
                    Price = (double)price,
                    CountOnStorage = count,
                    Discount = discount,
                    Description = TbDescription.Text,
                    Photo = finalPhotoName
                };

                _context.Products.Add(newProduct);
                _context.SaveChanges();

                MessageBox.Show("Товар добавлен!");
                DialogResult = true;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}");
            }
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            this.Close(); 
        }
    }
}

