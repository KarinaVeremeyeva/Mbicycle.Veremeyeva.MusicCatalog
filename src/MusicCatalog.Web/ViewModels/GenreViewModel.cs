using System.ComponentModel.DataAnnotations;

namespace MusicCatalog.Web.ViewModels
{
    /// <summary>
    /// View model for the genre
    /// </summary>
    public class GenreViewModel
    {
        /// <summary>
        /// Primary key
        /// </summary>
        public int GenreId { get; set; }

        /// <summary>
        /// Genre name
        /// </summary>
        [Required(ErrorMessage = "NameRequired")]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "NameLength")]
        public string Name { get; set; }
    }
}
