using AutoMapper;
using Disney.DTOs.Character;
using Disney.DTOs.Genre;
using Disney.DTOs.Movies;
using Disney.Entities;

namespace Disney
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            // Characters
            CreateMap<Character, CharacterResponseDto>();
            CreateMap<CharacterCreateDto, Character>();
            CreateMap<Character, CharacterListItemDto>();
            CreateMap<Character, CharacterUpdateDto>();
            CreateMap<Character, CharacterUpdateResponseDto>()
                .ForMember(x => x.Image, opt => opt.MapFrom(src => GetImage(src.ImagePath)));

            CreateMap<Character, CharacterGetDto>()
                .ForMember(x => x.Image, opt => opt.MapFrom(src => GetImage(src.ImagePath)))
                .ForMember(x => x.Movies, opt => opt.MapFrom(src => src.MovieSeries));

            // Movies
            CreateMap<MovieSerie, MovieListItem>()
                .ForMember(x => x.Image, opt => opt.MapFrom(src => GetImage(src.ImagePath)));

            CreateMap<MovieSerie, MovieDetailsDto>()
                .ForMember(x => x.Characters, opt => opt.Ignore());

            // Genres
            CreateMap<GenreCreateDto, Genre>()
                .ForSourceMember(x => x.Image, opt => opt.DoNotValidate());

            CreateMap<Genre, GenreGetDto>();

        }

        private string GetImage(string path)
        {
            var stream = System.IO.File.ReadAllBytes(path);
            return Convert.ToBase64String(stream);
        }
    }
}