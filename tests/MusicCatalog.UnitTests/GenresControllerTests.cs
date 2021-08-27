using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Moq;
using MusicCatalog.BusinessLogic.Interfaces;
using MusicCatalog.BusinessLogic.Models;
using MusicCatalog.Web.Controllers;
using MusicCatalog.Web.ViewModels;
using NUnit.Framework;
using System.Collections.Generic;

namespace MusicCatalog.UnitTests
{
    [TestFixture]
    public class GenresControllerTests
    {
        private static IMapper _mapper;

        [Test]
        public void AutoMapper_Configuration_IsValid()
        {
            var config = new MapperConfiguration(cfg => cfg.AddProfile<TestProfile>());
            config.AssertConfigurationIsValid();
        }

        [SetUp]
        public void SetUp()
        {
            if (_mapper == null)
            {
                var mappingConfig = new MapperConfiguration(mc =>
                {
                    mc.AddProfile(new TestProfile());
                });
                IMapper mapper = mappingConfig.CreateMapper();
                _mapper = mapper;
            }
        }

        [Test]
        public void Index_GetAllGenres_ReturnsAViewResultWithAListOfGenres()
        {
            // Arrange
            var mockService = new Mock<IGenresService>();
            mockService.Setup(service => service.GetGenres()).Returns(GetTestGenres());
            var controller = new GenresController(mockService.Object, _mapper);

            // Act
            var result = controller.Index() as ViewResult;

            // Assert
            Assert.That(result, Is.InstanceOf<ViewResult>());
        }

        [Test]
        public void Create_CreateGenre_ReturnsARedirectToIndex()
        {
            // Arrange
            var genre = new GenreDto { Name = "TestGenre" };
            var mockService = new Mock<IGenresService>();
            mockService.Setup(service => service.CreateGenre(genre)).Verifiable();
            var controller = new GenresController(mockService.Object, _mapper);
            var genreViewModel = _mapper.Map<GenreViewModel>(genre);
            
            // Act
            var result = controller.Create(genreViewModel);

            // Assert
            Assert.IsNotNull(result);
            Assert.That(result, Is.InstanceOf<RedirectToActionResult>());
        }

        [Test]
        public void Create_CreateGenre_ReturnsAViewResult()
        {
            // Arrange
            var genre = new GenreDto { Name = "TestGenre" };
            var mockService = new Mock<IGenresService>();
            mockService.Setup(service => service.CreateGenre(genre)).Verifiable();
            var controller = new GenresController(mockService.Object, _mapper);

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
            var controller = new GenresController(mockService.Object, _mapper);
            controller.ModelState.AddModelError("Name", "Required");
            var genre = new GenreDto();
            var genreViewModel = _mapper.Map<GenreViewModel>(genre);

            // Act
            var result = controller.Create(genreViewModel) as ViewResult;

            // Assert
            Assert.AreEqual(genreViewModel, result?.Model);
        }

        [Test]
        public void Edit_EditNotExistingGenre_ReturnsRedirectToActionResult()
        {
            // Arrange
            var genreId = 100;
            var mockService = new Mock<IGenresService>();
            mockService.Setup(service => service.GetGenreById(genreId)).Returns((GenreDto)null);
            var controller = new GenresController(mockService.Object, _mapper);

            // Act
            var result = controller.Edit(genreId);

            // Assert
            Assert.That(result, Is.InstanceOf<RedirectToActionResult>());
        }

        [Test]
        public void Edit_EditGenre_ReturnsARedirectToIndex()
        {
            // Arrange
            var genre = new GenreDto { GenreId = 1, Name = "TestGenre" };
            var mockService = new Mock<IGenresService>();
            mockService.Setup(service => service.UpdateGenre(genre)).Verifiable();
            var controller = new GenresController(mockService.Object, _mapper);
            var genreViewModel = _mapper.Map<GenreViewModel>(genre);

            // Act
            var result = controller.Edit(genreViewModel);

            // Assert
            Assert.That(result, Is.InstanceOf<RedirectToActionResult>());
        }

        [Test]
        public void Delete_DeleteNotExistingGenre_RedirectToActionResult()
        {
            // Arrange
            var genreId = 100;
            var mockService = new Mock<IGenresService>();
            mockService.Setup(service => service.GetGenreById(genreId)).Returns((GenreDto)null);
            var controller = new GenresController(mockService.Object, _mapper);

            // Act
            var result = controller.Delete(genreId);

            // Assert
            Assert.IsNotNull(result);
            Assert.That(result, Is.InstanceOf<RedirectToActionResult>());
        }

        [Test]
        public void Delete_DeleteGenre_ReturnsAViewResult()
        {
            // Arrange
            var genre = new GenreDto { GenreId = 100, Name = "TestGenre" };
            var mockService = new Mock<IGenresService>();
            mockService.Setup(service => service.GetGenreById(genre.GenreId)).Returns(genre);
            var controller = new GenresController(mockService.Object, _mapper);

            // Act
            var result = controller.Delete(genre.GenreId);

            // Assert
            Assert.IsNotNull(result);
            Assert.That(result, Is.InstanceOf<ViewResult>());
        }

        private List<GenreDto> GetTestGenres()
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