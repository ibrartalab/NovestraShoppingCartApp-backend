using System;
using NShoppingCart.Application.DTOs;

namespace NShoppingCart.Application.Services.Interfaces;

public interface IUserService
{
    Task<UserDto> GetUserByIdAsync(Guid userId);
    Task<UserDto> CreateUserAsync(UserDto userDto);
    Task<UserDto> UpdateUserAsync(Guid userId, UserDto userDto);
    Task<bool> DeleteUserAsync(Guid userId);
}
