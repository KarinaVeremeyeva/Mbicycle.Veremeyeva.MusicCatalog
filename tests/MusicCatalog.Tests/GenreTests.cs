using Microsoft.Extensions.Configuration;
using MusicCatalog.DataAccess.Entities;
using MusicCatalog.DataAccess.Repositories;
using NUnit.Framework;
using System.Linq;

namespace MusicCatalog.Tests
{
    [TestFixture]
    public class GenreTests
    {
        private GenreRepository GenreRepository { get; set; }

        private IConfiguration Configuration { get; set; }

        private DatabaseConfiguration _dbConfiguration;

        [SetUp]
        public void SetUp()
        {
            Configuration = new ConfigurationBuilder()
                .AddJsonFile(path: "appsettings.json")
                .Build();

            _dbConfiguration = new DatabaseConfiguration(Configuration);
            _dbConfiguration.DeployTestDatabase();

            var connectionString = Configuration.GetConnectionString("DefaultConnection");
            GenreRepository = new GenreRepository(connectionString);
        }

        [Test]
        public void Create_CreatingGenre_SavingCorrectGenre()
        {
            var genre = new Genre()
            {
                Name = "Genre1"
            };
            GenreRepository.Create(genre);

            var actual = GenreRepository.GetAll().Single();

            Assert.AreEqual(genre.Name, actual.Name);
        }

        [Test]
        public void Update_UpdatingGenre_GenreWasUpdated()
        {
            var genreToUpdate = new Genre()
            {
                Name = "Genre1"
            };
            GenreRepository.Create(genreToUpdate);

            var savedGenre = GenreRepository.GetAll().Single();
            savedGenre.Name = "ChangedName";
            GenreRepository.Update(savedGenre);

            var expected = GenreRepository.GetAll().Single();

            Assert.AreEqual(expected.Name, savedGenre.Name);
            Assert.AreEqual(expected.GenreId, savedGenre.GenreId);
        }

        [Test]
        public void Delete_DeletingGenre_GenreWasDeleted()
        {
            var genreToDelete = new Genre()
            {
                GenreId = 3,
                Name = "New genre"
            };
            GenreRepository.Delete(genreToDelete.GenreId);

            var actual = GenreRepository.GetAll().Where(g => g.GenreId == 3)
                .DefaultIfEmpty().Single();

            Assert.AreEqual(null, actual);
        }

        [Test]
        public void GetById_GettingGenreById_GenreWasFound()
        {
            var genre = new Genre
            {
                Name = "Genre1"
            };
            GenreRepository.Create(genre);

            var expected = GenreRepository.GetAll().FirstOrDefault();
            var actual = GenreRepository.GetById(expected.GenreId);

            Assert.AreEqual(expected.Name, actual.Name);
            Assert.AreEqual(expected.GenreId, actual.GenreId);
        }

        [Test]
        public void GetAll_GettingGenres_GetAllGenres()
        {
            var genre1 = new Genre
            {
                Name = "Genre1"
            };
            GenreRepository.Create(genre1);

            var genre2 = new Genre
            {
                Name = "Genre2"
            };
            GenreRepository.Create(genre2);

            var allGenres = GenreRepository.GetAll().ToList();

            Assert.AreEqual(allGenres.Count, 2);
            Assert.AreEqual(allGenres[0].Name, genre1.Name);
            Assert.AreEqual(allGenres[1].Name, genre2.Name);
        }

        [TearDown]
        public void CleanUp()
        {
            _dbConfiguration.DropTestDatabase();
        }
    }
}