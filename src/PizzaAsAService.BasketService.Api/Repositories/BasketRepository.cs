using System;
using System.Text.Json;
using System.Threading.Tasks;
using PizzaAsAService.BasketService.Api.Data.Interfaces;
using PizzaAsAService.BasketService.Api.Entities;
using PizzaAsAService.BasketService.Api.Repositories.Interfaces;

namespace PizzaAsAService.BasketService.Api.Repositories;

public class BasketRepository : IBasketRepository
{
    private readonly IBasketContext _context;

    public BasketRepository(IBasketContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task CreateBasketAsync(Basket basket)
    {
        var basketJson = JsonSerializer.Serialize(basket);
        _ = await _context.Redis.StringSetAsync(basket.Id, basketJson);
    }

    public async Task<Basket> GetBasketAsync(string basketId)
    {
        var basket = await _context.Redis.StringGetAsync(basketId);

        if (basket.IsNullOrEmpty)
            return null;

        var basketObj = JsonSerializer.Deserialize<Basket>(basket);
        return basketObj;

        //return basket.IsNullOrEmpty
        //    ? null
        //    : JsonSerializer.Deserialize<Basket>(basket);
    }

    public async Task<Basket> UpdateBasketAsync(Basket basket)
    {
        var updated = await _context
            .Redis
            .StringSetAsync(basket.Id, JsonSerializer.Serialize(basket));

        if (!updated)
            return null;

        return await GetBasketAsync(basket.Id);
    }

    public async Task<bool> DeleteBasketAsync(string basketId)
    {
        return await _context
            .Redis
            .KeyDeleteAsync(basketId);
    }
}