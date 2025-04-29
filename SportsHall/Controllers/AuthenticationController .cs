using System.Dynamic;
using System.Text;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SportsHall.Models;
using SportsHall.Models.Entities;
using SportsHall.Models.Domains;
using SportsHall.Services;
using SportsHall.Services.Interfaces;

namespace SportsHall.Controllers
{
    [ApiController]
    [Route("api")]
    public class AuthenticationController : ControllerBase
    {
        private readonly IUsersService _usersService;
        private readonly IMapper _mapper;
        private string _login = null!;

        public AuthenticationController(IUsersService usersService, IMapper mapper)
        {
            _usersService = usersService;
            _mapper = mapper;
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



        //[HttpPost("login")]
        //public async Task<IActionResult> Login([FromBody] UserLoginDto userDto)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    try
        //    {
        //        // 1. Находим пользователя по логину.  Вместо GetUserByLoginAsync, используйте метод,
        //        //    который возвращает объект пользователя, а не просто проверяет его существование.
        //        var user = await _usersService.GetUserByLoginAsync(userDto.Login);

        //        if (user == null)
        //        {
        //            return Unauthorized("Неверный логин или пароль."); // Или NotFound, в зависимости от ваших требований.
        //        }

        //        // 2. Получаем соль из объекта пользователя, полученного из базы данных.
        //        string salt = user.Salt;

        //        // 3. Хешируем введенный пароль с использованием соли пользователя.
        //        string hashedPassword = PasswordHelper.HashPassword(userDto.Password, salt);

        //        // 4. Сравниваем захешированный введенный пароль с захешированным паролем из базы данных.
        //        if (user.Password != hashedPassword)
        //        {
        //            return Unauthorized("Неверный логин или пароль.");
        //        }

        //        // 5. Если пароли совпадают, выполняем дальнейшие действия (например, создание токена аутентификации).
        //        //   Предположим, что у вас есть метод для создания токена.
        //       // var token = _tokenService.CreateToken(user); // Замените на вашу реализацию создания токена

        //        return Ok(new { Token = token, Message = "Вход выполнен успешно." });

        //    }
        //    catch (Exception ex)
        //    {
        //        Console.Error.WriteLine($"Ошибка при входе в кабинет пользователя: {ex}");
        //        return StatusCode(500, "Произошла ошибка при входе.");
        //    }
    }
}