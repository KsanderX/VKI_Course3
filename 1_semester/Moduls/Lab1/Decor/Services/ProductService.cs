using Decor.Models;
using Microsoft.EntityFrameworkCore;

namespace Decor.Services
{
    public class ProductService : IProductService
    {
        private AganichevDecorContext _context;
        public ProductService() 
        {
            _context = new AganichevDecorContext();
        }

        public List<ProductsImport> GetProducts()
        {
           return _context.ProductsImports.Include(p => p.FkТипПродукции).ToList();
        }
    }
}
