using Microsoft.AspNetCore.Mvc.Rendering;
using MusicCatalog.DataAccess.Entities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MusicCatalog.Web.ViewModels
{
    /// <summary>
    /// View model for a song
    /// </summary>
    public class SongViewModel
    {
        /// <summary>
        /// Song name
        /// </summary>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// Select list of albums
        /// </summary>
        [Required]
        public SelectList Albums { get; set; }

        /// <summary>
        /// Select list of genres
        /// </summary>
        [Required]
        public SelectList Genres { get; set; }

        /// <summary>
        /// Select list of performers
        /// </summary>
        [Required]
        public SelectList Performers { get; set; }

        /// <summary>
        /// Songs collection
        /// </summary>
        public IEnumerable<Song> Songs { get; set; }
    }
}
