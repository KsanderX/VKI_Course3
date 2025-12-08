using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Demodemo.Models;

namespace Demodemo.Controllers
{
    /// <summary>
    /// Логика взаимодействия для ProductItemController.xaml
    /// </summary>
    public partial class ProductItemController : UserControl
    {
        private string _projPath = AppDomain.CurrentDomain.BaseDirectory;
        public ProductItemController(Product product)
        {
            InitializeComponent();
            DataContext = product;

            if ( product.Discount>15)
            {
                BorderDiscount.Background = new BrushConverter().ConvertFrom("#2E8B57") as SolidColorBrush;
            }

            if(product.Discount>0)
            {
                runPrice.TextDecorations = TextDecorations.Strikethrough;
                runPrice.Foreground = Brushes.Red;

                runNewPrice.Text = (product.Price * (1 - product.Discount/100.0)).ToString();
            }

            if(product.CountOnStorage ==0)
            {
                tbCountOnStorage.Foreground = Brushes.Blue;
            }
            try
            {
                string path = null;

                if (!string.IsNullOrWhiteSpace(product.Photo))
                {
                    path = Path.Combine(_projPath, "Images", product.Photo);
                }
                if(string.IsNullOrWhiteSpace(path) || !File.Exists(path))
                {
                    path = Path.Combine(_projPath, "Images", "Defaults", "picture.png");
                }

                BitmapImage bitmap = new BitmapImage(new Uri(path));
                boxImage.Source = bitmap;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка отображения фото",ex.Message);
                return;
            }
           
        }
    }
}
