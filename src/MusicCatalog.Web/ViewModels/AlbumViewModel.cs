﻿using System;
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
        [Required(ErrorMessage = "NameRequired")]
        [StringLength(30, MinimumLength = 4, ErrorMessage = "NameLength")]
        public string Name { get; set; }

        /// <summary>
        /// Release date of the album 
        /// </summary>
        [Required(ErrorMessage = "DateRequired")]
        [Display(Name = "Release date")]
        [DisplayFormat(DataFormatString = "{0:dd'/'MM'/'yyyy}")]
        public DateTime? ReleaseDate { get; set; }
    }
}
