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
        [Required(ErrorMessage = "Name is not specified")]
        [StringLength(30, MinimumLength = 3,
            ErrorMessage = "The string length must be between 3 and 50 characters")]
        public string Name { get; set; }
    }
}
