using Disney.DTOs.Character;

namespace Disney.Services
{
    public interface ICharacterService
    {
        Task<CharacterResponseDto> AddOne(CharacterCreateDto charaterCreate, IFormFile imageFile);
    }
}