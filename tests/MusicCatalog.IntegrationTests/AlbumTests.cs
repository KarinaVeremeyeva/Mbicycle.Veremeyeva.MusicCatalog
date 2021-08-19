using Microsoft.Extensions.Configuration;
using MusicCatalog.DataAccess.Entities;
using MusicCatalog.DataAccess.Repositories;
using NUnit.Framework;
using System;
using System.Linq;

namespace MusicCatalog.Tests
{
    [TestFixture]
    public class AlbumTests
    {
        private AlbumRepository AlbumRepository { get; set; }

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
            AlbumRepository = new AlbumRepository(connectionString);
        }

        [Test]
        public void Create_CreatingAlbum_SavingCorrectAlbum()
        {
            // Arrange
            var album = new Album
            {
                Name = "Album1",
                ReleaseDate = new DateTime(2021, 1, 1)
            };

            // Act
            AlbumRepository.Create(album);
            var actual = AlbumRepository.GetAll().First();

            // Assert
            Assert.AreEqual(album.Name, actual.Name);
            Assert.AreEqual(album.ReleaseDate, actual.ReleaseDate);
        }

        [Test]
        public void Update_UpdatingAlbum_AlbumWasUpdated()
        {
            var album = new Album
            {
                Name = "Album1",
                ReleaseDate = new DateTime(2020, 1, 1)
            };
            AlbumRepository.Create(album);

            var savedAlbum = AlbumRepository.GetAll().First();
            savedAlbum.Name = "ChangedName";

            // Act
            AlbumRepository.Update(savedAlbum);
            var expected = AlbumRepository.GetAll().First();

            // Assert
            Assert.AreEqual(expected.Name, savedAlbum.Name);
            Assert.AreEqual(expected.ReleaseDate, savedAlbum.ReleaseDate);
        }

        [Test]
        public void Delete_DeletingAlbum_AlbumWasDeleted()
        {
            // Arrange
            var albumToDelete = new Album()
            {
                AlbumId = 1,
                Name = "Album1",
                ReleaseDate = new DateTime(2020, 1, 1)
            };

            // Act
            AlbumRepository.Delete(albumToDelete.AlbumId);

            var actual = AlbumRepository.GetAll().Where(a => a.AlbumId == 1)
                .DefaultIfEmpty().Single();

            // Assert
            Assert.AreEqual(null, actual);
        }

        [Test]
        public void GetById_GettingAlbumById_AlbumWasFound()
        {
            // Arrange
            var album = new Album()
            {
                Name = "Album1",
                ReleaseDate = new DateTime(2020, 1, 1)
            };

            AlbumRepository.Create(album);

            // Act
            var expected = AlbumRepository.GetAll().FirstOrDefault();
            var actual = AlbumRepository.GetById(expected.AlbumId);

            // Assert
            Assert.AreEqual(expected.Name, actual.Name);
            Assert.AreEqual(expected.ReleaseDate, actual.ReleaseDate);
        }

        [Test]
        public void GetAll_GettingAlbums_GetAllAlbums()
        {
            // Arrange
            var album1 = new Album()
            {
                Name = "Album1",
                ReleaseDate = new DateTime(2021, 1, 1)
            };
            var album2 = new Album()
            {
                Name = "Album2",
                ReleaseDate = new DateTime(2020, 1, 1)
            };
            AlbumRepository.Create(album1);
            AlbumRepository.Create(album2);

            // Act
            var allAlbums = AlbumRepository.GetAll().ToList();

            // Assert
            Assert.AreEqual(allAlbums.Count, 2);
            Assert.AreEqual(allAlbums[0].Name, album1.Name);
            Assert.AreEqual(allAlbums[1].Name, album2.Name);
        }

        [TearDown]
        public void CleanUp()
        {
            _dbConfiguration.DropTestDatabase();
        }
    }
}
