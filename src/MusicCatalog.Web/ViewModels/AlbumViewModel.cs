using System;
using System.ComponentModel.DataAnnotations;

namespace MusicCatalog.Web.ViewModels
{
    /// <summary>
    /// View model for an album
    /// </summary>
    public class AlbumViewModel
    {
        /// <summary>
        /// Album name 
        /// </summary>
        [Required] 
        public string Name { get; set; }

        /// <summary>
        /// Album name 
        /// </summary>
        [Required]
        [Display(Name = "Release date")]
        [DisplayFormat(DataFormatString = "{0:dd'/'MM'/'yyyy}")]
        public DateTime ReleaseDate { get; set; }
    }
}
