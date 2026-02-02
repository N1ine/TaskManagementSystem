using TaskManagement.Domain.Entities;
using TaskManagement.Domain.Common;
using TaskManagement.Domain.Interfaces;
using TaskManagement.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace TaskManagement.Infrastructure.Repositories;

public class TodoRepository : BaseRepository<TodoItem>, ITodoRepository
{
    public TodoRepository(TMDbContext context) : base(context)
    {
    }

    public async Task<List<TodoItem>> GetAllByUserIdAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        return await _context.TodoItems
            .Where(t => t.UserId == userId)
            .OrderByDescending(t => t.CreatedAt)
            .ToListAsync(cancellationToken);
    }
    
    public async Task<TodoItem?> GetByIdAndUserIdAsync(Guid id, Guid userId, CancellationToken cancellationToken = default)
    {
        return await _context.TodoItems
            .FirstOrDefaultAsync(t => t.Id == id && t.UserId == userId, cancellationToken);
    }

    public async Task<bool> DeleteByIdAndUserIdAsync(Guid id, Guid userId, CancellationToken cancellationToken = default)
    {
        var item = await GetByIdAndUserIdAsync(id, userId, cancellationToken);

        if (item == null)
        {
            return false;
        }
        
        _context.TodoItems.Remove(item);
        return true;
    }

    public Task DeleteAsync(TodoItem item, CancellationToken cancellationToken = default)
    {
        _context.TodoItems.Remove(item);
        return Task.CompletedTask;
    }

    public async Task<PaginatedList<TodoItem>> GetPagedAsync(Guid userId, int page, int pageSize, CancellationToken cancellationToken = default)
    {
        var query = _context.TodoItems
            .Where(t => t.UserId == userId)
            .OrderByDescending(t => t.CreatedAt);

        var count = await query.CountAsync(cancellationToken);

        var items = await query
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);

        return new PaginatedList<TodoItem>(items, count, page, pageSize);
    }
}
