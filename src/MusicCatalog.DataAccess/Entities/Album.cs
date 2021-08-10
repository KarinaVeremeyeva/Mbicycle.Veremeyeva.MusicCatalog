using System;

namespace MusicCatalog.DataAccess.Entities
{
    /// <summary>
    /// Represents an album
    /// </summary>
    public class Album
    {
        public int AlbumId { get; set; }
        public string Name { get; set; }
        public DateTime ReleaseDate { get; set; }
        public int SongId { get; set; }
        public Song Song { get; set; }
    }
}
