using TaskManagement.Domain.Entities;
using TaskManagement.Domain.Interfaces;
using TaskManagement.Application.Interfaces;
using BCrypt.Net;

namespace TaskManagement.Application.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UserService(IUserRepository userRepository, IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Guid?> RegisterUserAsync(User incomingUser)
    {
        var isUnique = await _userRepository.IsEmailUniqueAsync(incomingUser.Email);
        if (!isUnique)
        {
            return null;
        }

        var passwordHash = BCrypt.Net.BCrypt.HashPassword(incomingUser.PasswordHash);

        var newUser = new User(
            incomingUser.FirstName,
            incomingUser.LastName,
            incomingUser.Email,
            passwordHash
        );

        await _userRepository.AddAsync(newUser);
        await _unitOfWork.SaveChangesAsync();

        return newUser.Id;
    }

    public async Task<User?> LoginAsync(string email, string password)
    {
        var user = await _userRepository.GetByEmailAsync(email);

        if (user == null)
        {
            return null;
        }

        bool isVerified = BCrypt.Net.BCrypt.Verify(password, user.PasswordHash);

        if (!isVerified)
        {
            return null;
        }

        return user;
    }
}
