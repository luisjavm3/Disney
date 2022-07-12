using Disney.DTOs.Character;
using Microsoft.AspNetCore.Mvc;

namespace Disney.Services
{
    public interface ICharacterService
    {
        Task<CharacterResponseDto> AddCharacter(CharacterCreateDto charaterCreate, IFormFile imageFile);
        Task<IList<CharacterListItemDto>> GetAll();
        Task DeleteCharacter(int id);
        Task<CharacterUpdateResponseDto> UpdateCharacter(CharacterUpdateDto characterUpdate, IFormFile imageFile, int id);
        Task<CharacterListItemDto> GetByName(string name);
        Task<IList<CharacterListItemDto>> GetCharactersByAge(int age);
        Task<IList<CharacterListItemDto>> GetCharactersFromMovie(int movieId);
        Task<CharacterGetDto> GetById(int id);
    }
}