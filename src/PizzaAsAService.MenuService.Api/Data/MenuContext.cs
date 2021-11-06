using MongoDB.Driver;
using PizzaAsAService.MenuService.Api.Data.Interfaces;
using PizzaAsAService.MenuService.Api.Entities;

namespace PizzaAsAService.MenuService.Api.Data;

public class MenuContext : IMenuContext
{
    public MenuContext(IMenuDatabaseSettings settings)
    {
        var client = new MongoClient(settings.ConnectionString);
        var database = client.GetDatabase(settings.DatabaseName);

        Products = database.GetCollection<Product>(settings.CollectionName);

        MenuContextSeed.SeedData(Products);
    }

    public IMongoCollection<Product> Products { get; }
}

