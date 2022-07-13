using AutoMapper;
using Disney.Data;
using Disney.DTOs.Genre;
using Disney.Entities;
using Disney.Exceptions;
using Disney.Settings;
using Microsoft.EntityFrameworkCore;

namespace Disney.Services
{

    public class GenresService : IGenresService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        private readonly ImagePaths _imagePaths;

        public GenresService(DataContext context, IMapper mapper, IConfiguration configuration)
        {
            _context = context;
            _mapper = mapper;
            _configuration = configuration;
            _imagePaths = _configuration.GetSection(nameof(ImagePaths)).Get<ImagePaths>();
        }

        public async Task AddGenre(GenreCreateDto genreCreate)
        {
            var existingGenre = await _context.Genres
                                    .FirstOrDefaultAsync(g => g.Name.ToLower().Equals(genreCreate.Name.ToLower()));

            if (existingGenre != null)
                throw new AppException("Genre already exists.");

            var imagePath = "";

            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    var genre = _mapper.Map<Genre>(genreCreate);
                    await _context.Genres.AddAsync(genre);
                    await _context.SaveChangesAsync();

                    imagePath = $"{_imagePaths.Genres}/{genre.Id}{Path.GetExtension(genreCreate.Image.FileName)}";

                    using (var stream = System.IO.File.Create(imagePath))
                    {
                        await genreCreate.Image.CopyToAsync(stream);
                    }

                    genre.ImagePath = imagePath;
                    await _context.SaveChangesAsync();

                    await transaction.CommitAsync();
                }
                catch (Exception)
                {
                    await transaction.RollbackAsync();

                    if (!imagePath.Equals(""))
                        File.Delete(imagePath);

                    throw new AppException("Something went wrong when adding new genre.");
                }
            }
        }

        public async Task<IList<GenreGetDto>> GetAllGenres()
        {
            var existingGenres = await _context.Genres.ToListAsync();
            var result = existingGenres.Select(g => _mapper.Map<GenreGetDto>(g)).ToList();
            return result;
        }

        public async Task AddMovieToGenre(int genreId, int movieId)
        {
            var existingGenre = await _context.Genres.FirstOrDefaultAsync(g => g.Id == genreId);

            if (existingGenre == null)
                throw new ObjectNotFoundException("Genre not found");

            var existingMovie = await _context.MovieSeries.FirstOrDefaultAsync(m => m.Id == movieId);

            if (existingMovie == null)
                throw new ObjectNotFoundException("Movie not found");

            if (existingGenre.MovieSeries == null)
                existingGenre.MovieSeries = new List<MovieSerie>();

            existingGenre.MovieSeries.Add(existingMovie);

            await _context.SaveChangesAsync();
        }
    }


}