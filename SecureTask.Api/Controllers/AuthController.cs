using Microsoft.AspNetCore.Mvc;
using SecureTask.Api.DTOs;
using SecureTask.Api.Services;
using SecureTask.Domain.Entities;
using SecureTask.Domain.Interfaces;

namespace SecureTask.Api.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly IUserRepository _userRepository;
    private readonly AuthService _authService;

    public AuthController(
        IUserRepository userRepository,
        AuthService authService)
    {
        _userRepository = userRepository;
        _authService = authService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterRequest request)
    {
        var existingUser = await _userRepository.GetByEmailAsync(request.Email);
        if (existingUser != null)
            return BadRequest("User already exists");

        var user = new User
        {
            Email = request.Email,
            PasswordHash = _authService.HashPassword(request.Password),
            Role = "User"
        };

        await _userRepository.CreateAsync(user);
        return Ok();
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        var user = await _userRepository.GetByEmailAsync(request.Email);
        if (user == null ||
            !_authService.VerifyPassword(request.Password, user.PasswordHash))
            return Unauthorized();

        var token = _authService.GenerateToken(user);
        return Ok(new { token });
    }
}