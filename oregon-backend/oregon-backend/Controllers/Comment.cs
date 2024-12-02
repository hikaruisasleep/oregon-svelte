using Microsoft.AspNetCore.Mvc;

namespace oregon_backend.Controllers;

[Route("api/comment")]
[ApiController]
public class Comment
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
    
    [HttpPost]
    public string Post()
    {
        return "Hello, World!";
    }
    
    [HttpPut("{id}")]
    public string Put(int id)
    {
        return "Hello, World!";
    }
    
    [HttpDelete("{id}")]
    public string Delete(int id)
    {
        return "Hello, World!";
    }
}