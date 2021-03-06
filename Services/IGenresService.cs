using Disney.DTOs.Genre;

namespace Disney.Services
{
    public interface IGenresService
    {
        Task AddGenre(GenreCreateDto genreCreate);
        Task<IList<GenreGetDto>> GetAllGenres();
        Task AddMovieToGenre(int genreId, int movieId);
    }
}