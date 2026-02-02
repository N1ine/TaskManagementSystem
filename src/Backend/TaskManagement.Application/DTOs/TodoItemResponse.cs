using TaskManagement.Domain.Enums;

namespace TaskManagement.Application.DTOs;

public record TodoItemResponse(
    Guid Id,
    string Title,
    string? Description,
    bool IsCompleted,
    Priority Priority,
    DateTime? DueDate,
    DateTime CreatedAt
);
