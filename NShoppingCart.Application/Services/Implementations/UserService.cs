using NShoppingCart.Core.Interfaces;
using NShoppingCart.Application.DTOs;
using NShoppingCart.Application.Services.Interfaces;
using NShoppingCart.Core.Entities;
using NovestraTodo.Core.Interfaces;



namespace NShoppingCart.Application.Services.Implementation
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<IEnumerable<UserDto>> GetUsers()
        {
            var users = await _userRepository.GetAllUsersAsync();
            var userDtos = users.Select(user => new UserDto
            {
                Id = user.Id,
                FullName = user.FullName,
                UserName = user.UserName,
                Email = user.Email,
                CreatedAt = new DateTime() // Assuming CreatedAt is a DateTime property in User entity
            });
            return userDtos;
        }

        public async Task<UserDto?> GetUserById(int id)
        {
            var user = await _userRepository.GetUserByIdAsync(id);
            var userDto = user == null ? null : new UserDto
            {
                Id = user.Id,
                FullName = user.FullName,
                UserName = user.UserName,
                Email = user.Email,
                CreatedAt = new DateTime() // Assuming CreatedAt is a DateTime property in User entity
            };
            return userDto;
        }
        
        public async Task<UserDto> AddNewUser(User entity)
        {
            var user = await _userRepository.GetUserByIdAsync(entity.Id);
            var newUser = await _userRepository.AddUserAsync(entity);

            if (user != null)
            {
                throw new Exception("Username already exists");
            }

            var userDto = new UserDto
            {
                Id = newUser.Id,
                FullName = newUser.FullName,
                UserName = newUser.UserName,
                Email = newUser.Email,
                CreatedAt = newUser.CreatedAt,
            };
            return userDto;
        }
        public async Task<UserDto> UpdateUser(int userId, User entity)
        {
            var user = await _userRepository.GetUserByIdAsync(userId);
            if (user == null)
            {
                throw new Exception("User not found");
            }
            var updatedUser = await _userRepository.UpdateUserAsync(userId, entity);

            var userDto = new UserDto
            {
                Id = updatedUser.Id,
                FullName = updatedUser.FullName,
                UserName = updatedUser.UserName,
                Email = updatedUser.Email,
                CreatedAt = updatedUser.CreatedAt,
            };
            return userDto;
        }
        public async Task<bool> DeleteUser(int userId)
        {
            return await _userRepository.DeleteUserAsync(userId);
        }
    }
}