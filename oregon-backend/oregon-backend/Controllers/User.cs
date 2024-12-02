using Microsoft.AspNetCore.Mvc;

namespace oregon_backend.Controllers;

[Route("api/user")]
[ApiController]
public class User: ControllerBase
{
    [HttpGet]
    public string GetAll()
    {
        return "Hello, World!";
    }   
    
    [HttpGet("{id}")]
    public string Get(int id)
    {
        return "Hello, World!";
    }
    
    [HttpPost("register")]
    public string Register()
    {
        return "Hello, World!";
    }
    
    [HttpPost("login")]
    public string Login()
    {
        return "Hello, World!";
    }
    
    [HttpPut("{id}")]
    public string Put(int id)
    {
        return "Hello, World!";
    }
}