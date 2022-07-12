using System.ComponentModel.DataAnnotations;
using System.Configuration;

namespace Disney.DTOs.Movies
{
    public class MovieCreateDto
    {
        private static int CurrentYear = DateTime.UtcNow.Year;
        private int _released;

        [Required]
        public IFormFile Image { get; set; }

        [Required]
        public string Title { get; set; }

        // In 1888 the first movie was released.
        [Required]
        [Range(1888, 3000)]
        public int Released
        {
            get
            {
                if (_released > DateTime.UtcNow.Year)
                    return DateTime.UtcNow.Year;

                return _released;
            }
            set
            {
                _released = value;
            }
        }

        [Required]
        public int Rate { get; set; }
    }
}