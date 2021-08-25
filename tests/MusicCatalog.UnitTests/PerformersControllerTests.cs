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
    public class PerformersControllerTests
    {
        private static IMapper _mapper;

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
        public void Index_GetAllPerformers_ReturnsAViewResultWithAListOfPerformers()
        {
            // Arrange
            var mockService = new Mock<IPerformersService>();
            mockService.Setup(service => service.GetPerformers()).Returns(GetTestPerformers());
            var controller = new PerformersController(mockService.Object, _mapper);

            // Act
            var result = controller.Index() as ViewResult;

            // Assert
            Assert.That(result, Is.InstanceOf<ViewResult>());
        }

        [Test]
        public void Create_CreatePerformer_ReturnsARedirectToIndex()
        {
            // Arrange
            var performer = new PerformerDto { Name = "TestPerformer" };
            var mockService = new Mock<IPerformersService>();
            mockService.Setup(service => service.CreatePerformer(performer)).Verifiable();
            var controller = new PerformersController(mockService.Object, _mapper);
            var performerViewModel = _mapper.Map<PerformerViewModel>(performer);

            // Act
            var result = controller.Create(performerViewModel);

            // Assert
            Assert.IsNotNull(result);
            Assert.That(result, Is.InstanceOf<RedirectToActionResult>());
        }

        [Test]
        public void Create_CreatePerformer_ReturnsAViewResult()
        {
            // Arrange
            var performer = new PerformerDto { Name = "TestPerformer" };
            var mockService = new Mock<IPerformersService>();
            mockService.Setup(service => service.CreatePerformer(performer)).Verifiable();
            var controller = new PerformersController(mockService.Object, _mapper);

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
            var controller = new PerformersController(mockService.Object, _mapper);
            controller.ModelState.AddModelError("Name", "Required");
            var performer = new PerformerDto();
            var performerViewModel = _mapper.Map<PerformerViewModel>(performer);

            // Act
            var result = controller.Create(performerViewModel) as ViewResult;

            // Assert
            Assert.AreEqual(performerViewModel, result?.Model);
        }

        [Test]
        public void Edit_EditNotExistingPerformer_ReturnsNotFound()
        {
            // Arrange
            var performerId = 100;
            var mockService = new Mock<IPerformersService>();
            mockService.Setup(service => service.GetPerformerById(performerId)).Returns((PerformerDto)null);
            var controller = new PerformersController(mockService.Object, _mapper);

            // Act
            var result = controller.Edit(performerId);

            // Assert
            Assert.That(result, Is.InstanceOf<NotFoundResult>());
        }

        [Test]
        public void Edit_EditPerformer_ReturnsARedirectToIndex()
        {
            // Arrange
            var performer = new PerformerDto { PerformerId = 1, Name = "TestPerformer" };
            var mockService = new Mock<IPerformersService>();
            mockService.Setup(service => service.UpdatePerformer(performer)).Verifiable();
            var controller = new PerformersController(mockService.Object, _mapper);
            var performerViewModel = _mapper.Map<PerformerViewModel>(performer);

            // Act
            var result = controller.Edit(performerViewModel);

            // Assert
            Assert.That(result, Is.InstanceOf<RedirectToActionResult>());
        }

        [Test]
        public void Delete_DeleteNotExistingPerformer_ReturnsNotFound()
        {
            // Arrange
            var performerId = 100;
            var mockService = new Mock<IPerformersService>();
            mockService.Setup(service => service.GetPerformerById(performerId)).Returns((PerformerDto)null);
            var controller = new PerformersController(mockService.Object, _mapper);

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
            var performer = new PerformerDto { PerformerId = 1, Name = "TestPerformer" };
            var mockService = new Mock<IPerformersService>();
            mockService.Setup(service => service.GetPerformerById(performer.PerformerId)).Returns(performer);
            var controller = new PerformersController(mockService.Object, _mapper);

            // Act
            var result = controller.Delete(performer.PerformerId);

            // Assert
            Assert.IsNotNull(result);
            Assert.That(result, Is.InstanceOf<ViewResult>());
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