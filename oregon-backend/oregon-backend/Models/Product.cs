﻿namespace oregon_backend.Models;

public class Product
{
    public int? id { get; set; }
    public string name { get; set; }
    public string description { get; set; }
    public string imageUrl { get; set; }
    public double price { get; set; }
    public DateTime createdAt { get; set; }
    public DateTime updatedAt { get; set; }
    public DateTime? deletedAt { get; set; }
}
