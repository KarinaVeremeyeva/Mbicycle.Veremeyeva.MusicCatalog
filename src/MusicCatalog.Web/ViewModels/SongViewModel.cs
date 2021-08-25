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
        [Required(ErrorMessage = "NameRequired")]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "NameLength")]
        public string Name { get; set; }

        /// <summary>
        /// Album id
        /// </summary>
        [Required(ErrorMessage = "AlbumRequired")]
        public int? AlbumId { get; set; }

        /// <summary>
        /// Genre id
        /// </summary>
        [Required(ErrorMessage = "GenreRequired")]
        public int? GenreId { get; set; }

        /// <summary>
        /// Performer id
        /// </summary>
        [Required(ErrorMessage = "PerformerRequired")]
        public int? PerformerId { get; set; }

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
