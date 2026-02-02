using Microsoft.AspNetCore.Mvc;
using TaskManagement.Application.DTOs;
using TaskManagement.Domain.Entities;
using TaskManagement.Application.Interfaces;

namespace Task.Management.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;

    public UsersController(IUserService userService, IJwtTokenGenerator jwtTokenGenerator)
    {
        _userService = userService;
        _jwtTokenGenerator = jwtTokenGenerator;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterUserRequest request)
    {
        var user = new User(
            request.FirstName,
            request.LastName,
            request.Email,
            request.Password);

        var userId = await _userService.RegisterUserAsync(user);

        if(userId == null)
        {
            return BadRequest("User with this email already exists.");
        }

        return Ok(new { Id = user.Id });
    }
    
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginUserRequest request)
    {
        var user = await _userService.LoginAsync(request.Email, request.Password);

        if (user == null)
        {
            return Unauthorized(new { error = "Incorrect login or password." });
        }

        var token = _jwtTokenGenerator.GeneratorToken(user);

        return Ok(new 
        { token = token, 
          userId = user.Id,
          firstName = user.FirstName
        });
    }
}
