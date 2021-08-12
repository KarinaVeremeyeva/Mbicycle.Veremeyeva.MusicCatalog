using System.Collections.Generic;

namespace MusicCatalog.DataAccess.Entities
{
    /// <summary>
    /// Represents a genre
    /// </summary>
    public class Genre
    {
        /// <summary>
        /// Primary key for the genre
        /// </summary>
        public int GenreId { get; set; }

        /// <summary>
        /// Genre name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Collection navigation property for songs
        /// </summary>
        public virtual ICollection<Song> Songs { get; set; }
    }
}
