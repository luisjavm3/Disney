using Disney.DTOs.User;
using Microsoft.AspNetCore.Mvc;

namespace Disney.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {

        [HttpPost("Register")]
        public async Task<ActionResult> Register(UserAuthDto authDto)
        {
            throw new NotImplementedException();
        }

        [HttpPost("Login")]
        public async Task<ActionResult> Login(UserAuthDto authDto)
        {
            throw new NotImplementedException();
        }
    }
}