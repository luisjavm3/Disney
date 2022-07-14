using Disney.DTOs.User;

namespace Disney.Services
{
    public interface IAuthService
    {
        Task Register(UserAuthDto authDto);
        Task<string> Login(UserAuthDto authDto);
    }
}