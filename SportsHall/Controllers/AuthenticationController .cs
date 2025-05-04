using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SportsHall.Common;
using SportsHall.Models.Domains;
using SportsHall.Models.Entities;
using SportsHall.Services;
using SportsHall.Services.Interfaces;



namespace SportsHall.Controllers
{
    [ApiController]
    [Route("api/auth")]
    [Produces("application/json")]
    [Consumes("application/json")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public class AuthenticationController : ControllerBase
    {
        private readonly ILogger<AuthenticationController> _logger;
        private readonly ITokenService _tokenService;
        private readonly IUsersService _userService;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _environment;

        public AuthenticationController(
            ILogger<AuthenticationController> logger,
            ITokenService tokenService,
            IUsersService userService,
            IMapper mapper,
            IWebHostEnvironment environment)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _tokenService = tokenService ?? throw new ArgumentNullException(nameof(tokenService));
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _environment = environment ?? throw new ArgumentNullException(nameof(environment));
        }

        [HttpPost("register")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserReadDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> RegisterUser([FromBody] UserRegisterDto userDto)
        {
            if (!ModelState.IsValid)
            {
                return ValidationProblem();
            }

            if (!await _userService.IsEmailAddressUniqueAsync(userDto.EmailAddress))
            {
                ModelState.AddModelError("EmailAddress", "Этот адрес электронной почты уже зарегистрирован.");
                return ValidationProblem();
            }

            var user = _mapper.Map<Users>(userDto);

            string salt = PasswordHelper.GenerateSalt();
            string hashedPassword = PasswordHelper.HashPassword(user.Password, salt);
            user.Password = hashedPassword;
            user.Salt = salt;

            await _userService.AddUserAsync(user);

            var userReadDto = _mapper.Map<UserReadDto>(user);
            return Ok(userReadDto);
        }

        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(object))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Login([FromBody] UserLoginDto userLoginDto)
        {
            if (!ModelState.IsValid)
            {
                return ValidationProblem();
            }

            var user = await _userService.GetUserByLoginAsync(userLoginDto.Login, userLoginDto.Password);

            if (user == null)
            {
                _logger.LogWarning("Неудачная попытка входа: неверный логин или пароль.");

                return Unauthorized(new ProblemDetails
                {
                    Title = "Неверные учетные данные",
                    Detail = "Логин или пароль указаны неверно",
                    Status = StatusCodes.Status401Unauthorized
                });
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Login),
                new Claim(ClaimTypes.Role, Roles.User)
            };

            if (!string.IsNullOrEmpty(user.Status))
            {
                claims.Add(new Claim(ClaimTypes.Role, user.Status));
            }

            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Secure = _environment.IsProduction(),
                SameSite = SameSiteMode.Lax,
                Expires = DateTimeOffset.UtcNow.AddMinutes(30)
            };
            var token = _tokenService.GenerateToken(claims);

            Response.Cookies.Append("authToken", token, cookieOptions);
            var userReadDto = _mapper.Map<UserReadDto>(user);
            return Ok(userReadDto);
        }
    }
}