using MongoDB.Driver;
using PizzaAsAService.MenuService.Api.Entities;
using System.Collections.Generic;

namespace PizzaAsAService.MenuService.Api.Data;

public class MenuContextSeed
{
    public static void SeedData(IMongoCollection<Product> products)
    {
        if (!products.Find(products => true).Any())
            products.InsertManyAsync(GetSeedData());
    }

    private static IReadOnlyCollection<Product> GetSeedData()
    {
        return new List<Product>
        {
            new Product
            {
                Name = "Small Cheese Pizza",
                Category = nameof(Categories.Pizza),
                Description = "12\" Cheese Pizza",
                Price = 10.00m,
                Image = "CheesePizza.png"
            }
        };
    }
}

