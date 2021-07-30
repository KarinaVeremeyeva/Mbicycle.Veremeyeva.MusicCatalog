using Microsoft.EntityFrameworkCore;
using Mbicycle.Karina.MusicCatalog.Domain;

namespace Mbicycle.Karina.MusicCatalog.Infrastructure
{
    public class MusicContext : DbContext
    {
        public MusicContext(DbContextOptions<MusicContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Genre> Genres { get; set; }
        public virtual DbSet<Performer> Performers { get; set; }
        public virtual DbSet<Song> Songs { get; set; }
        public virtual DbSet<Album> Albums { get; set; }
    }
}
