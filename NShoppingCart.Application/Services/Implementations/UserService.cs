using System;
using NShoppingCart.Application.DTOs;
using NShoppingCart.Application.Services.Interfaces;
using NShoppingCart.Core.Interfaces;

namespace NShoppingCart.Application.Services.Implementations;

public class UserService(IUserRepository userRepository) : IUserService
{
    public async Task<UserDto> GetUserByIdAsync(Guid userId)
    {
        var user = await userRepository.GetUserByIdAsync(userId);
        if (user == null) return null;

        return new UserDto
        {
            Id = user.Id,
            Email = user.Email,
            FirstName = user.FirstName,
            LastName = user.LastName,
            CreatedAt = user.CreatedAt
        };
    }
    public async Task<UserDto> CreateUserAsync(UserDto userDto)
    {
        var newUser = new Core.Entities.User
        {
            Email = userDto.Email,
            FirstName = userDto.FirstName,
            LastName = userDto.LastName,
            CreatedAt = DateTime.UtcNow
        };

        await userRepository.AddUserAsync(newUser);

        userDto.Id = newUser.Id;
        userDto.CreatedAt = newUser.CreatedAt;

        return userDto;
    }
    public async Task<UserDto> UpdateUserAsync(Guid userId, UserDto userDto){
        var existingUser = await userRepository.GetUserByIdAsync(userId);
        if (existingUser == null) return null;

        existingUser.FirstName = userDto.FirstName;
        existingUser.LastName = userDto.LastName;

        await userRepository.UpdateUserAsync(existingUser);

        userDto.Id = existingUser.Id;
        userDto.Email = existingUser.Email;
        userDto.CreatedAt = existingUser.CreatedAt;

        return userDto;
    }
    public async Task<bool> DeleteUserAsync(Guid userId)
    {
        var existingUser = await userRepository.GetUserByIdAsync(userId);
        if (existingUser == null) return false;

        await userRepository.DeleteUserAsync(userId);
        return true;
    }
}
