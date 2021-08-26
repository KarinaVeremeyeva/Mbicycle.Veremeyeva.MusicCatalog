using Microsoft.EntityFrameworkCore;
using MusicCatalog.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MusicCatalog.DataAccess.Repositories.EFRepositories
{
    /// <summary>
    /// A repository for genres
    /// </summary>
    public class EFGenreRepository : EFRepository<Genre>
    {
        public EFGenreRepository(MusicContext context) : base(context)
        {
        }

        /// <summary>
        /// Creates genre
        /// </summary>
        /// <param name="genre">New genre</param>
        public override void Create(Genre genre)
        {
            context.Genres.Add(genre);
            context.SaveChanges();
        }

        /// <summary>
        /// Deletes genre by id
        /// </summary>
        /// <param name="id">Genre id to delete</param>
        public override void Delete(int id)
        {
            var genreToDelete = GetById(id);

            if (genreToDelete == null)
            {
                throw new ArgumentNullException(nameof(genreToDelete), $"Genre with id={id} doesn't exist");
            }

            context.Genres.Remove(genreToDelete);
            context.SaveChanges();
        }

        /// <summary>
        /// Returns all genres
        /// </summary>
        /// <returns>Genres</returns>
        public override IEnumerable<Genre> GetAll()
        {
            return context.Genres.ToList();
        }

        /// <summary>
        /// Returns genre by id
        /// </summary>
        /// <param name="id">Genre id</param>
        /// <returns>Genre with expecting id</returns>
        public override Genre GetById(int id)
        {
            var genreToFind = context.Genres.Find(id);
            
            return genreToFind;
        }

        /// <summary>
        /// Updates genre
        /// </summary>
        /// <param name="genre">Genre to update</param>
        public override void Update(Genre genre)
        {
            if (genre == null)
            {
                throw new ArgumentNullException(nameof(genre), $"Genre doesn't exist");
            }

            context.Entry(genre).State = EntityState.Modified;
            context.SaveChanges();
        }
    }
}
