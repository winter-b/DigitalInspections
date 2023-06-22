using DigitalInspectionsWebApi.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static DigitalInspectionsWebApi.Models.User;

namespace DigitalInspectionsWebApi.Controllers
{
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;


        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost]
        [Route("login")]
        public IActionResult Login([FromForm] string username, [FromForm] string password)
        {
            Console.WriteLine("Logging in user: " + username);
            Console.WriteLine("Password: " + password);
            string token = _authService.Login(username, password);
            if (token == "")
            {
                return Unauthorized();
            }
            else if (_authService.IsPasswordExpired(username))
            {
                Console.WriteLine(username + " password expired");
                return StatusCode(StatusCodes.Status201Created, token);
            }
            return Ok(token);
            
        }

        [HttpPost]
        [Route("account/update")]
        public IActionResult UpdateCredentials([FromForm] string oldPassword, [FromForm] string newPassword, [FromHeader] string token)
        {
            string username = _authService.GetUsernameFromToken(token);
            if (username == "")
            {
                return Unauthorized();
            }
            bool success = _authService.UpdateCredentials(username, oldPassword, newPassword);
            if (success)
            {
                return Ok();
            }
            return BadRequest();

        }

        [HttpPost]
        [Route("account/register")]
        public IActionResult Register([FromForm] string username, [FromForm] string password,[FromForm] Role role, [FromForm] string key)
        {
            if (!_authService.RegistrationCodeValid(key))
            {
                return Unauthorized();
            }
            bool success = _authService.Register(username, password, role);
            if (success)
            {
                return Ok();
            }
            return BadRequest();
        }
    }
}
