using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;
using System.Numerics;
using System;

namespace SportsHall.Models.Entities
{
    public class Users
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Имя пользователя обязательно для заполнения.")]
        [StringLength(100, ErrorMessage = "Имя пользователя не должно превышать 100 символов.")]
        public string Name { get; set; } = null!;

        [Required(ErrorMessage = "Дата рождения обязательна для заполнения.")]
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }

        [Required(ErrorMessage = "Контактный телефон обязателен для заполнения.")]
        [Phone(ErrorMessage = "Некорректный формат телефонного номера.")]
        public string ContactPhoneNumber { get; set; } = null!;

        [Required(ErrorMessage = "Email обязателен для заполнения.")]
        [EmailAddress(ErrorMessage = "Некорректный формат email.")]
        public string EmailAddress { get; set; } = null!;

        [Required(ErrorMessage = "Пол обязателен для заполнения.")]
        [RegularExpression("^[MF]$", ErrorMessage = "Пол должен быть указан как 'M' (мужской) или 'F' (женский).")]
        public char Gender { get; set; }

        [Required(ErrorMessage = "Статус обязателен для заполнения.")]
        public string Status { get; set; } = null!;

        [Required(ErrorMessage = "Логин обязателен для заполнения.")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Логин должен быть от 3 до 50 символов.")]
        public string Login { get; set; } = null!;

        [Required(ErrorMessage = "Пароль обязателен для заполнения.")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Пароль должен быть от 6 до 100 символов.")]
        [DataType(DataType.Password)] // Указывает, что это поле пароля
        public string Password { get; set; } = null!;

        public string Salt { get; set; } = null!;
    }
}