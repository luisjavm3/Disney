namespace Disney.Entities
{
    public class Genre : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IList<MovieSerie> MovieSeries { get; set; }
    }
}