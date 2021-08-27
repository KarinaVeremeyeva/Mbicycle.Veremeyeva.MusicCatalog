using System.Collections.Generic;

namespace MusicCatalog.BusinessLogic.Models
{
    /// <summary>
    /// Represents a performer dto model
    /// </summary>
    public class PerformerDto
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
        public virtual ICollection<SongDto> Songs { get; set; }
    }
}
