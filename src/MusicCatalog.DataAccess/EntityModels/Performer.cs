using System.Collections.Generic;

namespace MusicCatalog.Domain
{
    public class Performer
    {
        public int PerformerId { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Song> Songs { get; set; }
    }
}
