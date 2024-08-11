using Microsoft.EntityFrameworkCore;
using ORM_MiniApp.Contexts;
using ORM_MiniApp.Dtos.ProductDtos;
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
        public async Task AddProductAsync(ProductPostDto newProduct)
        {
            Product product = new Product()
            {
                Name=newProduct.Name,
                Price=newProduct.Price,
                Description=newProduct.Description,
                Stock=newProduct.Stock,
                UpdatedAt=DateTime.UtcNow,
                CreatedAt=DateTime.UtcNow
            };
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

        public async Task<ProductGetDto> GetProductByIdAsync(int id)
        {
            var product = await _context.Products.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id);
            if (product == null)
                throw new NotFoundException($"Can find product with id:{id}");
            var productDto = new ProductGetDto
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                Stock = product.Stock,
                Description = product.Description
            };
            return productDto;
        }

        public async Task<List<ProductGetDto>> GetProductsAsync()
        {
            var products = await _context.Products.AsNoTracking().ToListAsync();
            var productDtos = products.Select(product => new ProductGetDto
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                Stock = product.Stock,
                Description = product.Description,
            }).ToList();
            return productDtos;
        }

        public async Task<List<ProductGetDto>> SearchProducts(string name)
        {
            var products=await _context.Products.Where(p=>p.Name.Contains(name)).ToListAsync();
            var productDtos = products.Select(product => new ProductGetDto
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                Stock = product.Stock,
                Description = product.Description
            }).ToList();
            return productDtos;

        }

        public async Task UpdateProductAsync(ProductGetDto product)
        {
            var productDb = await _context.Products.AsNoTracking().FirstOrDefaultAsync(p => p.Id == product.Id);
            if (productDb == null)
                throw new NotFoundException($"Can find product with id:{product.Id}");
            var productDto = new Product()
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                Stock = product.Stock,
                Description = product.Description,
                UpdatedAt = DateTime.UtcNow
            };
            _context.Products.Update(productDto);
            await _context.SaveChangesAsync();
        }
    }
}
