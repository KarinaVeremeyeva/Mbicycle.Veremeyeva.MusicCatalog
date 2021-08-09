using MusicCatalog.DataAccess.Entities;
using MusicCatalog.DataAccess.Repositories;
using NUnit.Framework;
using System.Linq;

namespace MusicCatalog.Tests
{
    [TestFixture]
    public class GenreTests
    {
        private const string conncetionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=TestMusicCatalog;Integrated Security=True";

        [Test]
        public void Create_CreatingGenre_SavingCorrectGenre()
        {
            GenreRepository genreRepository = new GenreRepository(conncetionString);
            var genre = new Genre()
            {
                GenreId = 1,
                Name = "Genre1"
            };

            genreRepository.Create(genre);

            var actual = genreRepository.GetAll().Where(g => g.GenreId == 1).Single();
            var expected = new Genre()
            {
                GenreId = 1,
                Name = "Genre1"
            };

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Update_UpdatingGenre_GenreWasUpdated()
        {
            GenreRepository genreRepository = new GenreRepository(conncetionString);
            var genreToUpdate = new Genre()
            {
                GenreId = 1,
                Name = "NewGenre1"
            };

            genreRepository.Update(genreToUpdate);

            var actual = genreRepository.GetAll().Where(g => g.GenreId == 1).Single();
            var expected = new Genre()
            {
                GenreId = 1,
                Name = "NewGenre1"
            };

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Delete_DeletingGenre_GenreWasDeleted()
        {
            GenreRepository genreRepository = new GenreRepository(conncetionString);
            var genreToDelete = new Genre()
            {
                GenreId = 3,
                Name = "New genre"
            };

            genreRepository.Delete(genreToDelete.GenreId);

            var actual = genreRepository.GetAll().Where(g => g.GenreId == 3)
                .DefaultIfEmpty().Single();

            Assert.AreEqual(null, actual);
        }

        [Test]
        public void GetById_GettingGenreById_GenreWasFinded()
        {
            GenreRepository genreRepository = new GenreRepository(conncetionString);
            var actual = genreRepository.GetById(1);
            var expected = genreRepository.GetAll().Where(g => g.GenreId == 1).FirstOrDefault();

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void GetAll_GettingGenres_GetAllGenres()
        {
            GenreRepository genreRepository = new GenreRepository(conncetionString);
            var actual = genreRepository.GetAll();
            var expected = genreRepository.GetAll().Where(g => g.GenreId == 1).FirstOrDefault();

            Assert.AreEqual(expected, actual);
        }
    }
}