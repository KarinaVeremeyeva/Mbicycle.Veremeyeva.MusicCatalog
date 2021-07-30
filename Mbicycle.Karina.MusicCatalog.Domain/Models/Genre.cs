using System.Collections.Generic;

namespace Mbicycle.Karina.MusicCatalog.Domain
{
    public class Genre
    {
        public int GenreId { get; set; }
        public string Name { get; set; }

        public List<Song> Songs { get; set; }

        public Genre()
        {
            Songs = new List<Song>();
        }
    }
}
