using Microsoft.AspNetCore.Mvc;
using Moq;
using MusicCatalog.BusinessLogic.Interfaces;
using MusicCatalog.BusinessLogic.Models;
using MusicCatalog.WebApi.Controllers;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace MusicCatalog.UnitTests
{
    [TestFixture]
    public class PerformerControllerTests
    {
        [Test]
        public void GetPerformers_GetAllPerformers_ReturnsAllPerformers()
        {
            // Arrange
            var mockService = new Mock<IPerformersService>();
            mockService
                .Setup(service => service.GetPerformers())
                .Returns(GetTestPerformers());
            var controller = new PerformersController(mockService.Object);

            // Act
            var result = controller.GetPerformers();

            // Assert
            Assert.AreEqual(GetTestPerformers().Count(), result.Count());
        }

        [Test]
        public void GetPerformer_GetPerformerById_ReturnsCorrectPerformer()
        {
            // Arrange
            var performer = new PerformerDto { PerformerId = 1, Name = "TestPerformer" };
            var mockService = new Mock<IPerformersService>();
            mockService
                .Setup(service => service.GetPerformerById(performer.PerformerId))
                .Returns(performer);
            var controller = new PerformersController(mockService.Object);

            // Act
            var result = controller.GetPerformer(performer.PerformerId);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(performer, result);
        }

        [Test]
        public void CreatePerformer_CreatingPerformer_ReturnsOk()
        {
            // Arrange
            var performer = new PerformerDto { PerformerId = 1, Name = "TestPerformer" };
            var mockService = new Mock<IPerformersService>();
            mockService
                .Setup(service => service.CreatePerformer(performer))
                .Verifiable();
            var controller = new PerformersController(mockService.Object);

            // Act
            var result = controller.CreatePerformer(performer);

            // Assert
            Assert.IsNotNull(result);
            Assert.That(result, Is.InstanceOf<OkResult>());
        }

        [Test]
        public void CreatePerformer_CreatingPerformer_ReturnsBadRequest()
        {
            // Arrange
            var performer = new PerformerDto { PerformerId = 1, Name = "TestPerformer" };
            var mockService = new Mock<IPerformersService>();
            mockService
                .Setup(service => service.CreatePerformer(performer))
                .Verifiable();
            var controller = new PerformersController(mockService.Object);
            controller.ModelState.AddModelError(string.Empty, "Error");

            // Act
            var result = controller.CreatePerformer(performer);

            // Assert
            Assert.IsNotNull(result);
            Assert.That(result, Is.InstanceOf<BadRequestObjectResult>());
        }

        [Test]
        public void UpdatePerformer_UpdatingPerformer_ReturnsOk()
        {
            // Arrange
            var performer = new PerformerDto { PerformerId = 1, Name = "TestPerformer" };
            var mockService = new Mock<IPerformersService>();
            mockService
                .Setup(service => service.UpdatePerformer(performer))
                .Verifiable();
            var controller = new PerformersController(mockService.Object);

            // Act
            var result = controller.UpdatePerformer(performer);

            // Assert
            Assert.IsNotNull(result);
            Assert.That(result, Is.InstanceOf<OkResult>());
        }

        [Test]
        public void UpdateGenre_UpdatingWrongGenre_ReturnsBadRequest()
        {
            // Arrange
            var performer = new PerformerDto { PerformerId = 1, Name = "TestPerformer" };
            var mockService = new Mock<IPerformersService>();
            mockService
                .Setup(service => service.UpdatePerformer(performer))
                .Verifiable();
            var controller = new PerformersController(mockService.Object);
            controller.ModelState.AddModelError(string.Empty, "Error");

            // Act
            var result = controller.UpdatePerformer(performer);

            // Assert
            Assert.IsNotNull(result);
            Assert.That(result, Is.InstanceOf<BadRequestObjectResult>());
        }

        [Test]
        public void DeleteGenre_DeletingGenre_ReturnsOk()
        {
            // Arrange
            var performer = new PerformerDto { PerformerId = 1, Name = "TestPerformer" };
            var mockService = new Mock<IPerformersService>();
            mockService
                .Setup(service => service.GetPerformerById(performer.PerformerId))
                .Returns(performer);
            var controller = new PerformersController(mockService.Object);

            // Act
            var result = controller.DeletePerformer(performer.PerformerId);

            // Assert
            Assert.IsNotNull(result);
            Assert.That(result, Is.InstanceOf<OkResult>());
        }

        [Test]
        public void DeleteGenre_DeleteNotExistingGenre_ReturnsBadRequest()
        {
            // Arrange
            var performer = new PerformerDto { PerformerId = 1, Name = "TestPerformer" };
            var mockService = new Mock<IPerformersService>();
            mockService
                .Setup(service => service.GetPerformerById(performer.PerformerId))
                .Returns((PerformerDto)null);
            var controller = new PerformersController(mockService.Object);

            // Act
            var result = controller.DeletePerformer(performer.PerformerId);

            // Assert
            Assert.IsNotNull(result);
            Assert.That(result, Is.InstanceOf<BadRequestResult>());
        }

        private List<PerformerDto> GetTestPerformers()
        {
            var performers = new List<PerformerDto>
            {
                new PerformerDto { PerformerId = 1, Name = "TestPerformer1" },
                new PerformerDto { PerformerId = 2, Name = "TestPerformer2" },
                new PerformerDto { PerformerId = 3, Name = "TestPerformer3" },
            };

            return performers;
        }
    }
}
