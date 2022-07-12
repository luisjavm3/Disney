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
    }
}