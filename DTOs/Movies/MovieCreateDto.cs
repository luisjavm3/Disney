using System.ComponentModel.DataAnnotations;

namespace Disney.DTOs.Movies
{
    public class MovieCreateDto
    {
        [Required]
        public IFormFile Image { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public int Rate { get; set; }
    }
}