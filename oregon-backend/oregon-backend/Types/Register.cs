namespace oregon_backend.types;

public class RegisterRequest
{
    public string Username { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }
    public string Name { get; set; }
}

public class RegisterResponse
{
    public string Status { get; set; }
    public int? UserId { get; set; }
    public string Token { get; set; }
}
