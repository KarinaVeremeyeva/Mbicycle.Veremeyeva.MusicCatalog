using System;
using System.Collections.Generic;

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
        /// Collection navigation property for songs
        /// </summary>
        public virtual ICollection<Song> Songs { get; set; }
    }
}
