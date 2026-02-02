using TaskManagement.Domain.Enums;

namespace TaskManagement.Domain.Entities;

public class TodoItem
{
    public Guid Id { get; private set; }   
    public string Title { get; private set; }
    public string? Description { get; private set; }
    public bool IsCompleted { get; private set; }
    public Priority Priority { get; private set; }
    public DateTime? DueDate { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public Guid UserId { get; private set; }    
    public User? User { get; private set; }

    #pragma warning disable CS8618
    protected TodoItem() { }
    #pragma warning disable CS8618

    public TodoItem(string title, string? description, bool isCompleted, Priority priority, DateTime? dueDate, Guid userId)
    {
        if (string.IsNullOrWhiteSpace(title)) throw new ArgumentException("Title is necessary.");

        Id = Guid.NewGuid();
        Title = title;
        Description = description;
        Priority = priority;
        DueDate = dueDate;
        UserId = userId;
        IsCompleted = false;
        CreatedAt = DateTime.UtcNow;
    }

    public void MarkAsCompleted()
    {
        if (IsCompleted) return;
        IsCompleted = true;
    }

    public void Update(string title, string? description, bool isCompleted, Priority priority, DateTime? dueDate)
    {
        if (string.IsNullOrWhiteSpace(title)) throw new ArgumentException("Title is necessary.");

        Title = title;
        Description = description;
        IsCompleted = isCompleted;
        Priority = priority;
        DueDate = dueDate;
    }
}
