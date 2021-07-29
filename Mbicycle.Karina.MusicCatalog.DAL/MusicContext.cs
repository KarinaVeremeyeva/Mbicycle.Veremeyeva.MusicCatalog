using Microsoft.EntityFrameworkCore;

namespace Mbicycle.Karina.MusicCatalog.Domain
{
    public class MusicContext : DbContext
    {
        private readonly string connectionString;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(connectionString);
        }

        public virtual DbSet<Genre> Genres { get; set; }
        public virtual DbSet<Performer> Performers { get; set; }
        public virtual DbSet<Song> Songs { get; set; }
        public virtual DbSet<Album> Albums { get; set; }
    }
}
