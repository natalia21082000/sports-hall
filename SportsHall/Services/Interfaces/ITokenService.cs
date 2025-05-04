using System.Security.Claims;
using SportsHall.Models.Entities;

namespace SportsHall.Services.Interfaces
{
    public interface ITokenService
    {
        public string GenerateToken(IEnumerable<Claim> claims);
    }
}
