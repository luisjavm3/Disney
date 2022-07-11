using System.Text.Json.Serialization;

namespace Disney.DTOs.Character
{
    public class CharacterUpdatedDto
    {
        public int Id { get; set; }

        [JsonIgnore]
        public string ImagePath { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public int Weight { get; set; }
        public string History { get; set; }
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