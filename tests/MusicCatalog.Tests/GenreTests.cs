using MusicCatalog.DataAccess.Entities;
using MusicCatalog.DataAccess.Repositories;
using NUnit.Framework;
using System.Data.SqlClient;
using System.Linq;

namespace MusicCatalog.Tests
{
    [TestFixture]
    public class GenreTests
    {
        private const string conncetionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=TestMusicCatalog;Integrated Security=True";

        [SetUp]
        public void SetUp()
        {
            string sqlExpression = $"DELETE FROM Genres";
            using (var connection = new SqlConnection(conncetionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                command.ExecuteNonQuery();
            }
        }

        [Test]
        public void Create_CreatingGenre_SavingCorrectGenre()
        {
            GenreRepository genreRepository = new GenreRepository(conncetionString);
            var genre = new Genre()
            {
                Name = "Genre1"
            };
            genreRepository.Create(genre);

            var actual = genreRepository.GetAll().Single();

            Assert.AreEqual(genre.Name, actual.Name);
        }

        [Test]
        public void Update_UpdatingGenre_GenreWasUpdated()
        {
            GenreRepository genreRepository = new GenreRepository(conncetionString);
            var genreToUpdate = new Genre()
            {
                Name = "Genre1"
            };
            genreRepository.Create(genreToUpdate);

            var savedGenre = genreRepository.GetAll().Single();
            savedGenre.Name = "ChangedName";
            genreRepository.Update(savedGenre);

            var expected = genreRepository.GetAll().Single();

            Assert.AreEqual(expected.Name, savedGenre.Name);
            Assert.AreEqual(expected.GenreId, savedGenre.GenreId);
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
        public void GetById_GettingGenreById_GenreWasFound()
        {
            GenreRepository genreRepository = new GenreRepository(conncetionString);
            var genre = new Genre
            {
                Name = "Genre1"
            };
            genreRepository.Create(genre);

            var expected = genreRepository.GetAll().FirstOrDefault();
            var actual = genreRepository.GetById(expected.GenreId);

            Assert.AreEqual(expected.Name, actual.Name);
            Assert.AreEqual(expected.GenreId, actual.GenreId);
        }

        [Test]
        public void GetAll_GettingGenres_GetAllGenres()
        {
            GenreRepository genreRepository = new GenreRepository(conncetionString);
            var genre1 = new Genre
            {
                Name = "Genre1"
            };
            genreRepository.Create(genre1);

            var genre2 = new Genre
            {
                Name = "Genre2"
            };
            genreRepository.Create(genre2);

            var allGenres = genreRepository.GetAll().ToList();

            Assert.AreEqual(allGenres.Count, 2);
            Assert.AreEqual(allGenres[0].Name, genre1.Name);
            Assert.AreEqual(allGenres[1].Name, genre2.Name);
        }
    }
}