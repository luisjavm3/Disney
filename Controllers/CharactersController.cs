using Disney.DTOs.Character;
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
            CharacterResponseDto response = await _characterService.AddOne(characterCreate, image);
            return Ok(response);
        }
    }
}