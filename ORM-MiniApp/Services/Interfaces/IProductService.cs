﻿using ORM_MiniApp.Dtos.ProductDtos;
using ORM_MiniApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORM_MiniApp.Services.Interfaces
{
    internal interface IProductService
    {
        Task AddProductAsync(ProductPostDto newProduct);
        Task UpdateProductAsync(ProductGetDto product);
        Task DeleteProductAsync(int id);
        Task<List<ProductGetDto>> GetProductsAsync();
        Task<ProductGetDto> GetProductByIdAsync(int id);
        Task<List<ProductGetDto>> SearchProducts(string name);
    }
}
