﻿namespace oregon_backend.types;

public class LoginRequest
{
    public string Email { get; set; }
    public string Password { get; set; }
}

public class LoginResponse
{
    public string Status { get; set; }
    public int? UserId { get; set; }
    public string Token { get; set; }
}