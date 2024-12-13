using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using oregon_backend.Models;
using oregon_backend.types;
using System.Text.Json;

namespace oregon_backend.Controllers;

[Route("api/product")]
[ApiController]
public class Product : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public Product(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> GetAll([FromQuery] string category, [FromQuery] string name)
    {
        var products = await _context.Products
            .Where(p => string.IsNullOrEmpty(category) || p.Category == category)
            .Where(p => string.IsNullOrEmpty(name) || p.Name.Contains(name))
            .ToListAsync();
        return Ok(products);
    }

    [HttpGet("{id}")]
    [AllowAnonymous]
    public async Task<IActionResult> Get(int id)
    {
        var product = await _context.Products
            .Include(p => p.Comments)
            .Include(p => p.User)
            .FirstOrDefaultAsync(p => p.Id == id);

        if (product == null)
        {
            return NotFound();
        }

        product.PageView += 1;
        await _context.SaveChangesAsync();

        var averageRating = await _context.Ratings
            .Where(r => r.ProductId == id)
            .AverageAsync(r => (double?)r.Value) ?? 0;

        var response = new
        {
            Product = new
            {
                product.Id,
                product.Name,
                product.Description,
                product.ImageUrl,
                product.Price,
                product.Category,
                product.UserId,
                product.PageView,
                product.CreatedAt,
                product.UpdatedAt,
                Comments = product.Comments.Select(c => new
                {
                    c.Id,
                    c.UserId,
                    c.ProductId,
                    c.Content,
                    c.CreatedAt,
                    c.UpdatedAt
                }),
                User = new
                {
                    product.User.Id,
                    product.User.Username,
                    product.User.Role,
                    product.User.ProfileUrl,
                }
            },
            AverageRating = averageRating
        };

        return Ok(response);
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

        if (string.IsNullOrEmpty(product.Category))
        {
            return BadRequest("Product category is required");
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
            Category = product.Category,
            UserId = userId,
            ImageUrl = product.ImageUrl,
            PageView = 0,
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

        if (string.IsNullOrEmpty(product.Category))
        {
            return BadRequest("Product category is required");
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
        existingProduct.Category = product.Category;
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