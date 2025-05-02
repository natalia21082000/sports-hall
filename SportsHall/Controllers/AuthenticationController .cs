using System.Dynamic;
using System.Text;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SportsHall.Models;
using SportsHall.Models.Entities;
using SportsHall.Models.Domains;
using SportsHall.Services;
using SportsHall.Services.Interfaces;
using Microsoft.AspNetCore.Http;

namespace SportsHall.Controllers
{
    [ApiController]
    [Route("api")]
    public class AuthenticationController : ControllerBase
    {
        private readonly IUsersService _usersService;
        private readonly IMapper _mapper;
        private readonly ITokenService _token;
        private readonly IWebHostEnvironment _env;
        private string _login = null!;

        public AuthenticationController(IUsersService usersService, IMapper mapper, ITokenService token, IWebHostEnvironment env)
        {
            _usersService = usersService;
            _mapper = mapper;
            _token = token;
            _env = env;
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterValentine([FromBody] UserRegisterDto userDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!await _usersService.IsEmailAddressUniqueAsync(userDto.EmailAddress))
            {
                ModelState.AddModelError("EmailAddress", "Этот адрес электронной почты уже зарегистрирован.");
                return BadRequest(ModelState);
            }

            Users user = _mapper.Map<Users>(userDto);

            string salt = PasswordHelper.GenerateSalt();
            string hashedPassword = PasswordHelper.HashPassword(user.Password, salt);

            user.Password = hashedPassword;
            user.Salt = salt;

            try
            {
                await _usersService.AddUserAsync(user);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Ошибка при сохранении пользователя: {ex}");
                return StatusCode(500, "Произошла ошибка при регистрации пользователя.");
            }

            return Ok(new { Message = "Пользователь успешно зарегистрирован." });
        }



        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginDto userDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var user = await _usersService.GetUserByLoginAsync(userDto.Login, userDto.Password);

                if (user == null)
                {
                    return Unauthorized(new { Message = "Неверный логин или пароль." });
                }

                //  Создаем токен
                var token = _token.CreateToken(user);

                var cookieOptions = new CookieOptions
                {
                    HttpOnly = true,
                    Secure = _env.IsProduction() && !_env.IsDevelopment(), // Secure для продакшена
                    SameSite = SameSiteMode.Lax, // Менее строгий SameSite
                    Expires = DateTimeOffset.UtcNow.AddMinutes(30)
                };

                Response.Cookies.Append("authToken", token, cookieOptions);
                return Ok(new { Username = user.Login, Message = "Вход выполнен успешно." });
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Ошибка при входе: {ex}");
                if (_env.IsDevelopment())
                {
                    return StatusCode(500, $"Ошибка при входе: {ex.Message}\n{ex.StackTrace}"); // Подробности в dev
                }
                else
                {
                    return StatusCode(500, "Произошла ошибка при входе.");
                }
            }
        }
    }
}