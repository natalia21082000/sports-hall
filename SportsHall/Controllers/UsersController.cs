using Microsoft.AspNetCore.Mvc;
using SportsHall.Services.Interfaces;

namespace SportsHall.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UsersController : ControllerBase
    {
        private readonly ADatabaseConnection _connection;

        public UsersController(ADatabaseConnection connection)
        {
            _connection = connection;
        }

        [HttpGet]
        public IActionResult GetUsers()
        {
            var users = _connection.Users.ToList();
            return Ok(users);
        }
    }
}