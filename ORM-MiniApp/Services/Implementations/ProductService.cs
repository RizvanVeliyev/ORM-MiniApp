using Microsoft.EntityFrameworkCore;
using ORM_MiniApp.Contexts;
using ORM_MiniApp.Dtos.ProductDtos;
using ORM_MiniApp.Exceptions;
using ORM_MiniApp.Models;
using ORM_MiniApp.Repositories.Implementations;
using ORM_MiniApp.Repositories.Interfaces;
using ORM_MiniApp.Services.Interfaces;

namespace ORM_MiniApp.Services.Implementations
{
    internal class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService()
        {
            _productRepository = new ProductRepository();
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
            await _productRepository.CreateAsync(product);
            await _productRepository.SaveChangesAsync();
        }

        public async Task DeleteProductAsync(int id)
        {
            
            var product = await _productRepository.GetSingleAsync(p => p.Id == id);
            if (product == null)
                throw new NotFoundException($"Can find product with id:{id}");
            _productRepository.Delete(product);
            await _productRepository.SaveChangesAsync();
        }

        public async Task<ProductGetDto> GetProductByIdAsync(int id)
        {
            
            var product = await _productRepository.GetSingleAsync(p => p.Id == id); 
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
            
            var products = await _productRepository.GetAllAsync();

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
            AppDbContext _context = new AppDbContext();
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
            var productDb = await _productRepository.GetSingleAsync(p => p.Id == product.Id);
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
            _productRepository.Update(productDto);
            await _productRepository.SaveChangesAsync();
        }
    }
}
