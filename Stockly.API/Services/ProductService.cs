using Stockly.API.DTOs;
using Stockly.API.Models;
using Stockly.API.Repositories.Interfaces;
using Stockly.API.Services.Interfaces;

namespace Stockly.API.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        public async Task<ProductDto> CreateProductAsync(CreateProductDto dto)
        {
            Product product = new();
            product.Name = dto.Name;
            product.Description = dto.Description;
            product.Price = dto.Price;
            product.StockQuantity = dto.StockQuantity;

            Product createdProduct = await _productRepository.CreateAsync(product);

            ProductDto productDto = new();
            productDto.Id = createdProduct.Id;
            productDto.Name = createdProduct.Name;
            productDto.Description = createdProduct.Description;
            productDto.Price = createdProduct.Price;
            productDto.StockQuantity = createdProduct.StockQuantity;

            return productDto;
            
        }

        public async Task<bool> DeleteProductAsync(int id)
        {
            return await _productRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<ProductDto>> GetAllProductsAsync()
        {
            IEnumerable<Product> products = await _productRepository.GetAllAsync();
            List<ProductDto> productDtos = new ();
            foreach (var product in products)
            {
                productDtos.Add(new ProductDto
                {
                    Id = product.Id,
                    Name = product.Name,
                    Description = product.Description,
                    Price = product.Price,
                    StockQuantity = product.StockQuantity
                });
            }
            return productDtos;
        }

        public async Task<ProductDto?> GetProductByIdAsync(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            if (product == null)
            {
                return null;
            }
            ProductDto productdto = new();
            productdto.Id = product.Id;
            productdto.Name = product.Name;
            productdto.Description = product.Description;
            productdto.Price = product.Price;
            productdto.StockQuantity = product.StockQuantity;
            return productdto;
        }

        public async Task<IEnumerable<ProductDto>> SearchProductsByNameAsync(string name)
        {
            var products = await _productRepository.SearchByNameAsync(name);
            List<ProductDto> productDtos = new ();
            foreach(var product in products)
            {
                productDtos.Add(new ProductDto
                {
                    Id = product.Id,
                    Name = product.Name,
                    Description = product.Description,
                    Price = product.Price,
                    StockQuantity = product.StockQuantity
                });
            }
            return productDtos;
            
        }

        public async Task<ProductDto?> UpdateProductAsync(int id, UpdateProductDto dto)
        {
            Product product = new();
            product.Id = id;
            product.Name = dto.Name;
            product.Description = dto.Description;
            product.Price = dto.Price;
            product.StockQuantity = dto.StockQuantity;
            var updatedProduct = await _productRepository.UpdateAsync(product);
            if (updatedProduct == null)
            {
                return null;
            }
            ProductDto updatedProductDto = new();
            updatedProductDto.Id = updatedProduct.Id;
            updatedProductDto.Name = updatedProduct.Name;
            updatedProductDto.Description = updatedProduct.Description;
            updatedProductDto.Price = updatedProduct.Price;
            updatedProductDto.StockQuantity = updatedProduct.StockQuantity;
            return updatedProductDto;
        }
    }
}
