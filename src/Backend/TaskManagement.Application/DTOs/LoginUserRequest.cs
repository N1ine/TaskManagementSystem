namespace TaskManagement.Application.DTOs;

public record LoginUserRequest(
    string Email,
    string Password
);
