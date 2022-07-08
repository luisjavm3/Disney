using Disney.Entities;
using Microsoft.EntityFrameworkCore;

namespace Disney.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        { }

        public DbSet<Character> Characters { get; set; }
        public DbSet<MovieSerie> MovieSeries { get; set; }
        public DbSet<Genre> Genres { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Character>()
                .HasAlternateKey(r => r.Name);
        }
    }
}