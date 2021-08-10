using System.Collections.Generic;

namespace MusicCatalog.DataAccess.Entities
{
    /// <summary>
    /// Represents a performer
    /// </summary>
    public class Performer
    {
        public int PerformerId { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Song> Songs { get; set; }
    }
}
