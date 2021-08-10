using MusicCatalog.DataAccess.Entities;
using MusicCatalog.DataAccess.Repositories;
using NUnit.Framework;
using System.Data.SqlClient;
using System.Linq;

namespace MusicCatalog.Tests
{
    [TestFixture]
    public class PerformerTests
    {
        private const string conncetionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=TestMusicCatalog;Integrated Security=True";

        [SetUp]
        public void SetUp()
        {
            string sqlExpression = $"DELETE FROM Performers";
            using (var connection = new SqlConnection(conncetionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                command.ExecuteNonQuery();
            }
        }

        [Test]
        public void Create_CreatingPerformer_SavingCorrectPerformer()
        {
            PerformerRepository performerRepository = new PerformerRepository(conncetionString);
            var performer = new Performer()
            {
                Name = "Performer1"
            };
            performerRepository.Create(performer);

            var actual = performerRepository.GetAll().Single();

            Assert.AreEqual(performer.Name, actual.Name);
        }

        [Test]
        public void Update_UpdatingPerformer_PerformerWasUpdated()
        {
            PerformerRepository performerRepository = new PerformerRepository(conncetionString);
            var performer = new Performer()
            {
                Name = "Performer1"
            };
            performerRepository.Create(performer);

            var savedPerformer = performerRepository.GetAll().Single();
            savedPerformer.Name = "ChangedName";
            performerRepository.Update(savedPerformer);

            var expected = performerRepository.GetAll().Single();

            Assert.AreEqual(expected.Name, savedPerformer.Name);
            Assert.AreEqual(expected.PerformerId, savedPerformer.PerformerId);
        }

        [Test]
        public void Delete_DeletingPerformer_PerformerWasDeleted()
        {
            PerformerRepository performerRepository = new PerformerRepository(conncetionString);
            var performer = new Performer()
            {
                PerformerId = 2,
                Name = "Performer2"
            };
            performerRepository.Delete(performer.PerformerId);

            var actual = performerRepository.GetAll().Where(g => g.PerformerId == 2)
                .DefaultIfEmpty().Single();

            Assert.AreEqual(null, actual);
        }

        [Test]
        public void GetById_GettingPerformerById_PerformerWasFound()
        {
            PerformerRepository performerRepository = new PerformerRepository(conncetionString);
            var performer = new Performer()
            {
                Name = "Performer1"
            };
            performerRepository.Create(performer);

            var expected = performerRepository.GetAll().FirstOrDefault();
            var actual = performerRepository.GetById(expected.PerformerId);

            Assert.AreEqual(expected.Name, actual.Name);
            Assert.AreEqual(expected.PerformerId, actual.PerformerId);
        }

        [Test]
        public void GetAll_GettingPerformers_GetAllPerformers()
        {
            PerformerRepository performerRepository = new PerformerRepository(conncetionString);
            var performer1 = new Performer()
            {
                Name = "Performer1"
            };
            performerRepository.Create(performer1);

            var performer2 = new Performer
            {
                Name = "Performer2"
            };
            performerRepository.Create(performer2);

            var performers = performerRepository.GetAll().ToList();

            Assert.AreEqual(performers.Count, 2);
            Assert.AreEqual(performers[0].Name, performer1.Name);
            Assert.AreEqual(performers[1].Name, performer2.Name);
        }
    }
}