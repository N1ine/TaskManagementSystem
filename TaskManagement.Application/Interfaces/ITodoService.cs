using TaskManagement.Domain.Entities;
using TaskManagement.Domain.Enums;
using TaskManagement.Application.DTOs;
using AutoMapper;

namespace TaskManagement.Application.Interfaces;

public interface ITodoService
{
    Task<Guid> CreateTodoAsync(TodoItem todoItem);
    Task<List<TodoItem>> GetAllByUserIdAsync(Guid userId);
    Task<bool> UpdateAsync(Guid id, Guid userId, UpdateTodoRequest request);
    Task<bool> DeleteAsync(Guid id, Guid userId);
    Task<PagedResult<TodoItemResponse>> GetUserTodosAsync(Guid userId, int page, int pageSize);
    Task<TodoItem?> GetByIdAsync(Guid id, Guid userId);
}
