using Microsoft.AspNetCore.Mvc;
using MindTrack.Api.Models;
using MindTrack.Application.Interfaces;
using System.Threading.Tasks;

namespace MindTrack.Api.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
            => Ok(new { token = await _authService.RegisterAsync(model.Email, model.Name, model.Password) });

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
            => Ok(new { token = await _authService.LoginAsync(model.Email, model.Password) });
    }
}
