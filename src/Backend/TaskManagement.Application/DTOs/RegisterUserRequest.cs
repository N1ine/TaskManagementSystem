using TaskManagement.Domain.Entities;

namespace TaskManagement.Application.DTOs;

public record RegisterUserRequest (
    string FirstName,
    string LastName,
    string Email,
    string Password
)
{
    public User ToEntity()
    {
        return new User(FirstName, LastName, Email, Password);
    }
}
