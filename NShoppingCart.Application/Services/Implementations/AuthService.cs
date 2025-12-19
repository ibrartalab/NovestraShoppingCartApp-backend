using System;
using NShoppingCart.Application.DTOs;
using NShoppingCart.Application.Services.Interfaces;
using NShoppingCart.Core.Interfaces;
using NShoppingCart.Infrastructure.ExternalServices.JwtGeneration;
using BCrypt.Net;
using NovestraTodo.Core.Interfaces;

namespace NShoppingCart.Application.Services.Implementations;

public class AuthService:IAuthService
{
    private readonly IUserRepository _userRepository;
    private readonly IJwtService _jwtService;

    public AuthService(IUserRepository userRepository, IJwtService jwtService)
    {
        _userRepository = userRepository;
        _jwtService = jwtService;
    }

    private string HashPassword(string password)
    {
        // Simple hash for demonstration purposes only. Use a secure hashing algorithm in production.
        var hasedPass = BCrypt.Net.BCrypt.HashPassword(password);
        return hasedPass;
    }

    // Verify the password against the stored hash
    private bool VerifyPassword(string password, string passwordHash)
    {
        var hashedPassword = HashPassword(password);
        return BCrypt.Net.BCrypt.Verify(password, passwordHash);
    }

    // Register a new user
    public async Task<AuthResponseDto> RegisterUser(RegisterRequestDto registerRequestDto)
    {
        var existingUser = await _userRepository.GetUserByEmailAsync(registerRequestDto.Email);

        if (existingUser is not null)
        {
            throw new Exception("User with this email already exists.");
        }
        var newUser = new Core.Entities.User
        {
            Email = registerRequestDto.Email,
            FirstName = registerRequestDto.FirstName,
            LastName = registerRequestDto.LastName,
            PasswordHash = HashPassword(registerRequestDto.Password),
            Role = "User",
            IsActive = true
        };

        await _userRepository.AddUserAsync(newUser);
        var token = await _jwtService.GenerateToken(newUser);
        return new AuthResponseDto
        {
            Token = token,
            User = new UserDto
            {
                Id = newUser.Id,
                Email = newUser.Email,
                FirstName = newUser.FirstName,
                LastName = newUser.LastName
            }
        };
    }

    // Login an existing user
    public async Task<AuthResponseDto> LoginUser(LoginRequestDto loginRequest)
    {
        var user = await _userRepository.GetUserByEmailAsync(loginRequest.Email);

        if (user is null || !VerifyPassword(loginRequest.Password, user.PasswordHash))
        {
            throw new Exception("Invalid email or password.");
        }

        var token = await _jwtService.GenerateToken(user);

        return new AuthResponseDto
        {
            Token = token,
            User = new UserDto
            {
                Id = user.Id,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName
            }
        };
    }
}
