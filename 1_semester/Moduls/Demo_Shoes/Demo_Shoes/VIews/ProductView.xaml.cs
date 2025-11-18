using System.IO;
using System.Windows;
using System.Windows.Controls;
using Demo_Shoes.Controllers;
using Demo_Shoes.Models;
using Microsoft.EntityFrameworkCore;

namespace Demo_Shoes
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class ProductView : Window
    {
        private readonly IServiceProvider _service;
        private readonly AganichevShoesContext _context;
        public ProductView(AganichevShoesContext context, IServiceProvider service)
        {
            InitializeComponent();
            _service = service;
            _context = context;
            GetAllProducts();
        }
        public void GetAllProducts()
        {
            var allProducts = _context.Products
                .Include(p => p.FkManufacturerNavigation)
                .Include(p => p.FkProductCategoryNavigation)
                .Include(p => p.FkSupplierNavigation)
                .Include(p => p.FkUnitNavigation)
                .Include(p => p.FkProductNameNavigation)
                .ToList();

            string basePath = AppDomain.CurrentDomain.BaseDirectory;
            string photosFolder = "Photos";
            foreach (var item in allProducts)
            {
                string fullPath = null;
                if (!string.IsNullOrWhiteSpace(item.Photo))
                {
                    fullPath = Path.Combine(basePath, photosFolder, item.Photo);
                }
                if (string.IsNullOrWhiteSpace(fullPath))
                {
                    item.Photo = Path.Combine(basePath, photosFolder, "picture.png");
                }
                else
                {
                    item.Photo = fullPath;
                }
                ProductItemController productController = new ProductItemController();
                productController.DataContext = item;
                BoxProduct.Items.Add(productController);
            }
        }      
    }
}