using NUnit.Framework;
using Microsoft.EntityFrameworkCore;
using MusicCatalog.Infrastructure;
using MusicCatalog.Domain;
using System.Linq;

namespace MusicCatalog.UnitTests
{
    [TestFixture]
    public class PerformerTests
    {
        private MusicContext Context { get; set; }

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<MusicContext>()
               .UseInMemoryDatabase(databaseName: "TestDb")
               .Options;

            Context = new MusicContext(options);
        }

        [Test]
        public void Create_SaveCorrectPerformer_PerformerIsSaved()
        {
            var unitOfWork = new UnitOfWork(Context);
            var performer = new Performer
            {
                PerformerId = 1,
                Name = "TestName",
            };

            unitOfWork.Performers.Create(performer);
            Context.SaveChanges();

            var actual = Context.Performers.Single();
            var expected = Context.Performers.First();

            Assert.AreEqual(expected, actual);
        }
    
        [Test]
        public void Delete_DeletePerformer_PerformerIsDeleted()
        {
            var unitOfWork = new UnitOfWork(Context);
            var performer = new Performer
            {
                PerformerId = 1,
                Name = "TestName",
            };

            unitOfWork.Performers.Delete(performer.PerformerId);
            Context.SaveChanges();

            var actual = Context.Performers.DefaultIfEmpty();
            var expected = Context.Performers.DefaultIfEmpty();

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Update_UpdatePerformerName_PerformerNameWasChanged()
        {
            var unitOfWork = new UnitOfWork(Context);
            var performer = new Performer
            {
                PerformerId = 1,
                Name = "TestName",
            };

            unitOfWork.Performers.Create(performer);
            Context.SaveChanges();

            var performerToUpdate = Context.Performers.Single();
            var newName = performerToUpdate.Name = "NewName";

            unitOfWork.Performers.Update(performerToUpdate);
            Context.SaveChanges();

            var actual = newName;
            var expected = "NewName";

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void GetById_GetPerformerById_PerformerWithExpecingId()
        {
            var unitOfWork = new UnitOfWork(Context);
            var performer = new Performer
            {
                PerformerId = 1,
                Name = "TestName",
            };

            unitOfWork.Performers.Create(performer);
            Context.SaveChanges();

            var actual = unitOfWork.Performers.GetById(performer.PerformerId);
            var expected = Context.Performers.First();

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void GetAll_GetPerformers_GetPerformersList()
        {
            var unitOfWork = new UnitOfWork(Context);
            var performer = new Performer
            {
                PerformerId = 1,
                Name = "TestName",
            };

            unitOfWork.Performers.Create(performer);
            Context.SaveChanges();

            var actual = unitOfWork.Performers.GetAll();
            var expected = Context.Performers.ToList();

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