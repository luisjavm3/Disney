using Disney.DTOs.User;

namespace Disney.Services
{
    public interface IAuthService
    {
        Task Register(UserAuthDto authDto);
    }
}