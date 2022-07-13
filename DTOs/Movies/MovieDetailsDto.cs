using System.Text.Json.Serialization;
using Disney.DTOs.Character;

namespace Disney.DTOs.Movies
{
    public class MovieDetailsDto
    {
        public int Id { get; set; }

        [JsonIgnore]
        public string ImagePath { get; set; }

        public string Title { get; set; }

        public int Released { get; set; }

        public int Rate { get; set; }

        public string Image
        {
            get
            {
                var stream = System.IO.File.ReadAllBytes(path: ImagePath);
                return Convert.ToBase64String(stream);
            }
        }

        public IList<CharacterListItemDto> Characters { get; set; }
    }
}