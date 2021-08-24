using System;
using System.ComponentModel.DataAnnotations;

namespace MusicCatalog.Web.ViewModels
{
    /// <summary>
    /// View model for the album
    /// </summary>
    public class AlbumViewModel
    {
        /// <summary>
        /// Primary key
        /// </summary>
        public int AlbumId { get; set; }

        /// <summary>
        /// Album name 
        /// </summary>
        [Required (ErrorMessage = "Name is not specified")]
        [StringLength(30, MinimumLength = 3,
            ErrorMessage = "The string length must be between 3 and 50 characters")]
        public string Name { get; set; }

        /// <summary>
        /// Release date of the album 
        /// </summary>
        [Required(ErrorMessage = "Date of release is not specified")]
        [Display(Name = "Release date")]
        [DisplayFormat(DataFormatString = "{0:dd'/'MM'/'yyyy}")]
        public DateTime ReleaseDate { get; set; }
    }
}
