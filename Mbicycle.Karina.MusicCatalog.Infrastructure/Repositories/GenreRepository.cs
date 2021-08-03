using Mbicycle.Karina.MusicCatalog.Domain;
using System.Collections.Generic;
using System.Linq;

namespace Mbicycle.Karina.MusicCatalog.Infrastructure.Repositories
{
    public class GenreRepository : GenericRepository<Genre>, IGenreRepository
    {
        public GenreRepository(MusicContext context) : base(context) { }

        public override void Create(Genre genre)
        {
            dbSet.Add(genre);
        }

        public override void Delete(int id)
        {
            var genreToDelete = dbSet.Where(g => g.GenreId == id).FirstOrDefault();

            if (genreToDelete != null)
            {
                dbSet.Remove(genreToDelete);
            }
        }

        public override Genre GetById(int id)
        {
            return dbSet.Find(id);
        }

        public override IEnumerable<Genre> GetAll()
        {
            return dbSet.ToList();
        }

        public override void Update(Genre genre)
        {
            var genreToUpdate = dbSet.Where(g => g.GenreId == genre.GenreId).FirstOrDefault();

            if (genreToUpdate != null)
            {
                genreToUpdate.Name = genre.Name;
            }
        }
    }
}
