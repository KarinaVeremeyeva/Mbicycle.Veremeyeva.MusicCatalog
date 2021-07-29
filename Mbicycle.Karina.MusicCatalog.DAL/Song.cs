namespace Mbicycle.Karina.MusicCatalog.DAL
{
    public class Song
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int GenreId { get; set; }
        public Genre Genre { get; set; }
        public int PerformerId { get; set; }
        public Performer Performer { get; set; }
    }
}
