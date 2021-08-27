using System;
using System.Collections.Generic;

namespace MusicCatalog.BusinessLogic.Models
{
    /// <summary>
    /// Represents an album dto model
    /// </summary>
    public class AlbumDto
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
        public virtual ICollection<SongDto> Songs { get; set; }
    }
}
