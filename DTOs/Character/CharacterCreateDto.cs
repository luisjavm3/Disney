using System.ComponentModel.DataAnnotations;

namespace Disney.DTOs.Character
{
    public class CharacterCreateDto
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public int Age { get; set; }

        [Required]
        public string Weight { get; set; }

        [Required]
        public string History { get; set; }
    }
}