using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using oregon_backend.Models;
using oregon_backend.types;

namespace oregon_backend.Controllers;

[Route("api/cart")]
[ApiController]
public class Cart: ControllerBase
{
    private readonly ApplicationDbContext _context;

    public Cart(ApplicationDbContext context)
    {
        _context = context;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var userId = Int32.Parse(HttpContext.Items["UserId"].ToString());
        var carts = await _context.Carts.Where(c => c.UserId == userId).ToListAsync();
        return Ok(carts);
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var userId = Int32.Parse(HttpContext.Items["UserId"].ToString());
        var cart = await _context.Carts.FindAsync(id);
        if (cart == null)
        {
            return NotFound();
        }

        if (cart.UserId != userId)
        {
            return Unauthorized();
        }

        return Ok(cart);
    }
    
    [HttpPost]
    public async Task<IActionResult> Post()
    {
        var userId = Int32.Parse(HttpContext.Items["UserId"].ToString());
        using var reader = new StreamReader(Request.Body);
        var bodyStr = await reader.ReadToEndAsync();
        var cart = JsonSerializer.Deserialize<CartAddRequest>(bodyStr, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });

        if (cart == null)
        {
            return BadRequest();
        }
        
        if (cart.Quantity <= 0)
        {
            return BadRequest();
        }
        
        var cartModel = new Models.Cart()
        {
            UserId = userId,
            ProductId = cart.ProductId,
            Quantity = cart.Quantity,
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now
        };
        
        await _context.Carts.AddAsync(cartModel);
        await _context.SaveChangesAsync();

        var cartAddResponse = new CartAddResponse()
        {
            Message = "Success"
        };
        
        return Ok(cartAddResponse);
    }
    
    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id)
    {
        var userId = Int32.Parse(HttpContext.Items["UserId"].ToString());
        var bodyStr = await new StreamReader(Request.Body).ReadToEndAsync();
        var cart = JsonSerializer.Deserialize<CartUpdateRequest>(bodyStr);
        
        if (cart == null)
        {
            return BadRequest();
        }
        
        var cartModel = await _context.Carts.FindAsync(id);
        if (cartModel == null)
        {
            return NotFound();
        }

        if (cartModel.UserId != userId)
        {
            return Unauthorized();
        }

        if (cart.Quantity <= 0)
        {
            return BadRequest();
        }
        
        cartModel.Quantity = cart.Quantity;
        cartModel.UpdatedAt = DateTime.Now;
        
        await _context.SaveChangesAsync();
        
        var cartUpdateResponse = new CartUpdateResponse()
        {
            Message = "Success"
        };
        
        return Ok(cartUpdateResponse);
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var userId = Int32.Parse(HttpContext.Items["UserId"].ToString());
        var cart = await _context.Carts.FindAsync(id);
        if (cart == null)
        {
            return NotFound();
        }

        if (cart.UserId != userId)
        {
            return Unauthorized();
        }

        _context.Carts.Remove(cart);
        await _context.SaveChangesAsync();
        
        var cartDeleteResponse = new CartDeleteResponse()
        {
            Message = "Success"
        };
        
        return Ok(cartDeleteResponse);
    }
}