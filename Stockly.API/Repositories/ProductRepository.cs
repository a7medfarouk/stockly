using Microsoft.EntityFrameworkCore;
using Stockly.API.Data;
using Stockly.API.Models;
using Stockly.API.Repositories.Interfaces;

namespace Stockly.API.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly AppDbContext _context;

    public ProductRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Product> CreateAsync(Product product)
    {
        await _context.Products.AddAsync(product);
        await _context.SaveChangesAsync();

        return product;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var product = await _context.Products.FindAsync(id);

        if (product == null)
        {
            return false;
        }

        _context.Products.Remove(product);
        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<IEnumerable<Product>> GetAllAsync()
    {
        return await _context.Products.ToListAsync();
    }

    public async Task<Product?> GetByIdAsync(int id)
    {
        return await _context.Products.FindAsync(id);
    }

    public async Task<IEnumerable<Product>> SearchByNameAsync(string name)
    {
        return await _context.Products.Where(product => product.Name.Contains(name)).ToListAsync();
    }

    public async Task<Product?> UpdateAsync(Product product)
    {
        var existingProduct = await _context.Products.FindAsync(product.Id);

        if (existingProduct == null)
        {
            return null;
        }

        existingProduct.Name = product.Name;
        existingProduct.Description = product.Description;
        existingProduct.Price = product.Price;
        existingProduct.StockQuantity = product.StockQuantity;

        await _context.SaveChangesAsync();

        return existingProduct;
    }
}