using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using oregon_backend.Models;
using oregon_backend.types;

namespace oregon_backend.Controllers;

[Route("api/product")]
[ApiController]
public class Product: ControllerBase
{
    private readonly ApplicationDbContext _context;

    public Product(ApplicationDbContext context)
    {
        _context = context;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var products = await _context.Products.ToListAsync();
        return Ok(products);
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == id);
        if (product == null)
        {
            return NotFound();
        }
        return Ok(product);
    }
    
    [HttpPost]
    public async Task<IActionResult> Post()
    {
        using var reader = new StreamReader(Request.Body);
        var bodyStr = await reader.ReadToEndAsync();
        var product = JsonSerializer.Deserialize<ProductAddRequest>(bodyStr, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });

        if (product == null)
        {
            return BadRequest("Invalid request");
        }
        
        if (string.IsNullOrEmpty(product.Name))
        {
            return BadRequest("Name is required");
        }
        
        if (product.Price == 0)
        {
            return BadRequest("Price is required");
        }
        
        var newProduct = new Models.Product
        {
            Name = product.Name,
            Price = product.Price,
            Description = product.Description,
            ImageUrl = product.ImageUrl,
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now
        };
        
        _context.Products.Add(newProduct);
        await _context.SaveChangesAsync();

        var productAddReponse = new ProductAddResponse()
        {
            Message = "Success",
        };
        
        return Ok(productAddReponse);
    }
    
    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id)
    {
        using var reader = new StreamReader(Request.Body);
        var bodyStr = await reader.ReadToEndAsync();
        var product = JsonSerializer.Deserialize<ProductAddRequest>(bodyStr, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });

        if (product == null)
        {
            return BadRequest("Invalid request");
        }
        
        if (string.IsNullOrEmpty(product.Name))
        {
            return BadRequest("Name is required");
        }
        
        if (product.Price == 0)
        {
            return BadRequest("Price is required");
        }
        
        var existingProduct = await _context.Products.FirstOrDefaultAsync(p => p.Id == id);
        if (existingProduct == null)
        {
            return NotFound();
        }
        
        existingProduct.Name = product.Name;
        existingProduct.Price = product.Price;
        existingProduct.Description = product.Description;
        existingProduct.ImageUrl = product.ImageUrl;
        existingProduct.UpdatedAt = DateTime.Now;
        
        await _context.SaveChangesAsync();
        
        var productEditResponse = new ProductUpdateResponse()
        {
            Message = "Success",
        };
        
        return Ok(productEditResponse);
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == id);
        if (product == null)
        {
            return NotFound();
        }
        
        _context.Products.Remove(product);
        await _context.SaveChangesAsync();
        
        var productDeleteResponse = new ProductDeleteResponse()
        {
            Message = "Success",
        };
        
        return Ok(productDeleteResponse);
    }
}