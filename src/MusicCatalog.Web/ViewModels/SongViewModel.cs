using System.ComponentModel.DataAnnotations;

namespace MusicCatalog.Web.ViewModels
{
    /// <summary>
    /// View model for a song
    /// </summary>
    public class SongViewModel
    {
        /// <summary>
        /// Primary key
        /// </summary>
        public int SongId { get; set; }

        /// <summary>
        /// Song name
        /// </summary>
        [Required(ErrorMessage = "Name is not specified")]
        public string Name { get; set; }

        /// <summary>
        /// Album id
        /// </summary>
        [Required(ErrorMessage = "Album is not specified")]
        public int AlbumId { get; set; }

        /// <summary>
        /// Genre id
        /// </summary>
        [Required(ErrorMessage = "Genre is not specified")]
        public int GenreId { get; set; }

        /// <summary>
        /// Performer id
        /// </summary>
        [Required(ErrorMessage = "Performer is not specified")]
        public int PerformerId { get; set; }

        /// <summary>
        /// Album's name
        /// </summary>
        public string AlbumName { get; set; }

        /// <summary>
        /// Genres's name
        /// </summary>
        public string GenreName { get; set; }

        /// <summary>
        /// Performer's name
        /// </summary>
        public string PerformerName { get; set; }
    }
}
