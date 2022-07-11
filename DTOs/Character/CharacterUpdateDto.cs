using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Disney.DTOs.Character
{
    public class CharacterUpdateDto
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public int Age { get; set; }

        [Required]
        public int Weight { get; set; }

        [Required]
        public string History { get; set; }
    }
}