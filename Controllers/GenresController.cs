using Disney.DTOs.Genre;
using Disney.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Disney.Controllers
{
    [Authorize]
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
        public async Task<ActionResult> AddGenre([FromForm] GenreCreateDto genreCreate)
        {
            await _genresService.AddGenre(genreCreate);
            return Ok();
        }

        [HttpGet]
        public async Task<ActionResult<List<GenreGetDto>>> GetAllGenres()
        {
            return Ok(await _genresService.GetAllGenres());
        }

        [HttpPut("{genreId}/movies/{movieId}")]
        public async Task<ActionResult> AddMovieToGenre(int genreId, int movieId)
        {
            await _genresService.AddMovieToGenre(genreId, movieId);
            return Ok();
        }
    }
}