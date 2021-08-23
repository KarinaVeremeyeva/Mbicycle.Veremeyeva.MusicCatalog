using System.ComponentModel.DataAnnotations;

namespace MusicCatalog.Web.ViewModels
{
    /// <summary>
    /// View model for a genre
    /// </summary>
    public class GenreViewModel
    {
        /// <summary>
        /// Genre name
        /// </summary>
        [Required]
        public string Name { get; set; }
    }
}
