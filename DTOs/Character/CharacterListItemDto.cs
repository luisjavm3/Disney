using System.Text.Json.Serialization;

namespace Disney.DTOs.Character
{
    public class CharacterListItemDto
    {
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