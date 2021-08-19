using Microsoft.EntityFrameworkCore;
using MusicCatalog.DataAccess;
using MusicCatalog.DataAccess.Entities;
using MusicCatalog.DataAccess.Repositories.EFRepositories;
using NUnit.Framework;
using System;
using System.Linq;

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
            // Arrange
            var genre = new Genre
            {
                GenreId = 1,
                Name = "Genre1",
            };

            // Act
            GenreRepository.Create(genre);
            Context.SaveChanges();

            var actual = Context.Genres.Single();
            var expected = Context.Genres.First();

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Update_UpdateGenre_GenreWasChanged()
        {
            // Arrange
            var genre = new Genre
            {
                GenreId = 1,
                Name = "Genre1",
            };
            GenreRepository.Create(genre);
            Context.SaveChanges();

            var genreToUpdate = Context.Genres.Single();
            genreToUpdate.Name = "ChangedName";

            // Act
            GenreRepository.Update(genreToUpdate);
            Context.SaveChanges();

            var actual = Context.Genres.Single();
            var expected = Context.Genres.First();

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Update_UpdateNotExistingGenre_ThrowsArgumentNullException()
        {
            // Arrange
            var genre = Context.Genres.Where(g => g.GenreId == 1).FirstOrDefault();

            // Act and assert
            Assert.Throws<ArgumentNullException>(
               () => GenreRepository.Update(genre),
               $"Genre doesn't exist");
        }

        [Test]
        public void Delete_DeleteGenre_GenreWasDeleted()
        {
            // Arrange
            var genre = new Genre
            {
                GenreId = 1,
                Name = "Genre1",
            };
            GenreRepository.Create(genre);
            Context.SaveChanges();

            // Act
            GenreRepository.Delete(genre.GenreId);
            Context.SaveChanges();

            var actual = Context.Genres.DefaultIfEmpty();
            var expected = Context.Genres.DefaultIfEmpty();

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Delete_DeleteNotExistingGenreId_ThrowsArgumentNullException()
        {
            // Arrange
            var id = 1;

            // Act and assert
            Assert.Throws<ArgumentNullException>(
               () => GenreRepository.Delete(id),
               $"Genre with id={id} doesn't exist");
        }

        [Test]
        public void GetById_GetGenreById_GenreWithExpectingId()
        {
            // Arrange
            var genre = new Genre
            {
                GenreId = 1,
                Name = "Genre1",
            };
            GenreRepository.Create(genre);
            Context.SaveChanges();

            // Act
            var expected = Context.Genres.First();
            var actual = GenreRepository.GetById(genre.GenreId);

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void GetById_GenreNotExists_ThrowsArgumentNullException()
        {
            // Arrange
            var id = 1;

            // Act and assert
            Assert.Throws<ArgumentNullException>(
               () => GenreRepository.GetById(id),
               $"Genre with id={id} doesn't exist");
        }

        [Test]
        public void GetAll_GetGenres_GetGenresList()
        {
            // Arrange
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

            // Act
            var expected = Context.Genres.ToList();
            var actual = GenreRepository.GetAll();

            // Assert
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
