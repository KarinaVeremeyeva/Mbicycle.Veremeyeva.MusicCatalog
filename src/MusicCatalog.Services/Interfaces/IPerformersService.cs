using MusicCatalog.DataAccess.Entities;
using System.Collections;

namespace MusicCatalog.Services
{
    /// <summary>
    /// A performers service
    /// </summary>
    public interface IPerformersService
    {
        /// <summary>
        /// Creates a performer
        /// </summary>
        /// <param name="performer"></param>
        void CreatePerformer(Performer performer);

        /// <summary>
        /// Updates the performer
        /// </summary>
        /// <param name="performer"></param>
        void UpdatePerformer(Performer performer);

        /// <summary>
        /// Delete the performer by id
        /// </summary>
        /// <param name="performerId"></param>
        void DeletePerformer(int performerId);

        /// <summary>
        /// Returns the performer by id
        /// </summary>
        /// <param name="performerId"></param>
        /// <returns></returns>
        Performer GetPerformerById(int performerId);

        /// <summary>
        /// Returns all performers
        /// </summary>
        /// <returns></returns>
        IEnumerable GetPerformers();
    }
}
