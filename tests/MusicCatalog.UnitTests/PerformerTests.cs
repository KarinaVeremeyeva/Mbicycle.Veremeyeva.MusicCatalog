using NUnit.Framework;
using Microsoft.EntityFrameworkCore;
using MusicCatalog.Infrastructure;
using MusicCatalog.Domain;
using System.Linq;

namespace MusicCatalog.UnitTests
{
    [TestFixture]
    public class Tests
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

        [TearDown]
        public void Dispose()
        {
            Context.Dispose();
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
        public void Delete_DeletPerformer_PerformerIsDeleted()
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
    }
}