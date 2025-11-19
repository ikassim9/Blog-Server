using Blog_Backend.Models;
using Blog_Backend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Security.Claims;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Blog_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class UserController : ControllerBase
    {



        private readonly IUserService _userService;
        private readonly ILogger<PostController> _logger;



        public UserController(IUserService userService)
        {

            _userService = userService;
        }

        [Authorize]
        [HttpPost("login")]
        public ActionResult Login()
        {
            try
            {
                _userService.LoginUser();

                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }

        }

        [Authorize]
        [HttpPost("register")]
        public ActionResult Register()
        {
  

            try
            {
                _userService.RegiserUser();

                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
       

        }
    }
}



