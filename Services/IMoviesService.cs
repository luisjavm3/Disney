using Disney.DTOs.Movies;

namespace Disney.Services
{
    public interface IMoviesService
    {
        Task AddMovie(MovieCreateDto movieCreate);
    }
}