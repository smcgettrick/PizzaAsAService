using MongoDB.Driver;
using PizzaAsAService.MenuService.Api.Entities;

namespace PizzaAsAService.MenuService.Api.Data.Interfaces;

public interface IMenuContext
{
    IMongoCollection<Product> Products { get; }
}

