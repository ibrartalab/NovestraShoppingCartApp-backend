using System;
using NShoppingCart.Core.Interfaces;
using NShoppingCart.Core.Entities;
using Microsoft.EntityFrameworkCore;
using NShoppingCart.Infrastructure.Data;

namespace NShoppingCart.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly NShoppingCartDbContext _dbContext;

    public UserRepository(NShoppingCartDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<IEnumerable<User>> GetAllUsersAsync()
    {
        var allUsers = await _dbContext.Users.ToListAsync();

        return allUsers;
    }
    public async Task<User> GetUserByIdAsync(int id)
    {
        var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == id);
        return user;
    }
    public async Task AddUserAsync(User user)
    {
        await _dbContext.Users.AddAsync(user);
        await _dbContext.SaveChangesAsync();
    }
    public async Task UpdateUserAsync(User user)
    {
        var userExist = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == user.Id);

        if (userExist is not null)
        {
            userExist.FirstName = user.FirstName;
            userExist.LastName = user.LastName;
            userExist.PhoneNumber = user.PhoneNumber;

            await _dbContext.SaveChangesAsync();
        }
    }
    public async Task DeleteUserAsync(int id)
    {
        var userExist = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == id);

        if (userExist is not null)
        {
            _dbContext.Remove(userExist);

            await _dbContext.SaveChangesAsync();
        }
    }
}
