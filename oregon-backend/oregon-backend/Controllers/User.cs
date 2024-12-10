using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using oregon_backend.Models;
using oregon_backend.types;
using oregon_backend.Utils;
using System.Text.Json;

namespace oregon_backend.Controllers;

[Route("api/user")]
[ApiController]
public class User : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public User(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    [RoleAuthorizeAttribute(1)]
    public async Task<IActionResult> GetAll()
    {
        var users = await _context.Users.Select(u => new
        {
            u.Id,
            u.Username,
            u.Email,
            u.ProfileUrl,
            u.CreatedAt,
            u.UpdatedAt
        }).ToListAsync();

        return Ok(users);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
        if (user == null)
        {
            return NotFound();
        }
        return Ok(user);
    }

    [HttpPost("register")]
    [AllowAnonymous]
    public async Task<IActionResult> Register()
    {
        using var reader = new StreamReader(Request.Body);
        var bodyStr = await reader.ReadToEndAsync();
        var registerRequest = JsonSerializer.Deserialize<RegisterRequest>(bodyStr, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });

        if (registerRequest == null)
        {
            return BadRequest("Invalid request");
        }

        if (string.IsNullOrEmpty(registerRequest.Email))
        {
            return BadRequest("Email is required");
        }

        if (string.IsNullOrEmpty(registerRequest.Password))
        {
            return BadRequest("Password is required");
        }

        if (string.IsNullOrEmpty(registerRequest.Username))
        {
            return BadRequest("Username is required");
        }

        if (string.IsNullOrEmpty(registerRequest.Name))
        {
            return BadRequest("Name is required");
        }
        var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.Email == registerRequest.Email);
        if (existingUser != null)
        {
            return BadRequest("User already exists");
        }

        var hashedPassword = BCrypt.Net.BCrypt.HashPassword(registerRequest.Password);
        if (string.IsNullOrEmpty(hashedPassword))
        {
            return BadRequest("Failed to hash password");
        }

        var user = new Models.User()
        {
            Username = registerRequest.Username,
            Password = hashedPassword,
            Email = registerRequest.Email,
            Name = registerRequest.Name,
            ProfileUrl = "",
            Role = 0,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
        };

        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        if (user.Id == 0)
        {
            return BadRequest("Failed to create user");
        }

        var jwtToken = JwtTokenGenerator.GenerateToken(user.Id.ToString(), user.Username, user.Role);

        var registerResponse = new RegisterResponse
        {
            Status = "Success",
            UserId = user.Id,
            Token = jwtToken
        };

        return Ok(registerResponse);
    }

    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<IActionResult> Login()
    {
        using var reader = new StreamReader(Request.Body);
        var bodyStr = await reader.ReadToEndAsync();
        var loginRequest = JsonSerializer.Deserialize<LoginRequest>(bodyStr, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });

        if (loginRequest == null)
        {
            return BadRequest("Invalid request");
        }

        if (string.IsNullOrEmpty(loginRequest.Email))
        {
            return BadRequest("Email is required");
        }

        if (string.IsNullOrEmpty(loginRequest.Password))
        {
            return BadRequest("Password is required");
        }

        var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == loginRequest.Email);
        if (user == null)
        {
            return BadRequest("User not found");
        }

        if (!BCrypt.Net.BCrypt.Verify(loginRequest.Password, user.Password))
        {
            return BadRequest("Invalid password");
        }

        if (user.Id == 0)
        {
            return BadRequest("Failed to login");
        }

        var jwtToken = JwtTokenGenerator.GenerateToken(user.Id.ToString(), user.Username, user.Role);

        var loginResponse = new LoginResponse
        {
            Status = "Success",
            UserId = user.Id,
            Token = jwtToken
        };

        return Ok(loginResponse);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id)
    {
        var userId = Int32.Parse(HttpContext.Items["UserId"].ToString());
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
        if (user == null)
        {
            return NotFound();
        }

        if (user.Id != userId)
        {
            return Unauthorized();
        }

        using var reader = new StreamReader(Request.Body);
        var bodyStr = await reader.ReadToEndAsync();
        var userUpdate = JsonSerializer.Deserialize<UserUpdate>(bodyStr, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });

        if (userUpdate == null)
        {
            return BadRequest("Invalid request");
        }

        if (!string.IsNullOrEmpty(userUpdate.Username))
        {
            user.Username = userUpdate.Username;
        }

        if (!string.IsNullOrEmpty(userUpdate.Name))
        {
            user.Name = userUpdate.Name;
        }

        if (!string.IsNullOrEmpty(userUpdate.Email))
        {
            user.Email = userUpdate.Email;
        }

        if (!string.IsNullOrEmpty(userUpdate.ProfileUrl))
        {
            user.ProfileUrl = userUpdate.ProfileUrl;
        }

        user.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        var userUpdateResponse = new UserUpdateResponse
        {
            Status = "Success"
        };

        return Ok(userUpdateResponse);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var userId = Int32.Parse(HttpContext.Items["UserId"].ToString());
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
        if (user == null)
        {
            return NotFound();
        }

        if (user.Id != userId)
        {
            return Unauthorized();
        }

        _context.Users.Remove(user);
        await _context.SaveChangesAsync();

        var userDeleteResponse = new UserDeleteResponse
        {
            Status = "Success"
        };

        return Ok(userDeleteResponse);
    }
}