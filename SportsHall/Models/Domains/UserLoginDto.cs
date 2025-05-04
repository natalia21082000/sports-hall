using System.ComponentModel.DataAnnotations;

namespace SportsHall.Models.Domains
{
    public class UserLoginDto
    {
        public string Login { get; set; } = null!;

        public string Password { get; set; } = null!;
    }
}
