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
    public class GenresControllerTests
    {
        [Test]
        public void Index_GetAllGenres_ReturnsAViewResultWithAListOfGenres()
        {
            // Arrange
            var mockService = new Mock<IGenresService>();
            mockService.Setup(service => service.GetGenres()).Returns(GetTestGenres());
            var controller = new GenresController(mockService.Object);

            // Act
            var result = controller.Index() as ViewResult;

            // Assert
            Assert.That(result, Is.InstanceOf<ViewResult>());
        }

        [Test]
        public void Create_CreateGenre_ReturnsARedirectToIndex()
        {
            // Arrange
            var genre = new Genre { Name = "TestGenre" };
            var mockService = new Mock<IGenresService>();
            mockService.Setup(service => service.CreateGenre(genre)).Verifiable();
            var controller = new GenresController(mockService.Object);

            // Act
            var result = controller.Create(genre);

            // Assert
            Assert.IsNotNull(result);
            Assert.That(result, Is.InstanceOf<RedirectToActionResult>());
        }

        [Test]
        public void Create_CreateGenre_ReturnsAViewResult()
        {
            // Arrange
            var genre = new Genre { Name = "TestGenre" };
            var mockService = new Mock<IGenresService>();
            mockService.Setup(service => service.CreateGenre(genre)).Verifiable();
            var controller = new GenresController(mockService.Object);

            // Act
            var result = controller.Create();

            // Assert
            Assert.IsNotNull(result);
            Assert.That(result, Is.InstanceOf<ViewResult>());
        }

        [Test]
        public void Create_CreateGenre_ReturnsAViewResultWithGenreModel()
        {
            // Arrange
            var mockService = new Mock<IGenresService>();
            var controller = new GenresController(mockService.Object);
            controller.ModelState.AddModelError("Name", "Required");
            var genre = new Genre();

            // Act
            var result = controller.Create(genre) as ViewResult;

            // Assert
            Assert.AreEqual(genre, result?.Model);
        }

        [Test]
        public void Edit_EditNotExistingGenre_ReturnsNotFound()
        {
            // Arrange
            var genreId = 100;
            var mockService = new Mock<IGenresService>();
            mockService.Setup(service => service.GetGenreById(genreId)).Returns((Genre)null);
            var controller = new GenresController(mockService.Object);

            // Act
            var result = controller.Edit(genreId);

            // Assert
            Assert.That(result, Is.InstanceOf<NotFoundResult>());
        }

        [Test]
        public void Edit_EditGenre_ReturnsARedirectToIndex()
        {
            // Arrange
            var genre = new Genre { GenreId = 1, Name = "TestGenre" };
            var mockService = new Mock<IGenresService>();
            mockService.Setup(service => service.UpdateGenre(genre)).Verifiable();
            var controller = new GenresController(mockService.Object);

            // Act
            var result = controller.Edit(genre);

            // Assert
            Assert.That(result, Is.InstanceOf<RedirectToActionResult>());
        }

        [Test]
        public void Delete_DeleteNotExistingGenre_ReturnsNotFound()
        {
            // Arrange
            var genreId = 100;
            var mockService = new Mock<IGenresService>();
            mockService.Setup(service => service.GetGenreById(genreId)).Returns((Genre)null);
            var controller = new GenresController(mockService.Object);

            // Act
            var result = controller.Delete(genreId);

            // Assert
            Assert.IsNotNull(result);
            Assert.That(result, Is.InstanceOf<NotFoundResult>());
        }

        [Test]
        public void Delete_DeleteGenre_ReturnsAViewResult()
        {
            // Arrange
            var genre = new Genre { GenreId = 100, Name = "TestGenre" };
            var mockService = new Mock<IGenresService>();
            mockService.Setup(service => service.GetGenreById(genre.GenreId)).Returns(genre);
            var controller = new GenresController(mockService.Object);

            // Act
            var result = controller.Delete(genre.GenreId);

            // Assert
            Assert.IsNotNull(result);
            Assert.That(result, Is.InstanceOf<ViewResult>());
        }

        private List<Genre> GetTestGenres()
        {
            var genres = new List<Genre>
            {
                new Genre { GenreId = 1, Name="TestGenre1" },
                new Genre { GenreId = 2, Name="TestGenre2" },
                new Genre { GenreId = 3, Name="TestGenre3" },
            };

            return genres;
        }
    }
}