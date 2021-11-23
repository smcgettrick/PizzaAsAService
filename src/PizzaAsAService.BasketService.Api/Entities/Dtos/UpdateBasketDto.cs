using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PizzaAsAService.BasketService.Api.Entities.Dtos;

public class UpdateBasketDto
{
    [Required] public string BasketId { get; set; }
    [Required] public List<Item> Items { get; set; }
}