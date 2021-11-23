using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;

namespace PizzaAsAService.BasketService.Api.Entities;

public class Basket
{
    public Basket()
    {
        Id = Guid.NewGuid().ToString();
        Items = new List<Item>();
    }

    public bool AddItem(Item item)
    {
        if (item == null)
            throw new ArgumentNullException(nameof(item));

        if (Items.Any(i => i.Id == item.Id))
            return false;

        Items.Add(item);

        return true;
    }

    public bool DeleteItem(string itemId)
    {
        if (itemId is null)
            throw new ArgumentNullException(nameof(itemId));

        var itemInBag = Items.SingleOrDefault(i => i.Id == itemId);
        if (itemInBag is null)
            return false;

        Items.Remove(itemInBag);

        return true;
    }

    public bool SetItemQuantity(string itemId, int quantity)
    {
        if (quantity == 0)
            return false;

        if (itemId is null)
            throw new ArgumentNullException(nameof(itemId));

        if (Items.SingleOrDefault(i => i.Id == itemId) is not null)
        {
            Items.Single(i => i.Id == itemId).Quantity = quantity;
            return true;
        }

        return false;
    }

    [Key] [Required] [JsonInclude] public string Id { get; private set; }
    [Required] [JsonInclude] public List<Item> Items { get; private set; }
    public decimal TotalPrice => Items.Sum(item => item.Price * item.Quantity);
    public decimal TotalItems => Items.Count;
}