﻿namespace oregon_backend.Models;

public class Comment
{
    public int? Id { get; set; }
    public int UserId { get; set; }
    public int ProductId { get; set; }
    public string Content { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public DateTime? DeletedAt { get; set; }

    public Product Product { get; set; }
    public User User { get; set; }
}
