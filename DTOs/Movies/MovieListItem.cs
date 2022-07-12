using System.Text.Json.Serialization;

namespace Disney.DTOs.Movies
{
    public class MovieListItem
    {
        public int Id { get; set; }

        [JsonIgnore]
        public string ImagePath { get; set; }

        public string Title { get; set; }

        public int Released { get; set; }

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