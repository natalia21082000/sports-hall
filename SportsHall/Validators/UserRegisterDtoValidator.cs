using FluentValidation;
using SportsHall.Models.Domains;

namespace SportsHall.Validators
{
    public class UserRegisterDtoValidator : AbstractValidator<UserRegisterDto>
    {
        public UserRegisterDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Имя пользователя обязательно для заполнения.")
                .MaximumLength(100).WithMessage("Имя пользователя не должно превышать 100 символов.");

            RuleFor(x => x.DateOfBirth)
                .NotEmpty().WithMessage("Дата рождения обязательна для заполнения.");

            RuleFor(x => x.ContactPhoneNumber)
                .NotEmpty().WithMessage("Контактный телефон обязателен для заполнения.")
                .Matches(@"^\+?[1-9]\d{1,14}$").WithMessage("Некорректный формат телефонного номера.");
            RuleFor(x => x.EmailAddress)
                .NotEmpty().WithMessage("Email обязателен для заполнения.")
                .EmailAddress().WithMessage("Некорректный формат email.");

            RuleFor(x => x.Gender)
                .NotEmpty().WithMessage("Пол обязателен для заполнения.")
                .Must(g => g == 'M' || g == 'F').WithMessage("Пол должен быть указан как 'M' (мужской) или 'F' (женский).");

            RuleFor(x => x.Login)
                .NotEmpty().WithMessage("Логин обязателен для заполнения.")
                .MinimumLength(3).MaximumLength(50).WithMessage("Логин должен быть от 3 до 50 символов.");

            RuleFor(x => x.Password)
               .NotEmpty().WithMessage("Пароль обязателен для заполнения.")
               .MinimumLength(6).MaximumLength(100).WithMessage("Пароль должен быть от 6 до 100 символов.");
        }

    }
}
