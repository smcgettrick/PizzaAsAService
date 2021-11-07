using MongoDB.Driver;
using PizzaAsAService.MenuService.Api.Entities;

namespace PizzaAsAService.MenuService.Api.Data.Interfaces;

public interface IMenuDbContext
{
    IMongoCollection<Product> Products { get; }
}

