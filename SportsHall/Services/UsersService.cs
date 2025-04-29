using Microsoft.EntityFrameworkCore;
using SportsHall.Models.Entities;
using SportsHall.Services.Interfaces;

namespace SportsHall.Services
{
    public class UsersService : IUsersService
    {
        private readonly ADatabaseConnection _connection;

        public UsersService(ADatabaseConnection connection)
        {
            _connection = connection;
        }

        public async Task AddUserAsync(Users user)
        {
            await _connection.Users.AddAsync(user);
            await _connection.SaveChangesAsync();
        }

        public async Task<bool> IsEmailAddressUniqueAsync(string emailAddress)
        {
            return !await _connection.Users.AnyAsync(u => u.EmailAddress == emailAddress);
        }

        public async Task<Users> GetUserByLoginAsync(string login)
        {
            var user = await _connection.Users.FirstOrDefaultAsync(u => u.Login == login);
            if (user == null)
            {
                throw new InvalidOperationException("Пользователь с указанным логином не найден.");
            }
            return user;
        }
    }
}
