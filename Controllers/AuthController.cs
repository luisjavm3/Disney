using Disney.DTOs.User;
using Disney.Services;
using Disney.Settings;
using Microsoft.AspNetCore.Mvc;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace Disney.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IConfiguration _configuration;
        private readonly JwtSettings _jwtSettings;
        private readonly SendGridSettings _sendGridSettings;

        public AuthController(IAuthService authService, IConfiguration configuration)
        {
            _authService = authService;
            _configuration = configuration;
            _jwtSettings = _configuration.GetSection(nameof(JwtSettings)).Get<JwtSettings>();
            _sendGridSettings = _configuration.GetSection(nameof(SendGridSettings)).Get<SendGridSettings>();
        }

        [HttpPost("Register")]
        public async Task<ActionResult> Register(UserAuthDto authDto)
        {
            await _authService.Register(authDto);
            await SendWelcomeEmail(authDto.Email);
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

        private async Task SendWelcomeEmail(string email)
        {
            var apiKey = _sendGridSettings.NAME_OF_THE_ENVIRONMENT_VARIABLE_FOR_YOUR_SENDGRID_KEY;
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress(_sendGridSettings.From, "Luis J. Montes");
            var subject = $"Welcome {email}, now you are registed in Disney Movies App.";
            var to = new EmailAddress(email, email);
            var plainTextContent = "Enjoy...";
            var htmlContent = "<strong>Enjoy...</strong>";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            var response = await client.SendEmailAsync(msg);
        }
    }
}