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
    public class GenreControllerTests
    {
        [Test]
        public void GetGenres_GetAllGenres_ReturnsAllGenres()
        {
            // Arrange
            var mockService = new Mock<IGenresService>();
            mockService
                .Setup(service => service.GetGenres())
                .Returns(GetTestGenres());
            var controller = new GenresController(mockService.Object);

            // Act
            var result = controller.GetGenres();

            // Assert
            Assert.AreEqual(GetTestGenres().Count(), result.Count());
        }

        [Test]
        public void GetGenre_GetGenreById_ReturnsCorrectGenre()
        {
            // Arrange
            var genre = new GenreDto { GenreId = 1, Name = "TestGenre" };
            var mockService = new Mock<IGenresService>();
            mockService
                .Setup(service => service.GetGenreById(genre.GenreId))
                .Returns(genre);
            var controller = new GenresController(mockService.Object);

            // Act
            var result = controller.GetGenre(genre.GenreId);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(genre, result);
        }

        [Test]
        public void CreateGenre_CreatingGenre_ReturnsOk()
        {
            // Arrange
            var genre = new GenreDto { GenreId = 1, Name = "TestGenre" };
            var mockService = new Mock<IGenresService>();
            mockService
                .Setup(service => service.CreateGenre(genre))
                .Verifiable();
            var controller = new GenresController(mockService.Object);

            // Act
            var result = controller.CreateGenre(genre);

            // Assert
            Assert.IsNotNull(result);
            Assert.That(result, Is.InstanceOf<OkResult>());
        }

        [Test]
        public void CreateGenre_CreatingGenre_ReturnsBadRequest()
        {
            // Arrange
            var genre = new GenreDto { GenreId = 1, Name = "TestGenre" };
            
            var mockService = new Mock<IGenresService>();
            mockService
                .Setup(service => service.CreateGenre(genre))
                .Verifiable();
            var controller = new GenresController(mockService.Object);
            controller.ModelState.AddModelError(string.Empty, "Error");

            // Act
            var result = controller.CreateGenre(genre);

            // Assert
            Assert.IsNotNull(result);
            Assert.That(result, Is.InstanceOf<BadRequestObjectResult>());
        }

        [Test]
        public void UpdateGenre_UpdatingGenre_ReturnsOk()
        {
            // Arrange
            var genre = new GenreDto { GenreId = 1, Name = "TestGenre" };
            var mockService = new Mock<IGenresService>();
            mockService
                .Setup(service => service.UpdateGenre(genre))
                .Verifiable();
            var controller = new GenresController(mockService.Object);

            // Act
            var result = controller.UpdateGenre(genre);

            // Assert
            Assert.IsNotNull(result);
            Assert.That(result, Is.InstanceOf<OkResult>());
        }

        [Test]
        public void UpdateGenre_UpdatingWrongGenre_ReturnsBadRequest()
        {
            // Arrange
            var genre = new GenreDto { GenreId = 1, Name = "TestGenre" };
            var mockService = new Mock<IGenresService>();
            mockService
                .Setup(service => service.UpdateGenre(genre))
                .Verifiable();
            var controller = new GenresController(mockService.Object);
            controller.ModelState.AddModelError(string.Empty, "Error");

            // Act
            var result = controller.UpdateGenre(genre);

            // Assert
            Assert.IsNotNull(result);
            Assert.That(result, Is.InstanceOf<BadRequestObjectResult>());
        }

        [Test]
        public void DeleteGenre_DeletingGenre_ReturnsOk()
        {
            // Arrange
            var genre = new GenreDto { GenreId = 1, Name = "TestGenre" };
            var mockService = new Mock<IGenresService>();
            mockService
                .Setup(service => service.GetGenreById(genre.GenreId))
                .Returns(genre);
            var controller = new GenresController(mockService.Object);

            // Act
            var result = controller.DeleteGenre(genre.GenreId);

            // Assert
            Assert.IsNotNull(result);
            Assert.That(result, Is.InstanceOf<OkResult>());
        }

        [Test]
        public void DeleteGenre_DeleteNotExistingGenre_ReturnsBadRequest()
        {
            // Arrange
            var genre = new GenreDto { GenreId = 1, Name = "TestGenre" };
            var mockService = new Mock<IGenresService>();
            mockService
                .Setup(service => service.GetGenreById(genre.GenreId))
                .Returns((GenreDto)null);
            var controller = new GenresController(mockService.Object);

            // Act
            var result = controller.DeleteGenre(genre.GenreId);

            // Assert
            Assert.IsNotNull(result);
            Assert.That(result, Is.InstanceOf<BadRequestResult>());
        }

        private IEnumerable<GenreDto> GetTestGenres()
        {
            var genres = new List<GenreDto>
            {
                new GenreDto { GenreId = 1, Name = "TestGenre1" },
                new GenreDto { GenreId = 2, Name = "TestGenre2" },
                new GenreDto { GenreId = 3, Name = "TestGenre3" },
            };

            return genres;
        }
    }
}
