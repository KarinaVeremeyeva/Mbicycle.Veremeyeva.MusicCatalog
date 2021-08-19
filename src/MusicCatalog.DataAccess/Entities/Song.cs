namespace MusicCatalog.DataAccess.Entities
{
    /// <summary>
    /// Represents a song
    /// </summary>
    public class Song
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
        /// Reference navigation property for genre
        /// </summary>
        public Genre Genre { get; set; }

        /// <summary>
        /// Foreign key for performer
        /// </summary>
        public int PerformerId { get; set; }

        /// <summary>
        /// Reference navigation property for performer
        /// </summary>
        public Performer Performer { get; set; }

        /// <summary>
        /// Foreign key for album
<<<<<<< HEAD
        /// </summary>
        public int AlbumId { get; set; }

        /// <summary>
        /// Reference navigation property for album
        /// </summary>
=======
        /// </summary>
        public int AlbumId { get; set; }

        /// <summary>
        /// Reference navigation property for album
        /// </summary>
>>>>>>> 6eaff59acdd126263cefa21c5ba05761cb15d388
        public Album Album { get; set; }
    }
}
