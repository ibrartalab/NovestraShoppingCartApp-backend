namespace NShoppingCart.Core.Interfaces.Services;

public interface IUserService
{
    Task<User> GetUserProfileAsync(int id);
    Task UpdateProfileAsync(User user);
    Task DeactivateUserAsync(int id);
    Task<IEnumerable<User>> GetUsersByRoleAsync(string role);
}