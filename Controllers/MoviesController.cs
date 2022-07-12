using Disney.DTOs.Movies;
using Disney.Services;
using Microsoft.AspNetCore.Mvc;

namespace Disney.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MoviesController : ControllerBase
    {
        private readonly IMoviesService _moviesService;

        public MoviesController(IMoviesService moviesService)
        {
            _moviesService = moviesService;
        }

        [HttpPost]
        public async Task<ActionResult> AddMovie([FromForm] MovieCreateDto movieCreate)
        {
            await _moviesService.AddMovie(movieCreate);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteMovie(int id)
        {
            await _moviesService.DeleteMovie(id);
            return Ok();
        }

        [HttpGet]
        public async Task<ActionResult<IList<MovieListItem>>> AllMovies()
        {
            return Ok(await _moviesService.GetAllMovies());
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateMovie([FromForm] MovieUpdateDto movieUpdate, int id)
        {
            await _moviesService.UpdateMovie(movieUpdate, id);
            return Ok();
        }
    }
}