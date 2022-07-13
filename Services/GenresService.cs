using AutoMapper;
using Disney.Data;
using Disney.DTOs.Genre;
using Disney.Entities;
using Disney.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace Disney.Services
{

    public class GenresService : IGenresService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public GenresService(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task AddGenre(GenreCreateDto genreCreate)
        {
            var existingGenre = await _context.Genres
                                    .FirstOrDefaultAsync(g => g.Name.ToLower().Equals(genreCreate.Name.ToLower()));

            if (existingGenre != null)
                throw new AppException("Genre already exists.");

            var genre = _mapper.Map<Genre>(genreCreate);

            await _context.Genres.AddAsync(genre);
            await _context.SaveChangesAsync();
        }
    }
}