using Microsoft.AspNetCore.Mvc;
using Moq;
using MusicCatalog.DataAccess.Entities;
using MusicCatalog.Services;
using MusicCatalog.Web.Controllers;
using NUnit.Framework;
using System.Collections.Generic;

namespace MusicCatalog.UnitTests
{
    [TestFixture]
    public class PerformersControllerTests
    {
        [Test]
        public void Index_GetAllPerformers_ReturnsAViewResultWithAListOfPerformers()
        {
            // Arrange
            var mockService = new Mock<IPerformersService>();
            mockService.Setup(service => service.GetPerformers()).Returns(GetTestPerformers());
            var controller = new PerformersController(mockService.Object);

            // Act
            var result = controller.Index() as ViewResult;

            // Assert
            Assert.That(result, Is.InstanceOf<ViewResult>());
        }

        [Test]
        public void Create_CreatePerformer_ReturnsARedirectToIndex()
        {
            // Arrange
            var performer = new Performer { Name = "TestPerformer" };
            var mockService = new Mock<IPerformersService>();
            mockService.Setup(service => service.CreatePerformer(performer)).Verifiable();
            var controller = new PerformersController(mockService.Object);

            // Act
            var result = controller.Create(performer);

            // Assert
            Assert.IsNotNull(result);
            Assert.That(result, Is.InstanceOf<RedirectToActionResult>());
        }

        [Test]
        public void Create_CreatePerformer_ReturnsAViewResult()
        {
            // Arrange
            var performer = new Performer { Name = "TestPerformer" };
            var mockService = new Mock<IPerformersService>();
            mockService.Setup(service => service.CreatePerformer(performer)).Verifiable();
            var controller = new PerformersController(mockService.Object);

            // Act
            var result = controller.Create() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.That(result, Is.InstanceOf<ViewResult>());
        }

        [Test]
        public void Create_CreatePerformer_ReturnsAViewResultWithPerformerModel()
        {
            // Arrange
            var mockService = new Mock<IPerformersService>();
            var controller = new PerformersController(mockService.Object);
            controller.ModelState.AddModelError("Name", "Required");
            var performer = new Performer();

            // Act
            var result = controller.Create(performer) as ViewResult;

            // Assert
            Assert.AreEqual(performer, result?.Model);
        }

        [Test]
        public void Edit_EditNotExistingPerformer_ReturnsNotFound()
        {
            // Arrange
            var performerId = 100;
            var mockService = new Mock<IPerformersService>();
            mockService.Setup(service => service.GetPerformerById(performerId)).Returns((Performer)null);
            var controller = new PerformersController(mockService.Object);

            // Act
            var result = controller.Edit(performerId);

            // Assert
            Assert.That(result, Is.InstanceOf<NotFoundResult>());
        }

        [Test]
        public void Edit_EditPerformer_ReturnsARedirectToIndex()
        {
            // Arrange
            var performer = new Performer { PerformerId = 1, Name = "TestPerformer" };
            var mockService = new Mock<IPerformersService>();
            mockService.Setup(service => service.UpdatePerformer(performer)).Verifiable();
            var controller = new PerformersController(mockService.Object);

            // Act
            var result = controller.Edit(performer);

            // Assert
            Assert.That(result, Is.InstanceOf<RedirectToActionResult>());
        }

        [Test]
        public void Delete_DeleteNotExistingPerformer_ReturnsNotFound()
        {
            // Arrange
            var performerId = 100;
            var mockService = new Mock<IPerformersService>();
            mockService.Setup(service => service.GetPerformerById(performerId)).Returns((Performer)null);
            var controller = new PerformersController(mockService.Object);

            // Act
            var result = controller.Delete(performerId);

            // Assert
            Assert.IsNotNull(result);
            Assert.That(result, Is.InstanceOf<NotFoundResult>());
        }

        [Test]
        public void Delete_DeletePerformer_ReturnsAViewResult()
        {
            // Arrange
            var performer = new Performer { PerformerId = 1, Name = "TestPerformer" };
            var mockService = new Mock<IPerformersService>();
            mockService.Setup(service => service.GetPerformerById(performer.PerformerId)).Returns(performer);
            var controller = new PerformersController(mockService.Object);

            // Act
            var result = controller.Delete(performer.PerformerId);

            // Assert
            Assert.IsNotNull(result);
            Assert.That(result, Is.InstanceOf<ViewResult>());
        }

        private List<Performer> GetTestPerformers()
        {
            var genres = new List<Performer>
            {
                new Performer { PerformerId = 1, Name="TestPerformer1" },
                new Performer { PerformerId = 2, Name="TestPerformer2" },
                new Performer { PerformerId = 3, Name="TestPerformer3" },
            };

            return genres;
        }
    }
}