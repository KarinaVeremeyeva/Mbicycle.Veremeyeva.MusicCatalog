using System.Collections.Generic;

namespace MusicCatalog.DataAccess.Entities
{
    /// <summary>
    /// Represents a performer entity
    /// </summary>
    public class Performer
    {
        /// <summary>
        /// Primary key for performer
        /// </summary>
        public int PerformerId { get; set; }

        /// <summary>
        /// Performer name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Collection navigation property for songs
        /// </summary>
        public virtual ICollection<Song> Songs { get; set; }
    }
}
