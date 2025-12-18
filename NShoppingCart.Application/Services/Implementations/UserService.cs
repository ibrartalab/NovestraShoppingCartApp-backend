using NShoppingCart.Application.Services.Interfaces;
using NShoppingCart.Core.Entities;
using NShoppingCart.Core.Interfaces;
using NShoppingCart.Core.Interfaces.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<User> GetUserProfileAsync(int id)
    {
        var user = await _userRepository.GetUserByIdAsync(id);
        if (user == null) throw new KeyNotFoundException("User not found.");
        return user;
    }

    public async Task UpdateProfileAsync(User user)
    {
        var existingUser = await _userRepository.GetUserByIdAsync(user.Id);
        if (existingUser == null) throw new Exception("User record does not exist.");

        // Update specific fields only to prevent overwriting sensitive data like PasswordHash
        existingUser.FirstName = user.FirstName;
        existingUser.LastName = user.LastName;
        existingUser.PhoneNumber = user.PhoneNumber;
        existingUser.UpdatedAt = DateTime.UtcNow;

        await _userRepository.UpdateUserAsync(existingUser);
    }

    public async Task DeactivateUserAsync(int id)
    {
        var user = await _userRepository.GetUserByIdAsync(id);
        if (user != null)
        {
            user.IsActive = false;
            await _userRepository.UpdateUserAsync(user);
        }
    }

    public async Task<IEnumerable<User>> GetUsersByRoleAsync(string role)
    {
        var allUsers = await _userRepository.GetAllUsersAsync();
        return allUsers.Where(u => u.Role == role);
    }
}