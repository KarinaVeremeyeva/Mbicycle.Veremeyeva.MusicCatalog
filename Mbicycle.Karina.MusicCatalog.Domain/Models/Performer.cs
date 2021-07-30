using System.Collections.Generic;

namespace Mbicycle.Karina.MusicCatalog.Domain
{
    public class Performer
    {
        public int PerformerId { get; set; }
        public string Name { get; set; }
        public List<Song> Songs { get; set; }
        public Performer()
        {
            Songs = new List<Song>();
        }
    }
}
