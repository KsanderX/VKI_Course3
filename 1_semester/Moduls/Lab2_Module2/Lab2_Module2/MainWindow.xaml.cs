using System.IO;
using System.Windows;
using Lab2_Module2.Controllers;
using Lab2_Module2.Models;
using Microsoft.EntityFrameworkCore;

namespace Lab2_Module2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        AganichevMusicEquipmentRentalContext context = new AganichevMusicEquipmentRentalContext();
        public MainWindow()
        {
            InitializeComponent();
            GetAllProducts();
        }

        public void GetAllProducts()
        {
            var allProducts = context.Products
                                .Include(p => p.FkEquipmentTypeNavigation)
                                .Include(p => p.FkManufacturerNavigation)
                                .Include(p => p.FkSupplierNavigation)
                                .ToList();

            string basePath = AppDomain.CurrentDomain.BaseDirectory;

            string photosFolder = "Photos";

            foreach (var item in allProducts)
            {
                if (string.IsNullOrWhiteSpace(item.Photo))
                {

                    item.Photo = Path.Combine(basePath, photosFolder);
                }
                else
                {
                    item.Photo = Path.Combine(basePath, photosFolder, item.Photo);
                }

                EquipmentItemController equipment = new(item);
                BoxEquipment.Items.Add(equipment);
            }
        }

        private void BoxEquipment_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {

        }
    }    
}