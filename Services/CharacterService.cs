using AutoMapper;
using Disney.Data;
using Disney.DTOs.Character;
using Disney.Entities;
using Disney.Exceptions;
using Disney.Settings;

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

        public async Task<CharacterResponseDto> AddOne(CharacterCreateDto charaterCreate, IFormFile imageFile)
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
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();

                    if (!imagePath.Equals(string.Empty))
                        File.Delete(path: imagePath);

                    throw new AppException("Something went wrong when adding new character.");
                }
            }

            return _mapper.Map<CharacterResponseDto>(character);
        }

        // public async Task<IList<>>
    }
}