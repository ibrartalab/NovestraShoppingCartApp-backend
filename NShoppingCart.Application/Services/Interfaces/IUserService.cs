
using NShoppingCart.Application.DTOs;
using NShoppingCart.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NShoppingCart.Application.Services.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<UserDto>> GetUsers();
        Task<UserDto?> GetUserById(int id);
        // Task<UserDto?> GetUserByUsername(string username);
        Task<UserDto> AddNewUser(User entity);
        Task<UserDto> UpdateUser(int userId, User entity);
        Task<bool> DeleteUser(int userId);
    }
}