using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Lab2_Module2.Models;
namespace Lab2_Module2.Controllers
{
    /// <summary>
    /// Логика взаимодействия для EquipmentItemController.xaml
    /// </summary>
    public partial class EquipmentItemController : UserControl
    {
        public EquipmentItemController(Product products)
        {
            InitializeComponent();

            DataContext = products;

            if (products.Discount > 15)
            {
                DiscountTextBlock.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#2E8B57"));
                DiscountTextBlock.TextDecorations = TextDecorations.Underline;
            }

            if (products.Discount > 0)
            {
                RunPrice.Foreground = new SolidColorBrush(Colors.Red);
                RunPrice.TextDecorations.Add(TextDecorations.Strikethrough);
                RunNewPrice.Text = $"{products.Price - (products.Price * products.Discount) / 100}";
            }

            if (products.NumberUnits == 0)
            {
                RunNumberUnits.Foreground = new SolidColorBrush(Colors.Yellow);
                RunNumberUnits.TextDecorations.Add(TextDecorations.Strikethrough);
            }
        }
    }
}
