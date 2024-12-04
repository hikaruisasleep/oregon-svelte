namespace oregon_backend.Models;

public class Rating
{
    public int? Id { get; set; }
    public int UserId { get; set; }
    public int ProductId { get; set; }
    public int Value { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public DateTime? DeletedAt { get; set; }

    public Product Product { get; set; }
    public User User { get; set; }
}
