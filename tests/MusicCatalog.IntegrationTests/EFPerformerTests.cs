using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System.Linq;
using MusicCatalog.DataAccess;
using MusicCatalog.DataAccess.Entities;
using MusicCatalog.DataAccess.Repositories;
using System;

namespace MusicCatalog.IntegrationTests
{
    [TestFixture]
    public class EFPerformerTests
    {
        private MusicContext Context { get; set; }

        private EFPerformerRepository PerformerRepository { get; set; }

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<MusicContext>()
               .UseInMemoryDatabase(databaseName: "TestDb")
               .Options;

            Context = new MusicContext(options);
            PerformerRepository = new EFPerformerRepository(Context);
        }

        [Test]
        public void Create_SaveCorrectPerformer_PerformerWasSaved()
        {
            // Arrange
            var performer = new Performer
            {
                PerformerId = 1,
                Name = "Performer1",
            };

            // Act
            PerformerRepository.Create(performer);
            Context.SaveChanges();

            var actual = Context.Performers.Single();
            var expected = Context.Performers.First();

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Update_UpdatePerformer_PerformerWasChanged()
        {
            // Arrange
            var performer = new Performer
            {
                PerformerId = 1,
                Name = "Performer1",
            };
            PerformerRepository.Create(performer);
            Context.SaveChanges();

            var performerToUpdate = Context.Performers.Single();
            performerToUpdate.Name = "ChangedName";

            // Act
            PerformerRepository.Update(performerToUpdate);
            Context.SaveChanges();

            var actual = Context.Performers.Single();
            var expected = Context.Performers.First();

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Update_UpdateNotExistingPerformer_ThrowsArgumentNullException()
        {
            // Arrange
            var performer = Context.Performers.Where(p => p.PerformerId == 1).FirstOrDefault();

            // Assert
            Assert.Throws<ArgumentNullException>(
               () => PerformerRepository.Update(performer),
               $"Performer doesn't exist");
        }

        [Test]
        public void Delete_DeletePerformer_PerformerWasDeleted()
        {
            // Arrange
            var performer = new Performer
            {
                PerformerId = 1,
                Name = "Performer1",
            };
            PerformerRepository.Create(performer);
            Context.SaveChanges();

            // Act
            PerformerRepository.Delete(performer.PerformerId);
            Context.SaveChanges();

            var actual = Context.Performers.DefaultIfEmpty();
            var expected = Context.Performers.DefaultIfEmpty();

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Delete_DeleteNotExistingPerformerId_ThrowsArgumentNullException()
        {
            // Arrange
            var id = 1;

            // Assert
            Assert.Throws<ArgumentNullException>(
               () => PerformerRepository.Delete(id),
               $"Performer with id={id} doesn't exist");
        }

        [Test]
        public void GetById_GetPerformerById_PerformerWithExpectingId()
        {
            // Arrange
            var performer = new Performer
            {
                PerformerId = 1,
                Name = "Performer1",
            };
            PerformerRepository.Create(performer);
            Context.SaveChanges();

            // Act
            var expected = Context.Performers.First();
            var actual = PerformerRepository.GetById(performer.PerformerId);

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void GetById_PerformerNotExists_ThrowsArgumentNullException()
        {
            // Arrange
            var id = 1;

            // Assert
            Assert.Throws<ArgumentNullException>(
               () => PerformerRepository.GetById(id),
               $"Performer with id={id} doesn't exist");
        }

        [Test]
        public void GetAll_GetPerformers_GetPerformersList()
        {
            // Arrange
            var performer1 = new Performer
            {
                PerformerId = 1,
                Name = "Performer1",
            };
            var performer2 = new Performer
            {
                PerformerId = 2,
                Name = "Performer2",
            };
            PerformerRepository.Create(performer1);
            PerformerRepository.Create(performer2);

            Context.SaveChanges();

            // Act
            var expected = Context.Performers.ToList();
            var actual = PerformerRepository.GetAll();

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
