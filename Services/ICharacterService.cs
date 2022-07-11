using Disney.DTOs.Character;

namespace Disney.Services
{
    public interface ICharacterService
    {
        Task<CharacterResponseDto> AddCharacter(CharacterCreateDto charaterCreate, IFormFile imageFile);
        Task<IList<CharacterListItemDto>> GetAll();
        Task DeleteCharacter(int id);
    }
}