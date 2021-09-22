namespace MusicCatalog.BusinessLogic.Models
{
    /// <summary>
    /// Represents a song dto model
    /// </summary>
    public class SongDto
    {
        /// <summary>
        /// Primary key for song
        /// </summary>
        public int SongId { get; set; }

        /// <summary>
        /// Song name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Foreign key for genre
        /// </summary>
        public int GenreId { get; set; }

        /// <summary>
        /// Foreign key for performer
        /// </summary>
        public int PerformerId { get; set; }

        /// <summary>
        /// Foreign key for album
        /// </summary>
        public int AlbumId { get; set; }
    }
}
