namespace oregon_backend.types;

public class UserUpdate
{
    public string Username { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }
    public string Name { get; set; }
    public string ProfileUrl { get; set; }
}

public class UserUpdateResponse
{
    public string Status { get; set; }
}

public class UserDeleteResponse
{
    public string Status { get; set; }
}