using System;

namespace MusicCatalog.DataAccess.Entities
{
    /// <summary>
    /// Represents an album
    /// </summary>
    public class Album
    {
        /// <summary>
        /// Primary key for album
        /// </summary>
        public int AlbumId { get; set; }

        /// <summary>
        /// Album name 
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Release date
        /// </summary>
        public DateTime ReleaseDate { get; set; }

        /// <summary>
        /// Foreign key for song
        /// </summary>
        public int SongId { get; set; }

        /// <summary>
        /// Reference navigation property for song
        /// </summary>
        public Song Song { get; set; }
    }
}
