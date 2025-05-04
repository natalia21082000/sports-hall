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

        [HttpGet("users")]
        public IActionResult GetUsers()
        {
            try
            {
                var users = _db.Users.ToList();

                return Ok(users);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
