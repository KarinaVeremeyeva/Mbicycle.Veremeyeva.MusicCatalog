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
    public class EFAlbumTests
    {
        private MusicContext Context { get; set; }

        private EFAlbumRepository AlbumRepository { get; set; }

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<MusicContext>()
               .UseInMemoryDatabase(databaseName: "TestDb")
               .Options;

            Context = new MusicContext(options);
            AlbumRepository = new EFAlbumRepository(Context);
        }

        [Test]
        public void Create_SaveCorrectAlbum_AlbumWasSaved()
        {
            // Arrange
            var album = new Album
            {
                AlbumId = 1,
                Name = "Album1",
                ReleaseDate = new DateTime(2020, 1, 1)
            };

            // Act
            AlbumRepository.Create(album);
            Context.SaveChanges();

            var actual = Context.Albums.Single();
            var expected = Context.Albums.First();

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Update_UpdateAlbum_AlbumWasChanged()
        {
            // Arrange
            var album = new Album
            {
                AlbumId = 1,
                Name = "Album1",
                ReleaseDate = new DateTime(2020, 1, 1)
            };
            AlbumRepository.Create(album);
            Context.SaveChanges();

            var albumToUpdate = Context.Albums.Single();
            albumToUpdate.Name = "ChangedName";

            // Act
            AlbumRepository.Update(albumToUpdate);
            Context.SaveChanges();

            var actual = Context.Albums.Single();
            var expected = Context.Albums.First();

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Update_UpdateNotExistingAlbum_ThrowsArgumentNullException()
        {
            // Arrange
            var album = Context.Albums.Where(p => p.AlbumId == 1).FirstOrDefault();

            // Act and assert
            Assert.Throws<ArgumentNullException>(
               () => AlbumRepository.Update(album),
               $"Album doesn't exist");
        }

        [Test]
        public void Delete_DeleteAlbum_AlbumWasDeleted()
        {
            // Arrange
            var album = new Album
            {
                AlbumId = 1,
                Name = "Album1",
                ReleaseDate = new DateTime(2020, 1, 1)
            };
            AlbumRepository.Create(album);
            Context.SaveChanges();

            // Act
            AlbumRepository.Delete(album.AlbumId);
            Context.SaveChanges();

            var actual = Context.Albums.DefaultIfEmpty();
            var expected = Context.Albums.DefaultIfEmpty();

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Delete_DeleteNotExistingAlbumId_ThrowsArgumentNullException()
        {
            // Arrange
            var id = 1;

            // Act and assert
            Assert.Throws<ArgumentNullException>(
               () => AlbumRepository.Delete(id),
               $"Album with id={id} doesn't exist");
        }

        [Test]
        public void GetById_GetAlbumById_AlbumWithExpectingId()
        {
            // Arrange
            var album = new Album
            {
                AlbumId = 1,
                Name = "Album1",
                ReleaseDate = new DateTime(2020, 1, 1)
            };
            AlbumRepository.Create(album);
            Context.SaveChanges();

            // Act
            var expected = Context.Albums.First();
            var actual = AlbumRepository.GetById(album.AlbumId);

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void GetById_AlbumNotExists_ThrowsArgumentNullException()
        {
            // Arrange
            var id = 1;

            // Act and sssert
            Assert.Throws<ArgumentNullException>(
               () => AlbumRepository.GetById(id),
               $"Album with id={id} doesn't exist");
        }

        [Test]
        public void GetAll_GetAlbums_GetAlbumsList()
        {
            // Arrange
            var album1 = new Album
            {
                AlbumId = 1,
                Name = "Album1",
                ReleaseDate = new DateTime(2020, 1, 1)
            };
            var album2 = new Album
            {
                AlbumId = 2,
                Name = "Album2",
                ReleaseDate = new DateTime(2021, 1, 1)
            };
            AlbumRepository.Create(album1);
            AlbumRepository.Create(album2);
            Context.SaveChanges();

            // Act
            var expected = Context.Albums.ToList();
            var actual = AlbumRepository.GetAll();

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
