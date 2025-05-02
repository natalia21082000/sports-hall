using SportsHall.Models.Entities;

namespace SportsHall.Services.Interfaces
{
    public interface ITokenService
    {
        public string CreateToken(Users user);
    }
}
