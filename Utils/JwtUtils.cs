using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Disney.Settings;
using Microsoft.IdentityModel.Tokens;

namespace Disney.Utils
{
    public class JwtUtils : IJwtUtils
    {
        private readonly IConfiguration _configuration;
        private readonly JwtSettings _jwtSettings;

        public JwtUtils(IConfiguration configuration)
        {
            _configuration = configuration;
            _jwtSettings = _configuration.GetSection(nameof(JwtSettings)).Get<JwtSettings>();
        }

        public string GetJwt(int userId)
        {
            var key = System.Text.Encoding.UTF8.GetBytes(_jwtSettings.Secret);
            var signingKey = new SymmetricSecurityKey(key);
            var signingCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha512Signature);
            var claims = new List<Claim> { new Claim("id", userId.ToString()) };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddDays(int.Parse(_jwtSettings.DaysLifespan)),
                SigningCredentials = signingCredentials,
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}