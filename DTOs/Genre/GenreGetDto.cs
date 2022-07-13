using System.Text.Json.Serialization;

namespace Disney.DTOs.Genre
{
    public class GenreGetDto
    {
        public int Id { get; set; }
        public string Name { get; set; }

        [JsonIgnore]
        public string ImagePath { get; set; }

        public string Image
        {
            get
            {
                var stream = System.IO.File.ReadAllBytes(path: ImagePath);
                return Convert.ToBase64String(stream);
            }
        }
    }
}