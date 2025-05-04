using FluentValidation;
using SportsHall.Models.Domains;

namespace SportsHall.Validators
{
    public class UserLoginDtoValidator : AbstractValidator<UserLoginDto>
    {
        public UserLoginDtoValidator()
        {
            RuleFor(x => x.Login)
                .NotEmpty().WithMessage("Логин обязателен для заполнения.")
                .MinimumLength(3).MaximumLength(50).WithMessage("Логин должен быть от 3 до 50 символов.");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Пароль обязателен для заполнения.")
                .MinimumLength(6).MaximumLength(100).WithMessage("Пароль должен быть от 6 до 100 символов.");
        }
    }
}
