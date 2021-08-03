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

        public MusicContext() { }

        public virtual DbSet<Genre> Genres { get; set; }
        public virtual DbSet<Performer> Performers { get; set; }
        public virtual DbSet<Song> Songs { get; set; }
        public virtual DbSet<Album> Albums { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Album>().HasKey(x => x.AlbumId);
            modelBuilder.Entity<Album>().ToTable("Albums");

            modelBuilder.Entity<Genre>().HasKey(x => x.GenreId);
            modelBuilder.Entity<Genre>().ToTable("Genres");

            modelBuilder.Entity<Performer>().HasKey(x => x.PerformerId);
            modelBuilder.Entity<Performer>().ToTable("Performers");

            modelBuilder.Entity<Song>().HasKey(x => x.SongId);
            modelBuilder.Entity<Song>().ToTable("Songs");

            base.OnModelCreating(modelBuilder);
        }
    }
}
