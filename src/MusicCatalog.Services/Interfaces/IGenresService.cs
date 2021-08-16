using MusicCatalog.DataAccess.Entities;
using System.Collections;

namespace MusicCatalog.Services
{
    /// <summary>
    /// A genres service
    /// </summary>
    public interface IGenresService
    {
        /// <summary>
        /// Creates a genre
        /// </summary>
        /// <param name="genre">Genre</param>
        void CreateGenre(Genre genre);

        /// <summary>
        /// Updates the genre
        /// </summary>
        /// <param name="genre">Genre</param>
        void UpdateGenre(Genre genre);

        /// <summary>
        /// Deletes the genre by id
        /// </summary>
        /// <param name="genreId">Genre id</param>
        void DeleteGenre(int genreId);

        /// <summary>
        /// Returns the genre by id
        /// </summary>
        /// <param name="genreId">Genre id</param>
        /// <returns></returns>
        Genre GetGenreById(int genreId);

        /// <summary>
        /// Returns all genres
        /// </summary>
        /// <returns>Genres</returns>
        IEnumerable GetGenres();

        /// <summary>
        /// Saves changes of the context to the database
        /// </summary>
        void Save();
    }
}
