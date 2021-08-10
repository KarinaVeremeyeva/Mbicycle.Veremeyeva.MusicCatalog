using System.Collections.Generic;

namespace MusicCatalog.DataAccess.Entities
{
    /// <summary>
    /// Represents a genre
    /// </summary>
    public class Genre
    {
        public int GenreId { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Song> Songs { get; set; }
    }
}
