using System.Windows;
using System.Windows.Controls;
using Lab2_Module2.Controllers;
using Lab2_Module2.Models;

namespace Lab2_Module2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

        }

        //public void DrawProduct(List<Product> products)
        //{
        //    BoxEquipment.ItemsSourse = AganichevMusicEquipmentRentalContext.Products.Select
        //        (x => new EquipmentItemController(x));
        //}

        private void BoxEquipment_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            ListBox list = sender as ListBox;

            EquipmentItemController item = list.SelectedItem as EquipmentItemController;
        }
    }
}