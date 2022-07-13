using Disney.DTOs.Genre;
using Disney.Services;
using Microsoft.AspNetCore.Mvc;

namespace Disney.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GenresController : ControllerBase
    {
        private readonly IGenresService _genresService;

        public GenresController(IGenresService genresService)
        {
            _genresService = genresService;
        }

        [HttpPost]
        public async Task<ActionResult> AddGenre(GenreCreateDto genreCreate)
        {
            await _genresService.AddGenre(genreCreate);
            return Ok();
        }
    }
}