using MusicCatalog.BusinessLogic.Models;
using System.Collections.Generic;

namespace MusicCatalog.BusinessLogic.Interfaces
{
    /// <summary>
    /// A performers service
    /// </summary>
    public interface IPerformersService
    {
        /// <summary>
        /// Creates a performer
        /// </summary>
        /// <param name="performer">Performer</param>
        void CreatePerformer(PerformerDto performer);

        /// <summary>
        /// Updates the performer
        /// </summary>
        /// <param name="performer">Performer</param>
        void UpdatePerformer(PerformerDto performer);

        /// <summary>
        /// Delete the performer by id
        /// </summary>
        /// <param name="performerId">Performer id</param>
        void DeletePerformer(int performerId);

        /// <summary>
        /// Returns the performer by id
        /// </summary>
        /// <param name="performerId">Performer id</param>
        /// <returns></returns>
        PerformerDto GetPerformerById(int performerId);

        /// <summary>
        /// Returns all performers
        /// </summary>
        /// <returns>Performers</returns>
        IEnumerable<PerformerDto> GetPerformers();
    }
}
