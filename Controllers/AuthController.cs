using Disney.DTOs.User;
using Disney.Services;
using Microsoft.AspNetCore.Mvc;

namespace Disney.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("Register")]
        public async Task<ActionResult> Register(UserAuthDto authDto)
        {
            await _authService.Register(authDto);
            return Ok();
        }

        [HttpPost("Login")]
        public async Task<ActionResult> Login(UserAuthDto authDto)
        {
            throw new NotImplementedException();
        }
    }
}