<<<<<<< HEAD
using NShoppingCart.Core.Entities;
=======
using System;
using NShoppingCart.Application.DTOs;
>>>>>>> 947f1ba63b774114116ecd2dee7c55a89a3b20bb

namespace NShoppingCart.Core.Interfaces.Services;

public interface IUserService
{
<<<<<<< HEAD
    Task<User> GetUserProfileAsync(int id);
    Task UpdateProfileAsync(User user);
    Task DeactivateUserAsync(int id);
    Task<IEnumerable<User>> GetUsersByRoleAsync(string role);
}
=======
    Task<UserDto> GetUserByIdAsync(Guid userId);
    Task<UserDto> CreateUserAsync(UserDto userDto);
    Task<UserDto> UpdateUserAsync(Guid userId, UserDto userDto);
    Task<bool> DeleteUserAsync(Guid userId);
}
>>>>>>> 947f1ba63b774114116ecd2dee7c55a89a3b20bb
