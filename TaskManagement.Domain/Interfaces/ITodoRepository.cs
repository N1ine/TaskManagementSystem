using TaskManagement.Domain.Entities;
using TaskManagement.Domain.Common;

namespace TaskManagement.Domain.Interfaces;

public interface ITodoRepository : IRepository<TodoItem>
{
    Task<List<TodoItem>> GetAllByUserIdAsync(Guid userId, CancellationToken cancellationToken = default);
    Task<TodoItem?> GetByIdAndUserIdAsync(Guid id, Guid userId, CancellationToken cancellationToken = default);
    Task<bool> DeleteByIdAndUserIdAsync(Guid id, Guid userId, CancellationToken cancellationToken = default);
    Task DeleteAsync(TodoItem item, CancellationToken cancellationToken = default);
    Task<PaginatedList<TodoItem>> GetPagedAsync(Guid userId, int page, int pageSize, CancellationToken cancellationToken = default);
}
