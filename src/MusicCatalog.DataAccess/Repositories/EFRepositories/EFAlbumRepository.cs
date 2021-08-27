using Microsoft.EntityFrameworkCore;
using MusicCatalog.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MusicCatalog.DataAccess.Repositories.EFRepositories
{
    /// <summary>
    /// A repository for albums
    /// </summary>
    public class EFAlbumRepository : EFRepository<Album>
    {
        /// <summary>
        /// Albums repository
        /// </summary>
        /// <param name="context">Database context</param>
        public EFAlbumRepository(MusicContext context) : base(context)
        {
        }

        /// <summary>
        /// Creates album
        /// </summary>
        /// <param name="album">New album</param>
        public override void Create(Album album)
        {
            context.Albums.Add(album);
            context.SaveChanges();
        }

        /// <summary>
        ///  Deletes album by id
        /// </summary>
        /// <param name="id">Album id</param>
        public override void Delete(int id)
        {
            var albumToDelete = GetById(id);

            if (albumToDelete == null)
            {
                throw new ArgumentNullException(nameof(albumToDelete), $"Album with id={id} doesn't exist");
            }

            context.Albums.Remove(albumToDelete);
            context.SaveChanges();
        }

        /// <summary>
        /// Returns all albums
        /// </summary>
        /// <returns>Albums</returns>
        public override IEnumerable<Album> GetAll()
        {
            return context.Albums.ToList();
        }

        /// <summary>
        /// Returns album by id
        /// </summary>
        /// <param name="id">Album id</param>
        /// <returns>Album wit expecting id</returns>
        public override Album GetById(int id)
        {
            var albumToFind = context.Albums.Find(id);

            return albumToFind;
        }

        /// <summary>
        /// Updates album
        /// </summary>
        /// <param name="album">Album to update</param>
        public override void Update(Album album)
        {
            if (album == null)
            {
                throw new ArgumentNullException(nameof(album), $"Album doesn't exist");
            }

            context.Entry(album).State = EntityState.Modified;
            context.SaveChanges();
        }
    }
}
