using MusicCatalog.Domain;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace MusicCatalog.Infrastructure.Repositories
{
    public class SongRepository : GenericRepository<Song>, ISongRepository
    {
        public SongRepository(MusicContext context) : base(context) { }

        public override void Create(Song song)
        {
            context.Songs.Add(song);
        }

        public override void Delete(int id)
        {
            var songToDelete = GetById(id);

            if (songToDelete != null)
            {
                context.Songs.Remove(songToDelete);
            }
        }

        public override IEnumerable<Song> GetAll()
        {
            return context.Songs.ToList();
        }

        public override Song GetById(int id)
        {
            return context.Songs.Find(id);
        }

        public override void Update(Song song)
        {
            context.Entry(song).State = EntityState.Modified;
        }
    }
}
