using System.ComponentModel.DataAnnotations;

namespace Disney.Entities
{
    public class MovieSerie : IEntity
    {
        public int Id { get; set; }

        public string ImagePath { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        [Required]
        public int Rate { get; set; }

        public IList<Character> Characters { get; set; }
    }
}