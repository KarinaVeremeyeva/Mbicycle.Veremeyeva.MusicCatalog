using MusicCatalog.Domain;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace MusicCatalog.Infrastructure.Repositories
{
    public class AlbumRepository : GenericRepository<Album>, IAlbumRepository
    {
        public AlbumRepository(MusicContext context) : base(context) { }

        public override void Create(Album album)
        {
            context.Albums.Add(album);
        }

        public override void Delete(int id)
        {
            var albumToDelete = GetById(id);

            if (albumToDelete != null)
            {
                context.Albums.Remove(albumToDelete);
            }
        }

        public override IEnumerable<Album> GetAll()
        {
            return context.Albums;
        }

        public override Album GetById(int id)
        {
            return context.Albums.Find(id);
        }

        public override void Update(Album album)
        {
            context.Entry(album).State = EntityState.Modified;
        }
    }
}
