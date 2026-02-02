using TaskManagement.Domain.Entities;

namespace TaskManagement.Application.Interfaces;

public interface IUserService
{
    Task<Guid?> RegisterUserAsync(User user);
    Task<User?> LoginAsync(string email, string password);
}
