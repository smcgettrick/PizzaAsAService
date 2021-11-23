using System.ComponentModel.DataAnnotations;

namespace PizzaAsAService.BasketService.Api.Entities;

public class Item
{
    [Key] [Required] public string Id { get; set; }
    [Required] public string Name { get; set; }
    [Required] public decimal Price { get; set; }
    [Required] public int Quantity { get; set; }
    public string Notes { get; set; }
}