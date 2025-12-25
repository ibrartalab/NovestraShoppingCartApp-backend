using Microsoft.EntityFrameworkCore;
using NovestraTodo.Core.Interfaces;
using NShoppingCart.Application.DTOs;
using NShoppingCart.Core.Entities;
using NShoppingCart.Core.Interfaces;
using NShoppingCart.Infrastructure.Data;

namespace NShoppingCart.Infrastructure.Repositories
{
    public class UserRepository(NShoppingCartDbContext dbContext) : IUserRepository
    {
        // Get all the users list from the db
        public async Task<IEnumerable<User>> GetAllUsersAsync() => await dbContext.Users.ToListAsync();
        // Get user by id
        public async Task<User?> GetUserByIdAsync(int id) => await dbContext.Users.FirstOrDefaultAsync(user => user.Id == id);

        public async Task<User> GetUserByEmailAsync(string email) => await dbContext.Users.FirstOrDefaultAsync(user => user.Email == email);

        // // Get user by username
        // public async Task<User?> GetUserByUsernameAsync(string username)
        // {
        //     return await dbContext.Users.FirstOrDefaultAsync(user => user.UserName == username);
        // }

        // Add a new user
        public async Task<User> AddUserAsync(User entity)
        {
            entity.Id = Random.Shared.Next(1, 1000000);
            dbContext.Users.Add(entity);

            await dbContext.SaveChangesAsync();

            return entity;
        }

        // Update a user
        public async Task<User> UpdateUserAsync(int userId, User entity)
        {
            var user = await dbContext.Users.FirstOrDefaultAsync(user => user.Id == userId);

            if (user is not null)
            {
                user.FirstName = entity.FirstName;
                user.LastName = entity.LastName;
                user.Email = entity.Email;

                await dbContext.SaveChangesAsync();

                return user;
            }

            return entity;
        }

        // Delete a user
        public async Task<bool> DeleteUserAsync(int userId)
        {
            var user = await dbContext.Users.FirstOrDefaultAsync(user => user.Id == userId);

            if (user is not null)
            {
                dbContext.Remove(user);

                return await dbContext.SaveChangesAsync() > 0;
            }

            return false;

        }
    }
}