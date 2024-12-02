namespace oregon_backend.Models;

public class Comment
{
    public int? id { get; set; }
    public int userId { get; set; }
    public int productId { get; set; }
    public string content { get; set; }
    public DateTime createdAt { get; set; }
    public DateTime updatedAt { get; set; }
    public DateTime? deletedAt { get; set; }
}
