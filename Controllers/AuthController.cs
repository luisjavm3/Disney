using Disney.DTOs.User;
using Disney.Services;
using Disney.Settings;
using Microsoft.AspNetCore.Mvc;

namespace Disney.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IConfiguration _configuration;
        private readonly JwtSettings _jwtSettings;

        public AuthController(IAuthService authService, IConfiguration configuration)
        {
            _authService = authService;
            _configuration = configuration;
            _jwtSettings = _configuration.GetSection(nameof(JwtSettings)).Get<JwtSettings>();
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
            var jwt = await _authService.Login(authDto);
            SetCookie(jwt);
            return Ok();
        }

        private void SetCookie(string jwt)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = DateTime.UtcNow.AddDays(int.Parse(_jwtSettings.DaysLifespan))
            };

            Response.Cookies.Append("jwt", jwt, cookieOptions);
        }
    }
}