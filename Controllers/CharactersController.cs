using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Disney.DTOs.Character;
using Disney.Exceptions;
using Disney.Services;
using Microsoft.AspNetCore.Mvc;

namespace Disney.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CharactersController : ControllerBase
    {
        private readonly ICharacterService _characterService;

        public CharactersController(ICharacterService characterService)
        {
            _characterService = characterService;
        }

        [HttpPost]
        public async Task<ActionResult<CharacterResponseDto>> AddCharacter([FromForm] CharacterCreateDto characterCreate, IFormFile image)
        {
            CharacterResponseDto response = await _characterService.AddCharacter(characterCreate, image);
            return Ok(response);
        }

        [HttpGet]
        public async Task<ActionResult<IList<CharacterListItemDto>>> GetAllCharacters(string name = "", int age = 0, int movies = 0)
        {
            if (!name.Equals(string.Empty))
            {
                var existingCharacter = await _characterService.GetByName(name);
                return existingCharacter == null ? new CharacterListItemDto[0] : new[] { existingCharacter };
            }

            if (age != 0)
                return Ok(await _characterService.GetCharactersByAge(age));

            // if(movies!=0)


            return Ok(await _characterService.GetAll());
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCharacter(int id)
        {
            await _characterService.DeleteCharacter(id);
            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<CharacterUpdateResponseDto>> UpdateCharacter([FromForm] CharacterUpdateDto characterUpdate, [Required] IFormFile image, int id)
        {
            if (image == null)
                throw new AppException("No image provided.");

            var result = await _characterService.UpdateCharacter(characterUpdate, image, id);
            return Ok(result);
        }
    }
}