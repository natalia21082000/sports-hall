using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using SportsHall.Models.Domains;
using SportsHall.Services.Interfaces;

namespace SportsHall.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AdminAuthController : ControllerBase
    {
        private readonly IAppSettings _appSettings;

        public AdminAuthController(IAppSettings appSettings)
        {
            _appSettings = appSettings;
        }


        [HttpPost("login")]
        public IActionResult Login([FromBody] UserLoginDto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var superuser = _appSettings.DbSuperUser;
            var password = _appSettings.DbSuperUserPassword;

            if (model.Login == superuser && model.Password == password)
            {
                return Ok(new { message = "Вход выполнен успешно" });
            }
            return Unauthorized(new { message = "Неверные учетные данные" });
        }
    }
}
