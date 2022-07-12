using System.ComponentModel.DataAnnotations;

namespace Disney.DTOs.Movies
{
    public class MovieUpdateDto
    {
        [Required]
        public IFormFile Image { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public int Released { get; set; }

        [Required]
        public int Rate { get; set; }

    }
}