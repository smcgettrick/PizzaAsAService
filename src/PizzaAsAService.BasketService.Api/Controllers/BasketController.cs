using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PizzaAsAService.BasketService.Api.Entities;
using PizzaAsAService.BasketService.Api.Entities.Dtos;
using PizzaAsAService.BasketService.Api.Repositories.Interfaces;

namespace PizzaAsAService.BasketService.Api.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class BasketController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IBasketRepository _repository;

    public BasketController(IBasketRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    [HttpGet("{basketId}", Name = "GetBasketById")]
    [ProducesResponseType(typeof(Basket), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public async Task<ActionResult<Basket>> GetBasketById(string basketId)
    {
        var basket = await _repository.GetBasketAsync(basketId);

        if (basket is null)
            return NotFound();

        return Ok(basket);
    }

    [HttpPost]
    [ProducesResponseType(typeof(Basket), (int)HttpStatusCode.Created)]
    public async Task<ActionResult<Basket>> CreateBasket()
    {
        var basket = new Basket();
        await _repository.CreateBasketAsync(basket);

        return CreatedAtRoute(nameof(GetBasketById), new { BasketId = basket.Id }, basket);
    }

    [HttpPut("AddItem")]
    [ProducesResponseType(typeof(Basket), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public async Task<ActionResult<Basket>> AddItemToBasket(string basketId, [FromBody]Item item)
    {
        var basket = await _repository.GetBasketAsync(basketId);

        if (basket is null)
            return NotFound();

        var updated = basket.AddItem(item);

        return updated ? Ok(await _repository.UpdateBasketAsync(basket)) : Ok(basket);
    }

    [HttpPut("DeleteItem")]
    [ProducesResponseType(typeof(Basket), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public async Task<ActionResult<Basket>> DeleteItemFromBasket(string basketId, string itemId)
    {
        var basket = await _repository.GetBasketAsync(basketId);

        if (basket is null)
            return NotFound();

        var updated = basket.DeleteItem(itemId);

        return updated ? Ok(await _repository.UpdateBasketAsync(basket)) : Ok(basket);
    }

    [HttpPut("SetItemQuantity")]
    [ProducesResponseType(typeof(Basket), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public async Task<ActionResult<Basket>> SetItemQuantity(string basketId, string itemId, int quantity)
    {
        var basket = await _repository.GetBasketAsync(basketId);

        if (basket is null)
            return NotFound();

        var updated = basket.SetItemQuantity(itemId, quantity);

        return updated ? Ok(await _repository.UpdateBasketAsync(basket)) : Ok(basket);
    }

    [HttpDelete("{basketId}")]
    [ProducesResponseType(typeof(void), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> DeleteBasket(string basketId)
    {
        return Ok(await _repository.DeleteBasketAsync(basketId));
    }
}