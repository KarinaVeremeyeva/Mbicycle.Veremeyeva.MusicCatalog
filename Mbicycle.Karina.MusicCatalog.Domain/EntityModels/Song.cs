using System.Collections.Generic;

namespace Mbicycle.Karina.MusicCatalog.Domain
{
    public class Song
    {
        public int SongId { get; set; }
        public string Name { get; set; }
        public int GenreId { get; set; }
        public Genre Genre { get; set; }
        public int PerformerId { get; set; }
        public Performer Performer { get; set; }
        public virtual ICollection<Album> Albums { get; set; }
    }
}
