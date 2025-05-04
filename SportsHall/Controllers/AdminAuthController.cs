using Microsoft.AspNetCore.Mvc;
using SportsHall.Models.Domains;
using SportsHall.Services.Interfaces;
using System.Security.Claims;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Authentication;
using SportsHall.Common;

namespace SportsHall.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    [Consumes("application/json")]
    public class AdminAuthController : ControllerBase
    {
        private readonly IAppSettings _appSettings;
        //private readonly IAdminService _adminService;
        private readonly IConfiguration _configuration;
        private readonly ITokenService _tokenService;
        private readonly IWebHostEnvironment _environment;
        private readonly ILogger<AdminAuthController> _logger;

        public AdminAuthController(IAppSettings appSettings, IConfiguration configuration, ITokenService tokenService, ILogger<AdminAuthController> logger, IWebHostEnvironment environment)
        {
            _appSettings = appSettings;
            //_adminService = adminService;
            _configuration = configuration;
            _tokenService = tokenService;
            _logger = logger;
            _environment = environment;
        }

        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Login([FromBody] UserLoginDto model)
        {
            if (!ModelState.IsValid)
            {
                return ValidationProblem();
            }
            // Проверяем суперпользователя
            if (model.Login == _appSettings.DbSuperUser && model.Password == _appSettings.DbSuperUserPassword)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, _appSettings.DbSuperUser),
                    new Claim(ClaimTypes.Role, Roles.SuperAdmin),
                    new Claim("SuperUser", "true")
                };

                var token = _tokenService.GenerateToken(claims);

                return Ok(new { token });
            }

            // 2. Проверяем обычного администратора через сервис
            //
            //    var admin = await _adminService.Authenticate(model.Login, model.Password);

            //    if (admin != null)
            //    {
            //        // Создаем список Claims для обычного администратора
            //        var claims = new List<Claim>
            //        {
            //            new Claim(ClaimTypes.NameIdentifier, admin.Id.ToString()),
            //            new Claim(ClaimTypes.Name, admin.Login),
            //            new Claim(ClaimTypes.Role, Roles.Administrator) // Или admin.Role, если роль хранится в объекте admin
            //        };

            //        // Генерируем JWT с помощью TokenService
            //        var tokenString = _tokenService.GenerateToken(claims);
            //        return Ok(new { token = tokenString });
            //    }
            //
            //

            // 3. Если ни суперпользователь, ни администратор не найдены
            _logger.LogWarning("Неудачная попытка входа для администратора: неверный логин или пароль.");

            return Unauthorized(new ProblemDetails
            {
                Title = "Неверные учетные данные",
                Detail = "Логин или пароль указаны неверно",
                Status = StatusCodes.Status401Unauthorized
            });
        }
    }
}