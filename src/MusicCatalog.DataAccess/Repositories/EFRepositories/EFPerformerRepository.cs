using Microsoft.EntityFrameworkCore;
using MusicCatalog.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MusicCatalog.DataAccess.Repositories.EFRepositories
{
    /// <summary>
    /// A repository performers
    /// </summary>
    public class EFPerformerRepository : EFRepository<Performer>
    {
        /// <summary>
        /// Performers repository
        /// </summary>
        /// <param name="context">Database context</param>
        public EFPerformerRepository(MusicContext context) : base(context)
        {
        }

        /// <summary>
        /// Creates performer
        /// </summary>
        /// <param name="performer">New performer</param>
        public override void Create(Performer performer)
        {
            context.Performers.Add(performer);
            context.SaveChanges();
        }

        /// <summary>
        /// Deletes performer by id
        /// </summary>
        /// <param name="id">Performer id to delete</param>
        public override void Delete(int id)
        {
            var performerToDelete = GetById(id);

            if (performerToDelete == null)
            {
                throw new ArgumentNullException(nameof(performerToDelete), $"Performer with id={id} doesn't exist");
            }

            context.Performers.Remove(performerToDelete);
            context.SaveChanges();
        }

        /// <summary>
        /// Returns all performers
        /// </summary>
        /// <returns>Performers</returns>
        public override IEnumerable<Performer> GetAll()
        {
            return context.Performers.ToList();
        }

        /// <summary>
        /// Returns performer by id
        /// </summary>
        /// <param name="id">Performer id</param>
        /// <returns>Performer with expecting id</returns>
        public override Performer GetById(int id)
        {
            var performerToFind = context.Performers.Find(id);

            return performerToFind;
        }

        /// <summary>
        /// Updates performer
        /// </summary>
        /// <param name="performer">Performer to update</param>
        public override void Update(Performer performer)
        {
            if (performer == null)
            {
                throw new ArgumentNullException(nameof(performer), $"Performer doesn't exist");
            }

            context.Entry(performer).State = EntityState.Modified;
            context.SaveChanges();
        }
    }
}
