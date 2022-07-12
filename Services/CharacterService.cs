using System.ComponentModel.DataAnnotations;
using AutoMapper;
using Disney.Data;
using Disney.DTOs.Character;
using Disney.Entities;
using Disney.Exceptions;
using Disney.Settings;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Disney.Services
{
    public class CharacterService : ICharacterService
    {
        private readonly DataContext _context;
        private readonly ImagePaths _imagePaths;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public CharacterService(DataContext context, IConfiguration configuration, IMapper mapper)
        {
            _context = context;
            _configuration = configuration;
            _mapper = mapper;

            _imagePaths = _configuration.GetSection(nameof(ImagePaths)).Get<ImagePaths>();
        }

        public async Task<CharacterGetDto> GetById(int id)
        {
            var existingCharacter = await _context.Characters.FirstOrDefaultAsync(c => c.Id == id);

            if (existingCharacter == null)
                return null;

            return _mapper.Map<CharacterGetDto>(existingCharacter);
        }

        public async Task<CharacterResponseDto> AddCharacter(CharacterCreateDto charaterCreate, IFormFile imageFile)
        {
            if (imageFile.Length <= 0)
                throw new ArgumentOutOfRangeException("No image file found.");

            if (charaterCreate == null)
                throw new ArgumentNullException("No character-create-dto found.");

            Character character = null;
            String imagePath = string.Empty;

            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    character = _mapper.Map<Character>(charaterCreate);
                    await _context.Characters.AddAsync(character);

                    await _context.SaveChangesAsync();

                    imagePath = $"{_imagePaths.Characters}/{character.Id}{Path.GetExtension(imageFile.FileName)}";

                    using (var stream = System.IO.File.Create(path: imagePath))
                    {
                        await imageFile.CopyToAsync(stream);
                    }

                    character.ImagePath = imagePath;

                    await _context.SaveChangesAsync();

                    await transaction.CommitAsync();
                }
                catch (Exception)
                {
                    await transaction.RollbackAsync();

                    if (!imagePath.Equals(string.Empty))
                        File.Delete(path: imagePath);

                    throw new AppException("Something went wrong when adding new character.");
                }
            }

            return _mapper.Map<CharacterResponseDto>(character);
        }

        public async Task<IList<CharacterListItemDto>> GetAll()
        {
            var characters = await _context.Characters.ToListAsync();
            var result = characters.Select(c => _mapper.Map<CharacterListItemDto>(c)).ToList();

            return result;
        }

        public async Task DeleteCharacter(int id)
        {
            var existingCharacter = await _context.Characters.SingleOrDefaultAsync(c => c.Id == id);

            if (existingCharacter == null)
                throw new ObjectNotFoundException("Character not found.");

            _context.Characters.Remove(existingCharacter);
            await _context.SaveChangesAsync();

            File.Delete(existingCharacter.ImagePath);
        }

        [HttpPut("{id}")]
        public async Task<CharacterUpdateResponseDto> UpdateCharacter([FromForm] CharacterUpdateDto characterUpdate, IFormFile imageFile, int id)
        {
            var existingCharacter = await _context.Characters.FirstOrDefaultAsync(c => c.Id == id);

            if (existingCharacter == null)
                throw new ObjectNotFoundException("Character not found.");

            File.Delete(existingCharacter.ImagePath);

            using (var stream = System.IO.File.Create(existingCharacter.ImagePath))
            {
                await imageFile.CopyToAsync(stream);
            }

            existingCharacter.Name = characterUpdate.Name;
            existingCharacter.Age = characterUpdate.Age;
            existingCharacter.Weight = characterUpdate.Weight;
            existingCharacter.History = characterUpdate.History;
            await _context.SaveChangesAsync();

            var result = _mapper.Map<CharacterUpdateResponseDto>(existingCharacter);
            return result;
        }

        public async Task<CharacterListItemDto> GetByName(string name)
        {
            var existingCharacter = await _context.Characters.FirstOrDefaultAsync(c => c.Name.ToLower().Equals(name.ToLower()));

            if (existingCharacter == null)
                return null;

            var result = _mapper.Map<CharacterListItemDto>(existingCharacter);
            return result;
        }

        public async Task<IList<CharacterListItemDto>> GetCharactersByAge(int age)
        {
            var existingCharacters = await _context.Characters.Where(c => c.Age == age).ToListAsync();

            if (existingCharacters == null)
                return null;

            var result = existingCharacters.Select(c => _mapper.Map<CharacterListItemDto>(c)).ToList();
            return result;
        }

        public async Task<IList<CharacterListItemDto>> GetCharactersFromMovie(int movieId)
        {
            var existingMovie = await _context.MovieSeries
                                .Include(m => m.Characters)
                                .FirstOrDefaultAsync(m => m.Id == movieId);

            if (existingMovie == null)
                return new CharacterListItemDto[0];

            var result = existingMovie.Characters
                            .Select(c => _mapper.Map<CharacterListItemDto>(c))
                            .ToList();

            return result;
        }
    }
}