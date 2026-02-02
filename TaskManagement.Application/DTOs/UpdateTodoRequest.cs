using TaskManagement.Domain.Enums;

namespace TaskManagement.Application.DTOs;

public record UpdateTodoRequest(
    string Title,
    string? Description,
    bool IsCompleted,
    Priority Priority,
    DateTime? DueDate
);
