using Microsoft.EntityFrameworkCore;
using MusicCatalog.DataAccess.Entities;

namespace MusicCatalog.DataAccess
{
    /// <summary>
    /// A database context
    /// </summary>
    public class MusicContext : DbContext
    {
        public MusicContext(DbContextOptions<MusicContext> options)
            : base(options)
        {
        }

        /// <summary>
        /// Collection of genres
        /// </summary>
        public virtual DbSet<Genre> Genres { get; set; }

        /// <summary>
        /// Collection of performers
        /// </summary>
        public virtual DbSet<Performer> Performers { get; set; }

        /// <summary>
        /// Collection of songs
        /// </summary>
        public virtual DbSet<Song> Songs { get; set; }

        /// <summary>
        ///  Collection of albums
        /// </summary>
        public virtual DbSet<Album> Albums { get; set; }

        /// <summary>
        /// Overrides to configure models
        /// </summary>
        /// <param name="modelBuilder">Defines the relationships between entities</param>
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
