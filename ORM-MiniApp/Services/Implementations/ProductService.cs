using Microsoft.EntityFrameworkCore;
using ORM_MiniApp.Contexts;
using ORM_MiniApp.Exceptions;
using ORM_MiniApp.Models;
using ORM_MiniApp.Services.Interfaces;

namespace ORM_MiniApp.Services.Implementations
{
    internal class ProductService : IProductService
    {
        private readonly AppDbContext _context;
        public ProductService()
        {
            _context= new AppDbContext();
        }
        public async Task AddProductAsync(Product product)
        {
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync(); 
        }

        public async Task DeleteProductAsync(int id)
        {
            var product = await _context.Products.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id);
            if (product == null)
                throw new NotFoundException($"Can find product with id:{id}");
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
        }

        public async Task<Product> GetProductByIdAsync(int id)
        {
            var product = await _context.Products.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id);
            if (product == null)
                throw new NotFoundException($"Can find product with id:{id}");
            return product;
        }

        public async Task<List<Product>> GetProductsAsync()
        {
            var products = await _context.Products.AsNoTracking().ToListAsync();
            return products;
        }

        public async Task<List<Product>> SearchProducts(string name)
        {
            var products=await _context.Products.Where(p=>p.Name.Contains(name)).ToListAsync();
            return products;

        }

        public async Task UpdateProductAsync(Product product)
        {
            var productDb=await _context.Products.AsNoTracking().FirstOrDefaultAsync(p=>p.Id==product.Id);
            if (product == null)
                throw new NotFoundException($"Can find product with id:{product.Id}");
            _context.Products.Update(product);
            await _context.SaveChangesAsync();
        }
    }
}
