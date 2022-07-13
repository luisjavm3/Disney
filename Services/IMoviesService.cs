using Disney.DTOs.Movies;

namespace Disney.Services
{
    public interface IMoviesService
    {
        Task AddMovie(MovieCreateDto movieCreate);
        Task DeleteMovie(int id);
        Task<IList<MovieListItem>> GetAllMovies();
        Task UpdateMovie(MovieUpdateDto movieUpdate, int id);
        Task<MovieDetailsDto> GetMovieDetails(int id);
        Task AddCharacterToMovie(int movieId, int characterId);
        Task<IList<MovieListItem>> GetMoviesByTitle(string title);
        Task<IList<MovieListItem>> GetMoviesByGenre(int genreId);
    }
}