using Microsoft.EntityFrameworkCore;
using MusicCatalog.DataAccess;
using MusicCatalog.DataAccess.Entities;
using MusicCatalog.DataAccess.Repositories.EFRepositories;
using NUnit.Framework;
using System;
using System.Configuration;
using System.Linq;

namespace MusicCatalog.IntegrationTests
{
    [TestFixture]
    public class EFSongTests
    {
        private MusicContext Context { get; set; }

        private EFSongRepository SongRepository { get; set; }

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<MusicContext>()
               .UseInMemoryDatabase(databaseName: "TestDb")
               .Options;

            Context = new MusicContext(options);
            SongRepository = new EFSongRepository(Context);
            InitTestData();
        }

        [Test]
        public void Create_SaveCorrectSong_SongWasSaved()
        {
            // Arrange
            var song = new Song
            {
                SongId = 1,
                Name = "Song1",
                GenreId = 1,
                PerformerId = 1,
                AlbumId = 1
            };

            // Act
            SongRepository.Create(song);
            Context.SaveChanges();

            var actual = Context.Songs.Single();
            var expected = Context.Songs.First();

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Update_UpdateSong_SongWasChanged()
        {
            // Arrange
            var song = new Song
            {
                SongId = 1,
                Name = "Song1",
                GenreId = 1,
                PerformerId = 1,
                AlbumId = 1
            };
            SongRepository.Create(song);
            Context.SaveChanges();

            var songToUpdate = Context.Songs.Single();
            songToUpdate.Name = "ChangedName";

            // Act
            SongRepository.Update(songToUpdate);
            Context.SaveChanges();

            var actual = Context.Songs.Single();
            var expected = Context.Songs.First();

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Update_UpdateNotExistingSong_ThrowsArgumentNullException()
        {
            // Arrange
            var song = Context.Songs.Where(p => p.SongId == 1).FirstOrDefault();

            // Act and assert
            Assert.Throws<ArgumentNullException>(
               () => SongRepository.Update(song),
               $"Song doesn't exist");
        }

        [Test]
        public void Delete_DeleteSong_SongWasDeleted()
        {
            // Arrange
            var song = new Song
            {
                SongId = 1,
                Name = "Song1",
                GenreId = 1,
                PerformerId = 1,
                AlbumId = 1
            };
            SongRepository.Create(song);
            Context.SaveChanges();

            // Act
            SongRepository.Delete(song.SongId);
            Context.SaveChanges();

            var actual = Context.Songs.DefaultIfEmpty();
            var expected = Context.Songs.DefaultIfEmpty();

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Delete_DeleteNotExistingSongId_ThrowsArgumentNullException()
        {
            // Arrange
            var id = 1;

            // Act and assert
            Assert.Throws<ArgumentNullException>(
               () => SongRepository.Delete(id),
               $"Song with id={id} doesn't exist");
        }

        [Test]
        public void GetById_GetSongById_SongWithExpectingId()
        {
            // Arrange
            var song = new Song
            {
                SongId = 1,
                Name = "Song1",
                GenreId = 1,
                PerformerId = 1,
                AlbumId = 1
            };
            SongRepository.Create(song);
            Context.SaveChanges();

            // Act
            var expected = Context.Songs.First();
            var actual = SongRepository.GetById(song.SongId);

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void GetAll_GetSongs_GetSongsList()
        {
            // Arrange
            var song1 = new Song
            {
                SongId = 1,
                Name = "Song1",
                GenreId = 1,
                PerformerId = 1,
                AlbumId = 1
            };
            var song2 = new Song
            {
                SongId = 2,
                Name = "Song2",
                GenreId = 1,
                PerformerId = 1,
                AlbumId = 1
            };
            SongRepository.Create(song1);
            SongRepository.Create(song2);
            Context.SaveChanges();

            // Act
            var expected = Context.Songs.ToList();
            var actual = SongRepository.GetAll();

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TearDown]
        public void CleanUp()
        {
            Context.Database.EnsureDeleted();
            Context.Dispose();
        }

        private void InitTestData()
        {
            var genreRepository = new EFGenreRepository(Context);
            var performerRepository = new EFPerformerRepository(Context);
            var albumRepository = new EFAlbumRepository(Context);

            genreRepository.Create(new Genre() { GenreId = 1, Name = "Test" });
            performerRepository.Create(new Performer() { PerformerId = 1, Name = "Test" });
            albumRepository.Create(new Album() { AlbumId = 1, Name = "Test", ReleaseDate = new DateTime(2020, 1, 1) });
        }
    }
}
