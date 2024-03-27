using System;
using System.Collections.Generic;
using BiuroPodrozyAPI.Controllers;
using BiuroPodrozyAPI.Models;
using BiuroPodrozyAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;

namespace BiuroPodrozyAPI.Tests
{
    public class OfferControllerTests
    {

        //Sprawdza czy metoda Delete po udanym usunięciu zwraca 204 No Content
        [Test]
        public void Delete_Returns_NoContent()
        {
            // Arrange
            var mockOfferService = new Mock<iOfferService>();
            var controller = new OfferController(mockOfferService.Object);

            // Act
            var result = controller.Delete(1);

            // Assert
            Assert.AreEqual(typeof(NoContentResult), result.GetType());
        }

        //Sprawdza czy po udanym dodaniu nowej oferty kod jest 201 Created
        [Test]
        public void Post_Returns_Created()
        {
            // Arrange
            var mockOfferService = new Mock<iOfferService>();
            mockOfferService.Setup(service => service.Create(It.IsAny<int>(), It.IsAny<CreateOfferDto>())).Returns(1);
            var controller = new OfferController(mockOfferService.Object);
            var createDto = new CreateOfferDto();

            // Act
            var result = controller.Post(1, createDto);

            // Assert
            Assert.AreEqual(typeof(CreatedResult), result.GetType());
        }

        //Czy jest zwracany obiekt zawierający oferty
        [Test]
        public void Get_Returns_Ok_With_OfferDto()
        {
            // Arrange
            var mockOfferService = new Mock<iOfferService>();
            mockOfferService.Setup(service => service.GetById(It.IsAny<int>(), It.IsAny<int>())).Returns(new OfferDto());
            var controller = new OfferController(mockOfferService.Object);

            // Act
            var result = controller.Get(1, 1);

            // Assert
            Assert.AreEqual(typeof(ActionResult<OfferDto>), result.GetType());
        }


        //Czy jest zwracana lista ofert ze statusem 200 OK
        [Test]
        public void GetAll_Returns_Ok_With_List_Of_OfferDto()
        {
            // Arrange
            var mockOfferService = new Mock<iOfferService>();
            mockOfferService.Setup(service => service.GetAll(It.IsAny<int>())).Returns(new List<OfferDto>());
            var controller = new OfferController(mockOfferService.Object);

            // Act
            var result = controller.Get(1) as ActionResult<List<OfferDto>>;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<OkObjectResult>(result.Result);
            var okResult = result.Result as OkObjectResult;
            Assert.AreEqual(200, okResult.StatusCode);
        }

    }
}
