using Microsoft.EntityFrameworkCore;
using MusicCatalog.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MusicCatalog.DataAccess.Repositories
{
    /// <summary>
    /// A performers repository
    /// </summary>
    public class EFPerformerRepository : EFRepository<Performer>
    {
        public EFPerformerRepository(MusicContext context) : base(context)
        {
        }

        /// <summary>
        /// Creates performer
        /// </summary>
        /// <param name="performer"></param>
        public override void Create(Performer performer)
        {
            context.Performers.Add(performer);
        }

        /// <summary>
        /// Deletes performer by id
        /// </summary>
        /// <param name="id"></param>
        public override void Delete(int id)
        {
            var performerToDelete = GetById(id);

            if (performerToDelete == null)
            {
                throw new ArgumentNullException($"Performer with id={id} doesn't exist");
            }

            context.Performers.Remove(performerToDelete);
        }

        /// <summary>
        /// Returns all performers
        /// </summary>
        /// <returns></returns>
        public override IEnumerable<Performer> GetAll()
        {
            return context.Performers.ToList();
        }

        /// <summary>
        /// Returns performer by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public override Performer GetById(int id)
        {
            var performerToFind = context.Performers.Find(id);

            if (performerToFind == null)
            {
                throw new ArgumentNullException($"Performer with id={id} doesn't exist");
            }

            return context.Performers.Find(id);
        }

        /// <summary>
        /// Updates performer
        /// </summary>
        /// <param name="performer"></param>
        public override void Update(Performer performer)
        {
            if (performer == null)
            {
                throw new ArgumentNullException($"Performer doesn't exist");
            }

            context.Entry(performer).State = EntityState.Modified;
        }
    }
}
