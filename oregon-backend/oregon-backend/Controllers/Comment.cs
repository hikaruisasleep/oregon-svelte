﻿using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using oregon_backend.Models;
using oregon_backend.types;

namespace oregon_backend.Controllers;

[Route("api/comment")]
[ApiController]
public class Comment: ControllerBase
{
    private readonly ApplicationDbContext _context;

    public Comment(ApplicationDbContext context)
    {
        _context = context;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var comments = await _context.Comments.ToListAsync();
        return Ok(comments);
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var comment = await _context.Comments.FirstOrDefaultAsync(x => x.Id == id);
        return Ok(comment);
    }
    
    [HttpPost]
    public async Task<IActionResult> Post()
    {
        var bodyStr = await new StreamReader(Request.Body).ReadToEndAsync();
        var body = JsonSerializer.Deserialize<CommentAddRequest>(bodyStr);
        
        if (body == null)
        {
            return BadRequest();
        }
        
        if (body.UserId == 0 || body.ProductId == 0 || body.Content == null)
        {
            return BadRequest();
        }
        
        var comment = new Models.Comment()
        {
            UserId = body.UserId,
            ProductId = body.ProductId,
            Content = body.Content,
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now
        };
        
        await _context.Comments.AddAsync(comment);
        await _context.SaveChangesAsync();

        var commentAddResponse = new CommentAddResponse()
        {
            Message = "Success"
        };
        
        return Ok(commentAddResponse);
    }
    
    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id)
    {
        var bodyStr = await new StreamReader(Request.Body).ReadToEndAsync();
        var body = JsonSerializer.Deserialize<CommentUpdateRequest>(bodyStr);
        
        if (body == null)
        {
            return BadRequest();
        }
        
        var comment = await _context.Comments.FirstOrDefaultAsync(x => x.Id == id);
        
        if (comment == null)
        {
            return NotFound();
        }
        
        if (body.Content != null)
        {
            comment.Content = body.Content;
        }
        
        comment.UpdatedAt = DateTime.Now;
        
        await _context.SaveChangesAsync();
        
        var commentUpdateResponse = new CommentUpdateResponse()
        {
            Message = "Success"
        };
        
        return Ok(commentUpdateResponse);
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var comment = await _context.Comments.FirstOrDefaultAsync(x => x.Id == id);
        
        if (comment == null)
        {
            return NotFound();
        }
        
        _context.Comments.Remove(comment);
        await _context.SaveChangesAsync();
        
        var commentDeleteResponse = new CommentDeleteResponse()
        {
            Message = "Success"
        };
        
        return Ok(commentDeleteResponse);
    }
}