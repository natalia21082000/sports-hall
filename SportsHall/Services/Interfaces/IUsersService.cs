using SportsHall.Models.Entities;

namespace SportsHall.Services.Interfaces
{
    public interface IUsersService
    {
        Task AddUserAsync(Users user);
        Task<Users> GetUserByLoginAsync(string login);

        Task<bool> IsEmailAddressUniqueAsync(string emailAddress);
    }
}
