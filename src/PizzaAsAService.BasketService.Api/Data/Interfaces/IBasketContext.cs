using StackExchange.Redis;

namespace PizzaAsAService.BasketService.Api.Data.Interfaces;

public interface IBasketContext
{
    IDatabase Redis { get; }
}