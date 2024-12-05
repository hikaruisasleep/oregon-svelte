using System.Text.Json;
using Microsoft.AspNetCore.Authorization;
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
    [AllowAnonymous]
    public async Task<IActionResult> GetAll()
    {
        var products = await _context.Products.ToListAsync();
        return Ok(products);
    }
    
    [HttpGet("{id}")]
    [AllowAnonymous]
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
    [RoleAuthorizeAttribute(1)]
    public async Task<IActionResult> Post()
    {
        var userId = Int32.Parse(HttpContext.Items["UserId"].ToString());
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
            UserId = userId,
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
    [RoleAuthorizeAttribute(1)]
    public async Task<IActionResult> Put(int id)
    {
        var userId = Int32.Parse(HttpContext.Items["UserId"].ToString());
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
    [RoleAuthorizeAttribute(1)]
    public async Task<IActionResult> Delete(int id)
    {
        var userId = Int32.Parse(HttpContext.Items["UserId"].ToString());
        var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == id);
        if (product == null)
        {
            return NotFound();
        }

        if (product.UserId != userId)
        {
            return Unauthorized();
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