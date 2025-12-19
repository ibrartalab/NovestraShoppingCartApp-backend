using NShoppingCart.Core.Entities;


namespace NovestraTodo.Core.Interfaces
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task<User?> GetUserByIdAsync(int id);
        Task<User> GetUserByEmailAsync(string email);

        // Task<User?> GetUserByUsernameAsync(string username);
        Task<User> AddUserAsync(User entity);
        Task<User> UpdateUserAsync(int userId, User entity);
        Task<bool> DeleteUserAsync(int userId);
    }
}