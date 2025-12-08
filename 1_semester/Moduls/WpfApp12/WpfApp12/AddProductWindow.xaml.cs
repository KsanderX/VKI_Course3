using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WpfApp12.Models;

namespace WpfApp12
{
    /// <summary>
    /// Логика взаимодействия для AddProductWindow.xaml
    /// </summary>
    public partial class AddProductWindow : Window
    {
        private string _projPath = System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
        private KirillDbContext _context;
        private string _imageName;
        public AddProductWindow()
        {
            InitializeComponent();
            _context = new KirillDbContext();
            cbCategory.ItemsSource = _context.ProductCategories.ToList();
            cbProductNames.ItemsSource = _context.ProductNames.ToList();
            cbProviders.ItemsSource = _context.Providers.ToList();
            cbSuppliers.ItemsSource = _context.Suppliers.ToList();
            cbUnits.ItemsSource = _context.Units.ToList();
            
        }

        private void btnLoadPhoto(object sender, RoutedEventArgs e)
        {
            OpenFileDialog selectedImage = new OpenFileDialog();

            bool? resultDialog = selectedImage.ShowDialog();

            if (resultDialog == true) 
            { 
                BitmapImage bitmap = new BitmapImage(new Uri(selectedImage.FileName));

                if (bitmap.PixelHeight >= 200 && bitmap.PixelWidth >= 300) 
                {
                    MessageBox.Show("Формат изображения должен быть 200 высота, 300 ширина");
                }
                else
                {
                    MessageBox.Show("Все норм!");
                    BoxImage.Source = bitmap;
                    _imageName = selectedImage.FileName;

                    string sourcePath = selectedImage.FileName;

                    string imagesFolderPath = System.IO.Path.Combine(_projPath, "Images");

                    string fileName = System.IO.Path.GetFileName(sourcePath);

                    string destPath = System.IO.Path.Combine(imagesFolderPath, fileName);
                    
                    File.Copy(sourcePath, destPath, true);
                }

                


            }
            else
            {
                MessageBox.Show("Вы закрыли окно выбора фото");
            }
        }

        private void btnAddProduct(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(tbArticle.Text)
                && !string.IsNullOrWhiteSpace(tbDescription.Text)
                && !string.IsNullOrWhiteSpace(tbDiscount.Text)
                && !string.IsNullOrWhiteSpace(tbPrice.Text)
                && !string.IsNullOrWhiteSpace(tbQuantity.Text))
            {
                try
                {
                    if (int.Parse(tbPrice.Text) < 0) 
                    {
                        MessageBox.Show("Нельзя указывать отрицательную цену");
                        return;
                    }

                    if (int.Parse(tbQuantity.Text) < 0)
                    {
                        MessageBox.Show("Количество не может быть отрицательным");
                        return;
                    }

                    if (int.Parse(tbDiscount.Text) < 0)
                    {
                        MessageBox.Show("Скидка не может быть отрицательной");
                        return;
                    }

                    if (int.Parse(tbDiscount.Text) > 100)
                    {
                        MessageBox.Show("Скидка не может быть больше 100 процентов");
                        return;
                    }


                    Product product = new Product()
                    {
                        Article = tbArticle.Text,
                        Description = tbDescription.Text,
                        Discount = int.Parse(tbDiscount.Text),
                        Price = int.Parse(tbPrice.Text),
                        Quantity = int.Parse(tbQuantity.Text),
                        ProductNameId = ((ProductName)cbProductNames.SelectedItem).Id,
                        ProductCategoryId = ((ProductCategory)cbCategory.SelectedItem).Id,
                        UnitId = ((Unit)cbUnits.SelectedItem).Id,
                        ProviderId = ((Provider)cbProviders.SelectedItem).Id,
                        SupplierId = ((Supplier)cbSuppliers.SelectedItem).Id,
                        Photo = _imageName,
                    };

                    _context.Products.Add(product);
                    _context.SaveChanges();
                    MessageBox.Show("Создано");

                    var mainWindow = new MainWindow();
                    this.Close();
                    mainWindow.ShowDialog();

                }
                catch(Exception ex)
                {
                    MessageBox.Show("Ошибка формата данных");
                }
            }
            else
            {
                MessageBox.Show("Заполните поля");
            }

           
        }

        private void btnOpenMainView(object sender, RoutedEventArgs e)
        {
            var mainView = new MainWindow();
            this.Close();
            mainView.ShowDialog();
        }
    }
}
