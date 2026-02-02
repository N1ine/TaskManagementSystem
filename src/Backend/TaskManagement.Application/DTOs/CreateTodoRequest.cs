using TaskManagement.Domain.Entities;
using TaskManagement.Domain.Enums;

namespace TaskManagement.Application.DTOs;

public record CreateTodoRequest(
    string Title,
    string Description,
    Priority Priority,
    DateTime? DueDate,
    Guid UserId
)
{
    public TodoItem ToEntity()
    {
        return new TodoItem(Title, Description, false, Priority, DueDate, UserId);
    }
}
