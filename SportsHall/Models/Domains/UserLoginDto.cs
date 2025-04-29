using System.ComponentModel.DataAnnotations;

namespace SportsHall.Models.Domains
{
    public class UserLoginDto
    {
        [Required(ErrorMessage = "Логин обязателен для заполнения.")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Логин должен быть от 3 до 50 символов.")]
        public string Login { get; set; } = null!;

        [Required(ErrorMessage = "Пароль обязателен для заполнения.")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Пароль должен быть от 6 до 100 символов.")]
        [DataType(DataType.Password)]

        public string Password { get; set; } = null!;
    }
}
