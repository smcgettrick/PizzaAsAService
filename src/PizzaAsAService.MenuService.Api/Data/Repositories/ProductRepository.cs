using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Driver;
using PizzaAsAService.MenuService.Api.Data.Interfaces;
using PizzaAsAService.MenuService.Api.Data.Repositories.Interfaces;
using PizzaAsAService.MenuService.Api.Entities;

namespace PizzaAsAService.MenuService.Api.Data.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly IMenuDbContext _context;

    public ProductRepository(IMenuDbContext context)
    {
        _context = context;
    }

    public async Task<IReadOnlyCollection<Product>> GetProducts()
    {
        return await _context
            .Products
            .Find(p => true)
            .ToListAsync();
    }

    public async Task<Product> GetProduct(string id)
    {
        return await _context
            .Products
            .Find(p => p.Id == id)
            .FirstOrDefaultAsync();
    }

    public async Task<IReadOnlyCollection<Product>> GetProductByName(string name)
    {
        return await _context
            .Products
            .Find(p => p.Name == name)
            .ToListAsync();
    }

    public async Task<IReadOnlyCollection<Product>> GetProductByCategory(string category)
    {
        return await _context
            .Products
            .Find(p => p.Category == category)
            .ToListAsync();
    }
}