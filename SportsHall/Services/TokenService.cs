using Microsoft.IdentityModel.Tokens;
using SportsHall.Models.Entities;
using SportsHall.Services.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SportsHall.Services
{
    public class TokenService: ITokenService
    {
        private readonly string _secretKey; // Ваш секретный ключ.  Храните его БЕЗОПАСНО!

        public TokenService(IConfiguration configuration) // Получаем секретный ключ из конфигурации
        {
            _secretKey = configuration["Jwt:Key"]; //  Jwt:Key -  ключ в вашем appsettings.json
        }

        public string CreateToken(Users user)
        {
            var claims = new[]
            {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()), // ID пользователя
            new Claim(ClaimTypes.Name, user.Login), // Логин пользователя
            // Добавьте другие claims, например, роли:  new Claim(ClaimTypes.Role, "Admin")
        };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddMinutes(30), // Время жизни токена (30 минут)
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
