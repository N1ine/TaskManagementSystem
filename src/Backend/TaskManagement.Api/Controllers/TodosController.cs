using Microsoft.AspNetCore.Mvc;
using TaskManagement.Application.DTOs;
using TaskManagement.Application.Interfaces;
using TaskManagement.Application.Services;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using TaskManagement.Domain.Entities;
using AutoMapper;

namespace TaskManagement.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class TodosController : ControllerBase
{
    private readonly ITodoService _todoService;
    private readonly IMapper _mapper;

    public TodosController(ITodoService todoService, IMapper mapper)
    {
        _todoService = todoService;
        _mapper = mapper;
    }
    
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateTodoRequest request)
    {
        var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (userIdString == null)
        {
            return Unauthorized("Cannot identify the user.");
        }

        var userId = Guid.Parse(userIdString);

        var todoItem = new TodoItem(
            request.Title,
            request.Description,
            false,
            request.Priority,
            request.DueDate,
            userId
        );

        await _todoService.CreateTodoAsync(todoItem);

        return Ok(new { Id = todoItem.Id });
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] int page = 1, int pageSize = 10)
    {
        var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
        
        if (userIdString == null) 
        {
            return Unauthorized();
        }

        var userId = Guid.Parse(userIdString);

        var pagedResult = await _todoService.GetUserTodosAsync(userId, page, pageSize);

        return Ok(pagedResult);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (userIdString == null)
        {
            return Unauthorized();
        }

        var userId = Guid.Parse(userIdString);

        var todoItem = await _todoService.GetByIdAsync(id, userId);

        if (todoItem == null)
        {
            return NotFound("Task not found.");
        }

        var response = _mapper.Map<TodoItemResponse>(todoItem);

        return Ok(response);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateTodoRequest request)
    {
        var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
        
        if (userIdString == null)
        {
            return Unauthorized();
        }

        var userId = Guid.Parse(userIdString);

        var isUpdated = await _todoService.UpdateAsync(id, userId, request);

        if (!isUpdated)
        {
            return NotFound("Task is not found or access denied.");
        }

        return NoContent();
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
        
        if (userIdString == null)
        {
            return Unauthorized();
        }
        
        var userId = Guid.Parse(userIdString);

        var isDeleted = await _todoService.DeleteAsync(id, userId);

        if (!isDeleted)
        {
            return NotFound("Task is not found or access denied.");
        }

        return NoContent();
    }
}
