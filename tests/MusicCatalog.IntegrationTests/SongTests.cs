using Microsoft.Extensions.Configuration;
using MusicCatalog.DataAccess.Entities;
using MusicCatalog.DataAccess.Repositories;
using NUnit.Framework;
using System;
using System.Linq;

namespace MusicCatalog.IntegrationTests
{
    [TestFixture]
    public class SongTests
    {
        private SongRepository SongRepository { get; set; }

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
            SongRepository = new SongRepository(connectionString);
            InitTestData();
        }

        [Test]
        public void Create_CreatingSong_SavingCorrectSong()
        {
            // Arrange
            var song = new Song
            {
                Name = "Song1",
                GenreId = 1,
                PerformerId = 1,
                AlbumId = 1
            };

            // Act
            SongRepository.Create(song);
            var actual = SongRepository.GetAll().First();

            // Assert
            Assert.AreEqual(song.Name, actual.Name);
            Assert.AreEqual(song.GenreId, actual.GenreId);
            Assert.AreEqual(song.PerformerId, actual.PerformerId);
            Assert.AreEqual(song.AlbumId, actual.AlbumId);
        }

        [Test]
        public void Update_UpdatingSong_SongWasUpdated()
        {
            // Arrange
            var songToUpdate = new Song()
            {
                Name = "Song1",
                GenreId = 1,
                PerformerId = 1,
                AlbumId = 1
            };
            SongRepository.Create(songToUpdate);

            var savedSong = SongRepository.GetAll().First();
            savedSong.Name = "ChangedName";

            // Act
            SongRepository.Update(savedSong);
            var expected = SongRepository.GetAll().First();

            // Assert
            Assert.AreEqual(expected.Name, savedSong.Name);
            Assert.AreEqual(expected.GenreId, savedSong.GenreId);
            Assert.AreEqual(expected.PerformerId, savedSong.PerformerId);
            Assert.AreEqual(expected.AlbumId, savedSong.AlbumId);
        }

        [Test]
        public void Delete_DeletingSong_SongWasDeleted()
        {
            // Arrange
            var songToDelete = new Song()
            {
                SongId = 1,
                Name = "Song1",
                GenreId = 1,
                PerformerId = 1,
                AlbumId = 1
            };

            // Act
            SongRepository.Delete(songToDelete.SongId);

            var actual = SongRepository.GetAll().Where(s => s.SongId == 1)
                .DefaultIfEmpty().Single();

            // Assert
            Assert.AreEqual(null, actual);
        }

        [Test]
        public void GetById_GettingSongById_SongWasFound()
        {
            // Arrange
            var song = new Song()
            {
                Name = "Song1",
                GenreId = 1,
                PerformerId = 1,
                AlbumId = 1
            };
            SongRepository.Create(song);

            // Act
            var expected = SongRepository.GetAll().FirstOrDefault();
            var actual = SongRepository.GetById(expected.SongId);

            // Assert
            Assert.AreEqual(expected.Name, actual.Name);
            Assert.AreEqual(expected.GenreId, actual.GenreId);
            Assert.AreEqual(expected.PerformerId, actual.PerformerId);
            Assert.AreEqual(expected.AlbumId, actual.AlbumId);
        }

        [Test]
        public void GetAll_GettingSongs_GetAllSongs()
        {
            // Arrange
            var song1 = new Song()
            {
                Name = "Song1",
                GenreId = 1,
                PerformerId = 1,
                AlbumId = 1
            };
            var song2 = new Song()
            {
                Name = "Song2",
                GenreId = 1,
                PerformerId = 1,
                AlbumId = 1
            };
            SongRepository.Create(song1);
            SongRepository.Create(song2);

            // Act
            var allSongs = SongRepository.GetAll().ToList();

            // Assert
            Assert.AreEqual(allSongs.Count, 2);
            Assert.AreEqual(allSongs[0].Name, song1.Name);
            Assert.AreEqual(allSongs[1].Name, song2.Name);
        }

        [TearDown]
        public void CleanUp()
        {
            ClearTestData();
            _dbConfiguration.DropTestDatabase();
        }

        private void InitTestData()
        {
            var connectionString = Configuration.GetConnectionString("DefaultConnection");
            var genreRepository = new GenreRepository(connectionString);
            var performerRepository = new PerformerRepository(connectionString);
            var albumRepository = new AlbumRepository(connectionString);

            genreRepository.Create(new Genre() { GenreId = 1, Name = "Test" });
            performerRepository.Create(new Performer() { PerformerId = 1, Name = "Test" });
            albumRepository.Create(new Album() { AlbumId = 1, Name = "Test", ReleaseDate = new DateTime(2020, 1, 1) });
        }

        private void ClearTestData()
        {
            var connectionString = Configuration.GetConnectionString("DefaultConnection");
            var genreRepository = new GenreRepository(connectionString);
            var performerRepository = new PerformerRepository(connectionString);
            var albumRepository = new AlbumRepository(connectionString);

            genreRepository.Delete(1);
            performerRepository.Delete(1);
            albumRepository.Delete(1);
        }
    }
}
