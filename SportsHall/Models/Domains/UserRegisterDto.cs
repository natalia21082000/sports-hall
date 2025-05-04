using System.ComponentModel.DataAnnotations;

namespace SportsHall.Models.Domains
{
    public class UserRegisterDto
    {
        public string Name { get; set; } = null!;

        public DateTime DateOfBirth { get; set; }

        public string ContactPhoneNumber { get; set; } = null!;

        public string EmailAddress { get; set; } = null!;

        public char Gender { get; set; }

        public string Login { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
}
