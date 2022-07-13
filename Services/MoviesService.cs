using AutoMapper;
using Disney.Data;
using Disney.DTOs.Character;
using Disney.DTOs.Movies;
using Disney.Entities;
using Disney.Exceptions;
using Disney.Settings;
using Microsoft.EntityFrameworkCore;

namespace Disney.Services
{
    public class MoviesService : IMoviesService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        private readonly ImagePaths _imagePaths;

        public MoviesService(DataContext context, IMapper mapper, IConfiguration configuration)
        {
            _context = context;
            _mapper = mapper;
            _configuration = configuration;
            _imagePaths = _configuration.GetSection(nameof(ImagePaths)).Get<ImagePaths>();
        }

        public async Task AddMovie(MovieCreateDto movieCreate)
        {
            var movie = new MovieSerie
            {
                Title = movieCreate.Title,
                Released = movieCreate.Released,
                Rate = movieCreate.Rate
            };

            using (var transaction = _context.Database.BeginTransaction())
            {
                var imagePath = string.Empty;

                try
                {
                    await _context.MovieSeries.AddAsync(movie);
                    await _context.SaveChangesAsync();

                    imagePath = $"{_imagePaths.MovieSeries}/{movie.Id}{Path.GetExtension(movieCreate.Image.FileName)}";

                    using (var stream = System.IO.File.Create(imagePath))
                    {
                        await movieCreate.Image.CopyToAsync(stream);
                    }

                    movie.ImagePath = imagePath;

                    await _context.SaveChangesAsync();
                    await transaction.CommitAsync();
                }
                catch (Exception)
                {
                    await transaction.RollbackAsync();

                    if (!imagePath.Equals(string.Empty))
                        File.Delete(imagePath);

                    throw new AppException("Something went wrong when adding new movie.");
                }
            }
        }

        public async Task DeleteMovie(int id)
        {
            var existingMovie = await _context.MovieSeries.FirstOrDefaultAsync(m => m.Id == id);

            if (existingMovie == null)
                throw new ObjectNotFoundException("Movie not found.");

            var imagePath = existingMovie.ImagePath;

            _context.MovieSeries.Remove(existingMovie);
            await _context.SaveChangesAsync();

            File.Delete(imagePath);
        }

        public async Task<IList<MovieListItem>> GetAllMovies()
        {
            var existingMovies = await _context.MovieSeries.ToListAsync();
            var result = existingMovies.Select(m => _mapper.Map<MovieListItem>(m)).ToList();
            return result;
        }

        public async Task UpdateMovie(MovieUpdateDto movieUpdate, int id)
        {
            var existingMovie = await _context.MovieSeries.FirstOrDefaultAsync(m => m.Id == id);

            if (existingMovie == null)
                throw new ObjectNotFoundException("Movie not found.");

            existingMovie.Title = movieUpdate.Title;
            existingMovie.Released = movieUpdate.Released;
            existingMovie.Rate = movieUpdate.Rate;

            var imagePath = existingMovie.ImagePath;

            File.Delete(imagePath);

            using (var stream = System.IO.File.Create(imagePath))
            {
                await movieUpdate.Image.CopyToAsync(stream);
            }

            await _context.SaveChangesAsync();
        }

        public async Task<MovieDetailsDto> GetMovieDetails(int id)
        {
            var existingMovie = await _context.MovieSeries
                                    .Include(m => m.Characters)
                                    .FirstOrDefaultAsync(m => m.Id == id);

            if (existingMovie == null)
                throw new ObjectNotFoundException("Movie not found.");

            var result = _mapper.Map<MovieDetailsDto>(existingMovie);
            result.Characters = existingMovie.Characters
                                    .Select(c => _mapper.Map<CharacterListItemDto>(c))
                                    .ToList();

            return result;
        }

        public async Task AddCharacterToMovie(int movieId, int characterId)
        {
            var existingMovie = await _context.MovieSeries
                                    .Include(m => m.Characters)
                                    .FirstOrDefaultAsync(m => m.Id == movieId);

            if (existingMovie == null)
                throw new ObjectNotFoundException("Movie not found.");

            var existingCharacter = await _context.Characters.FirstOrDefaultAsync(c => c.Id == characterId);

            if (existingCharacter == null)
                throw new ObjectNotFoundException("Character not found.");

            existingMovie.Characters.Add(existingCharacter);

            await _context.SaveChangesAsync();
        }
    }
}