using System.ComponentModel.DataAnnotations;

namespace MusicCatalog.Web.ViewModels
{
    /// <summary>
    /// View model for a performer
    /// </summary>
    public class PerformerViewModel
    {
        /// <summary>
        /// Primary key
        /// </summary>
        public int PerformerId { get; set; }

        /// <summary>
        /// Performer's name
        /// </summary>
        [Required(ErrorMessage = "NameRequired")]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "NameLength")]
        public string Name { get; set; }
    }
}
