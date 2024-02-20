using Blog_Backend.Model;
using Blog_Backend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Blog_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class AuthController : ControllerBase
    {


        private readonly IAuthService _authService;

        private readonly IUserService _userService;



        public AuthController(IAuthService authService, IUserService userService)
        {
            _authService = authService;
            _userService = userService;
        }

        [Authorize]
        [HttpPost("login")]
        public ActionResult Login()
        {
            return Ok("Login");
        }

        [Authorize]
        [HttpPost("register")]
        public ActionResult Register(User user)
        {

            return Ok(_userService.CreateUser(user));

        }
    }
}



