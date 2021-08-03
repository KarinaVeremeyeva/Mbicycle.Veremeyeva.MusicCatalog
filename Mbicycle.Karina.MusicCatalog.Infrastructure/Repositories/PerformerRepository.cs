using Mbicycle.Karina.MusicCatalog.Domain;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Mbicycle.Karina.MusicCatalog.Infrastructure.Repositories
{
    public class PerformerRepository : GenericRepository<Performer>, IPerformerRepository
    {
        public PerformerRepository(MusicContext context) : base(context) { }

        public override void Create(Performer performer)
        {
            context.Performers.Add(performer);
        }

        public override void Delete(int id)
        {
            var performerToDelete = GetById(id);

            if (performerToDelete != null)
            {
                context.Performers.Remove(performerToDelete);
            }
        }

        public override IEnumerable<Performer> GetAll()
        {
            return context.Performers.ToList();
        }

        public override Performer GetById(int id)
        {
            return context.Performers.Find(id);
        }

        public override void Update(Performer performer)
        {
            context.Entry(performer).State = EntityState.Modified;
        }
    }
}
