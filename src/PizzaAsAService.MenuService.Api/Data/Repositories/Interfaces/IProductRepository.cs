using System.Collections.Generic;
using System.Threading.Tasks;
using PizzaAsAService.MenuService.Api.Entities;

namespace PizzaAsAService.MenuService.Api.Data.Repositories.Interfaces;

public interface IProductRepository
{
    Task<IReadOnlyCollection<Product>> GetProducts();
    Task<Product> GetProduct(string id);
    Task<IReadOnlyCollection<Product>> GetProductByName(string name);
    Task<IReadOnlyCollection<Product>> GetProductByCategory(string category);
}