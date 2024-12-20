﻿namespace oregon_backend.Models;

public class Product
{
    public int? Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string ImageUrl { get; set; }
    public double Price { get; set; }
    public string Category { get; set; }
    public int UserId { get; set; }
    public int PageView { get; set; }
    public bool FeaturedItem { get; set; }
    public string? FeaturedImageUrl { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public DateTime? DeletedAt { get; set; }

    public User User { get; set; }
    public List<Rating> Ratings { get; set; }
    public List<Comment> Comments { get; set; }
}
