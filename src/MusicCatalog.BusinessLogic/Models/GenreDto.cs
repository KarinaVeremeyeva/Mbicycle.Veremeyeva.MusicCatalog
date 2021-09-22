namespace MusicCatalog.BusinessLogic.Models
{
    /// <summary>
    /// Represents a genre dto model
    /// </summary>
    public class GenreDto
    {
        /// <summary>
        /// Primary key for the genre
        /// </summary>
        public int GenreId { get; set; }

        /// <summary>
        /// Genre name
        /// </summary>
        public string Name { get; set; }
    }
}
