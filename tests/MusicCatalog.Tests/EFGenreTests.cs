using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System.Linq;
using MusicCatalog.DataAccess;
using MusicCatalog.DataAccess.Entities;
using MusicCatalog.DataAccess.Repositories;
using System;

namespace MusicCatalog.Tests
{
    [TestFixture]
    public class EFGenreTests
    {
        private MusicContext Context { get; set; }

        private EFGenreRepository GenreRepository { get; set; }

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<MusicContext>()
               .UseInMemoryDatabase(databaseName: "TestDb")
               .Options;

            Context = new MusicContext(options);
            GenreRepository = new EFGenreRepository(Context);
        }

        [Test]
        public void Create_SaveCorrectGenre_GenreWasSaved()
        {
            var genre = new Genre
            {
                GenreId = 1,
                Name = "Genre1",
            };
            GenreRepository.Create(genre);
            Context.SaveChanges();

            var actual = Context.Genres.Single();
            var expected = Context.Genres.First();

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Update_UpdateGenre_GenreWasChanged()
        {
            var genre = new Genre
            {
                GenreId = 1,
                Name = "Genre1",
            };
            GenreRepository.Create(genre);
            Context.SaveChanges();

            var genreToUpdate = Context.Genres.Single();
            genreToUpdate.Name = "ChangedName";
            GenreRepository.Update(genreToUpdate);
            Context.SaveChanges();

            var actual = Context.Genres.Single();
            var expected = Context.Genres.First();

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Update_UpdateNotExistingGenre_ThrowsArgumentNullException()
        {
            var genre = Context.Genres.Where(g => g.GenreId == 1).FirstOrDefault();

            Assert.Throws<ArgumentNullException>(
               () => GenreRepository.Update(genre),
               $"Genre doesn't exist");
        }

        [Test]
        public void Delete_DeleteGenre_GenreWasDeleted()
        {
            var genre = new Genre
            {
                GenreId = 1,
                Name = "Genre1",
            };
            GenreRepository.Create(genre);
            Context.SaveChanges();

            GenreRepository.Delete(genre.GenreId);
            Context.SaveChanges();

            var actual = Context.Genres.DefaultIfEmpty();
            var expected = Context.Genres.DefaultIfEmpty();

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Delete_DeleteNotExistingGenreId_ThrowsArgumentNullException()
        {
            var id = 1;

            Assert.Throws<ArgumentNullException>(
               () => GenreRepository.Delete(id),
               $"Genre with id={id} doesn't exist");
        }

        [Test]
        public void GetById_GetGenreById_GenreWithExpectingId()
        {
            var genre = new Genre
            {
                GenreId = 1,
                Name = "Genre1",
            };
            GenreRepository.Create(genre);
            Context.SaveChanges();

            var expected = Context.Genres.First();
            var actual = GenreRepository.GetById(genre.GenreId);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void GetById_GenreNotExists_ThrowsArgumentNullException()
        {
            var id = 1;

            Assert.Throws<ArgumentNullException>(
               () => GenreRepository.GetById(id),
               $"Genre with id={id} doesn't exist");
        }

        [Test]
        public void GetAll_GetGenres_GetGenresList()
        {
            var genre1 = new Genre
            {
                GenreId = 1,
                Name = "Genre1",
            };
            var genre2 = new Genre
            {
                GenreId = 2,
                Name = "Genre2",
            };
            GenreRepository.Create(genre1);
            GenreRepository.Create(genre2);

            Context.SaveChanges();

            var expected = Context.Genres.ToList();
            var actual = GenreRepository.GetAll();

            Assert.AreEqual(expected, actual);
        }

        [TearDown]
        public void CleanUp()
        {
            Context.Database.EnsureDeleted();
            Context.Dispose();
        }
    }
}
