using System;
using PizzaAsAService.BasketService.Api.Data.Interfaces;
using StackExchange.Redis;

namespace PizzaAsAService.BasketService.Api.Data;

public class BasketContext : IBasketContext
{
    public BasketContext(ConnectionMultiplexer connection)
    {
        if (connection is null)
            throw new ArgumentNullException(nameof(connection));

        Redis = connection.GetDatabase();
    }

    public IDatabase Redis { get; }
}