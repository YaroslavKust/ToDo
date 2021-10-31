using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Serilog;
using ToDo.API.ActionFilters;
using ToDo.API.Authentication;
using ToDo.Entities.DTO;

namespace ToDo.Web.Controllers
{
    [Route("api/authentication")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private IAuthenticationManager _authManager;
        private ILogger _logger;

        public AuthenticationController(IAuthenticationManager authManager, ILogger logger)
        {
            _authManager = authManager;
            _logger = logger;
        }


        [HttpPost("login")]
        [ServiceFilter(typeof(ValidateModelAttribute))]
        public async Task<IActionResult> LoginAsync([FromBody] UserForAuth user)
        {
            if (await _authManager.ValidateUser(user))
            {
                return Ok(new { Token = _authManager.GenerateToken() });
            }

            _logger.Information("Authorization failed");

            return Unauthorized("Authorization failed, try again");
        }


        [HttpPost]
        [ServiceFilter(typeof(ValidateModelAttribute))]
        public async Task<IActionResult> RegisterAsync([FromBody] UserForAuth user)
        {
            if (await _authManager.CreateUser(user))
            {
                return StatusCode(201);
            }

            _logger.Information("Cannot register user");

            return BadRequest("Cannot register user");
        }
    }
}
