using MusicCatalog.DataAccess.Entities;
using System.Collections.Generic;

namespace MusicCatalog.Services.Interfaces
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
        void CreateSong(Song song);

        /// <summary>
        /// Updates the album
        /// </summary>
        /// <param name="song">Song to update</param>
        void UpdateSong(Song song);

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
        Song GetSongById(int songId);

        /// <summary>
        /// Returns all songs by id
        /// </summary>
        /// <returns>Songs</returns>
        IEnumerable<Song> GetSongs();
    }
}
