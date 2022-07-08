using System.ComponentModel.DataAnnotations;

namespace Disney.Entities
{
    public class Character : IEntity
    {
        public int Id { get; set; }

        // [Required]
        public string ImagePath { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public int Age { get; set; }

        [Required]
        public int Weight { get; set; }

        [Required]
        public string History { get; set; }

        public IList<MovieSerie> MovieSeries { get; set; }
    }
}