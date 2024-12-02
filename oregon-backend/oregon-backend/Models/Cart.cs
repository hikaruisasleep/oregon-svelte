namespace oregon_backend.Models;

public class Cart
{
    public int? id { get; set; }
    public int userId { get; set; }
    public int productId { get; set; }
    public int quantity { get; set; }
    public DateTime createdAt { get; set; }
    public DateTime updatedAt { get; set; }
    public DateTime? deletedAt { get; set; }
}
