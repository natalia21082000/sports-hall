using Microsoft.AspNetCore.Mvc;
using SportsHall.Services.Interfaces;

namespace SportsHall.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TestController : ControllerBase
    {
        private readonly ADatabaseConnection _db;

        public TestController(ADatabaseConnection db)
        {
            _db = db;
        }

        [HttpGet("users")] // Изменили маршрут
        public IActionResult GetUsers()
        {
            try
            {
                // Используем EF Core для запроса к базе данных
                var users = _db.Users.ToList(); // Получаем всех пользователей

                return Ok(users); // Возвращаем список пользователей
            }
            catch (Exception ex)
            {
                // Обрабатываем ошибки
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
