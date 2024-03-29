﻿using MusicCatalog.BusinessLogic.Models;
using System.Collections.Generic;

namespace MusicCatalog.BusinessLogic.Interfaces
{
    /// <summary>
    /// A songs service
    /// </summary>
    public interface ISongsService
    {
        /// <summary>
        /// Creates a song
        /// </summary>
        /// <param name="song">New song</param>
        void CreateSong(SongDto song);

        /// <summary>
        /// Updates the album
        /// </summary>
        /// <param name="song">Song to update</param>
        void UpdateSong(SongDto song);

        /// <summary>
        ///  Deletes the song by id
        /// </summary>
        /// <param name="songId">Song id</param>
        void DeleteSong(int songId);

        /// <summary>
        /// Returns the song by id
        /// </summary>
        /// <param name="songId">Song id</param>
        /// <returns>Song</returns>
        SongDto GetSongById(int songId);

        /// <summary>
        /// Returns all songs
        /// </summary>
        /// <returns>Songs</returns>
        IEnumerable<SongDto> GetSongs();
    }
}
