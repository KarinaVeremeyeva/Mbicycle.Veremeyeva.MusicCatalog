using MusicCatalog.BusinessLogic.Models;
using System.Collections.Generic;

namespace MusicCatalog.BusinessLogic.Interfaces
{
    /// <summary>
    /// A genres service
    /// </summary>
    public interface IGenresService
    {
        /// <summary>
        /// Creates a genre
        /// </summary>
        /// <param name="genre">New genre</param>
        void CreateGenre(GenreDto genre);

        /// <summary>
        /// Updates the genre
        /// </summary>
        /// <param name="genre">Genre</param>
        void UpdateGenre(GenreDto genre);

        /// <summary>
        /// Deletes the genre by id
        /// </summary>
        /// <param name="genreId">Genre id</param>
        void DeleteGenre(int genreId);

        /// <summary>
        /// Returns the genre by id
        /// </summary>
        /// <param name="genreId">Genre id</param>
        /// <returns>Genre</returns>
        GenreDto GetGenreById(int genreId);

        /// <summary>
        /// Returns all genres
        /// </summary>
        /// <returns>Genres</returns>
        IEnumerable<GenreDto> GetGenres();
    }
}
