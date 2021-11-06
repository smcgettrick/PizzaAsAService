﻿using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace PizzaAsAService.MenuService.Api.Entities;

public class Product
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public int Id { get; set; }
    public string Name { get; set; }
    public string Category { get; set; }
    public string Description { get; set; }
    public string Image { get; set; }
    public decimal Price { get; set; }
}

