using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Demo_Shoes.Models;

namespace Demo_Shoes.Controllers
{
    /// <summary>
    /// Логика взаимодействия для ProductItemController.xaml
    /// </summary>
    public partial class ProductItemController : UserControl
    {
        public ProductItemController()
        {
            InitializeComponent();

            this.DataContextChanged += UserControl_DataContextChanged;
        }

        private void UserControl_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {      
            if (DataContext is Product product)
            {
                string basePath = AppDomain.CurrentDomain.BaseDirectory;
                string photosFolder = "Photos";
                string fullPath = null;

                if (!string.IsNullOrWhiteSpace(product.Photo))
                {
                    fullPath = Path.Combine(basePath, photosFolder, product.Photo);
                }

                if(string.IsNullOrWhiteSpace(fullPath) || !File.Exists(fullPath))
                {
                    fullPath = Path.Combine(basePath, photosFolder, "picture.png");
                }
                Uri uri = new Uri(fullPath);
                BitmapImage bitmap = new(uri);
                ImgProduct.Source = bitmap;

                MainBorder.Background = Brushes.White;
                RunPrice.TextDecorations = null;
                RunPrice.Foreground = Brushes.Black;
                RunNewPrice.Text = "";

                if (product.Discount > 15)
                {
                    var brush = new BrushConverter().ConvertFrom("#2E8B57") as SolidColorBrush;
                    BorderDiscount.Background = brush;
                }

                if (product.Discount > 0 && product.Price != null)
                {
                    RunPrice.TextDecorations = TextDecorations.Strikethrough;
                    RunPrice.Foreground = Brushes.Red;

                    double oldPrice = product.Price.Value;
                    double discount = product.Discount.Value;
                    double newPrice = oldPrice - (oldPrice * discount / 100);

                    RunNewPrice.Text = $" {newPrice:N2} р.";
                    RunNewPrice.Foreground = Brushes.Black;
                }

                if (product.CountOnStorage == 0)
                {
                    BoxCount.Foreground = Brushes.Blue; 
                }
            }

        }
    }
}
