using System;
using System.Threading.Tasks;
using PizzaAsAService.BasketService.Api.Entities;

namespace PizzaAsAService.BasketService.Api.Repositories.Interfaces;

public interface IBasketRepository
{
    Task CreateBasketAsync(Basket basket);
    Task<Basket> GetBasketAsync(string basketId);
    Task<Basket> UpdateBasketAsync(Basket basket);
    Task<bool> DeleteBasketAsync(string basketId);
}