using Disney.DTOs.Movies;

namespace Disney.Services
{
    public interface IMoviesService
    {
        Task AddMovie(MovieCreateDto movieCreate);
        Task DeleteMovie(int id);
        Task<IList<MovieListItem>> GetAllMovies();
    }
}