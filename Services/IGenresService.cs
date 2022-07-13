using Disney.DTOs.Genre;

namespace Disney.Services
{
    public interface IGenresService
    {
        Task AddGenre(GenreCreateDto genreCreate);
    }
}