using System.ComponentModel.DataAnnotations;

namespace MusicCatalog.Web.ViewModels
{
    /// <summary>
    /// View model for a performer
    /// </summary>
    public class PerformerViewModel
    {
        /// <summary>
        /// Performer name
        /// </summary>
        [Required]  
        public string Name { get; set; }
    }
}
