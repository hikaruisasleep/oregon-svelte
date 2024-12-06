namespace oregon_backend.Models;


/*
 * Role
 * 0 = User
 * 1 = Admin
 */

public class User
{
    public int? Id { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }
    public int Role { get; set; }
    public string? ProfileUrl { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public DateTime? DeletedAt { get; set; }

    public List<Rating> Ratings { get; set; }
    public List<Comment> Comments { get; set; }
    public List<Cart> Carts { get; set; }
    public List<Product> Products { get; set; }
}
