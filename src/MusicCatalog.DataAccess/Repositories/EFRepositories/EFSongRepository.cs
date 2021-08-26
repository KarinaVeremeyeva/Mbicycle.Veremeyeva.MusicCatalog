using Microsoft.EntityFrameworkCore;
using MusicCatalog.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MusicCatalog.DataAccess.Repositories.EFRepositories
{
    /// <summary>
    /// A song repository
    /// </summary>
    public class EFSongRepository : EFRepository<Song>
    {
        /// <summary>
        /// Songs repository
        /// </summary>
        /// <param name="context">Database context</param>
        public EFSongRepository(MusicContext context) : base(context)
        {
        }

        /// <summary>
        /// Creates song
        /// </summary>
        /// <param name="song">New song</param>
        public override void Create(Song song)
        {
            context.Songs.Add(song);
            context.SaveChanges();
        }

        /// <summary>
        /// Deletes song by id
        /// </summary>
        /// <param name="id">Song id</param>
        public override void Delete(int id)
        {
            var songToDelete = GetById(id);

            if (songToDelete == null)
            {
                throw new ArgumentNullException(nameof(songToDelete), $"Song with id={id} doesn't exist");
            }

            context.Songs.Remove(songToDelete);
            context.SaveChanges();
        }

        /// <summary>
        /// Returns all songs
        /// </summary>
        /// <returns>Songs</returns>
        public override IEnumerable<Song> GetAll()
        {
            return context.Songs
                .Include(s => s.Performer)
                .Include(s => s.Genre)
                .Include(s => s.Album).ToList();
        }

        /// <summary>
        ///  Returns song by id
        /// </summary>
        /// <param name="id">Song id</param>
        /// <returns>Song with expecting id</returns>
        public override Song GetById(int id)
        {
            var songToFind = context.Songs.Where(q => q.SongId == id)
                .Include(s => s.Performer)
                .Include(s => s.Genre)
                .Include(s => s.Album)
                .SingleOrDefault();

            return songToFind;
        }

        /// <summary>
        /// Updates song
        /// </summary>
        /// <param name="song">Song to update</param>
        public override void Update(Song song)
        {
            if (song == null)
            {
                throw new ArgumentNullException(nameof(song), $"Song doesn't exist");
            }

            context.Entry(song).State = EntityState.Modified;
            context.SaveChanges();
        }
    }
}
