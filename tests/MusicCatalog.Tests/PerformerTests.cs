using Microsoft.Extensions.Configuration;
using MusicCatalog.DataAccess.Entities;
using MusicCatalog.DataAccess.Repositories;
using NUnit.Framework;
using System.Linq;

namespace MusicCatalog.Tests
{
    [TestFixture]
    public class PerformerTests
    {
        private PerformerRepository PerformerRepository { get; set; }

        private IConfiguration Configuration { get; set; }

        private DatabaseConfiguration dbConfiguration;

        [SetUp]
        public void SetUp()
        {
            Configuration = new ConfigurationBuilder()
                .AddJsonFile(path: "appsettings.json")
                .Build();

            dbConfiguration = new DatabaseConfiguration(Configuration);
            dbConfiguration.DeployTestDatabase();

            var connectionString = Configuration.GetConnectionString("DefaultConnection");
            PerformerRepository = new PerformerRepository(connectionString);
        }

        [Test]
        public void Create_CreatingPerformer_SavingCorrectPerformer()
        {
            var performer = new Performer()
            {
                Name = "Performer1"
            };
            PerformerRepository.Create(performer);

            var actual = PerformerRepository.GetAll().Single();

            Assert.AreEqual(performer.Name, actual.Name);
        }

        [Test]
        public void Update_UpdatingPerformer_PerformerWasUpdated()
        {
            var performer = new Performer()
            {
                Name = "Performer1"
            };
            PerformerRepository.Create(performer);

            var savedPerformer = PerformerRepository.GetAll().Single();
            savedPerformer.Name = "ChangedName";
            PerformerRepository.Update(savedPerformer);

            var expected = PerformerRepository.GetAll().Single();

            Assert.AreEqual(expected.Name, savedPerformer.Name);
            Assert.AreEqual(expected.PerformerId, savedPerformer.PerformerId);
        }

        [Test]
        public void Delete_DeletingPerformer_PerformerWasDeleted()
        {
            var performer = new Performer()
            {
                PerformerId = 2,
                Name = "Performer2"
            };
            PerformerRepository.Delete(performer.PerformerId);

            var actual = PerformerRepository.GetAll().Where(g => g.PerformerId == 2)
                .DefaultIfEmpty().Single();

            Assert.AreEqual(null, actual);
        }

        [Test]
        public void GetById_GettingPerformerById_PerformerWasFound()
        {
            var performer = new Performer()
            {
                Name = "Performer1"
            };
            PerformerRepository.Create(performer);

            var expected = PerformerRepository.GetAll().FirstOrDefault();
            var actual = PerformerRepository.GetById(expected.PerformerId);

            Assert.AreEqual(expected.Name, actual.Name);
            Assert.AreEqual(expected.PerformerId, actual.PerformerId);
        }

        [Test]
        public void GetAll_GettingPerformers_GetAllPerformers()
        {
            var performer1 = new Performer()
            {
                Name = "Performer1"
            };
            PerformerRepository.Create(performer1);

            var performer2 = new Performer
            {
                Name = "Performer2"
            };
            PerformerRepository.Create(performer2);

            var performers = PerformerRepository.GetAll().ToList();

            Assert.AreEqual(performers.Count, 2);
            Assert.AreEqual(performers[0].Name, performer1.Name);
            Assert.AreEqual(performers[1].Name, performer2.Name);
        }

        [TearDown]
        public void CleanUp()
        {
            dbConfiguration.DropTestDatabase();
        }
    }
}