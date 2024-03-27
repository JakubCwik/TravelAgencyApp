using Moq;
using NUnit.Framework;
using BiuroPodrozyAPI;
using BiuroPodrozyAPI.Services;
using BiuroPodrozyAPI.Controllers;
using BiuroPodrozyAPI.Models;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using BiuroPodrozyAPI.Entitties;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using BiuroPodrozyAPI.Exceptions;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;

namespace BiuroPodrozyAPI.Tests
{

    public class Tests
    {
        //Testowanie GetById zwraca poprawn¹ odpowiedz. Sprawdzanie poprawnosci typu wyniku oraz kod HTTP
        [Test]
        public void Test_GetById_Returns_Ok_ForExistingId()
        {
            // Arrange
            var expectedTravelAgencyDto = new TravelAgencyDto
            {
                Id = 1,
                Name = "Example Travel Agency"
                // Tutaj dodaj inne w³aœciwoœci biura podró¿y, które chcesz przetestowaæ
            };

            var mockService = new Mock<ITravelAgencyService>();
            mockService.Setup(repo => repo.GetById(1)).Returns(expectedTravelAgencyDto);
            var controller = new TravelAgencyController(mockService.Object);

            // Act
            var result = controller.GetTravelAgencyById(1); // Istniej¹ce ID biura podró¿y

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result.Result);
            var okResult = result.Result as OkObjectResult;
            Assert.AreEqual(200, okResult.StatusCode); // Oczekiwany status HTTP 200 Ok

            var returnedTravelAgency = okResult.Value as TravelAgencyDto;
            Assert.AreEqual(expectedTravelAgencyDto.Id, returnedTravelAgency.Id);
            Assert.AreEqual(expectedTravelAgencyDto.Name, returnedTravelAgency.Name);
            // SprawdŸ inne w³aœciwoœci biura podró¿y, jeœli to konieczne
        }




        [SetUp]
        public void Setup()
        {
            ITravelAgencyService _travelAgencyService = new TravelAgencyService(null, null, null);
        }
        // Sprawdzenie czy metoda GetByID zwraca oczekiwane dane dla danego id
        [Test]
        public void GetTravelAgencyById_WithValidId_ReturnsExpectedTravelAgency()
        {
            // Arrange
            var expectedTravelAgencyDto = new TravelAgencyDto
            {
                Id = 1,
                Name = "Adventure Expeditions",
                Description = "Specializing in thrilling adventures around the globe.",
                ContactEmail = "info@adventureexpeditions.com",
                ContactNumber = "+1234567890",
                City = "Adventure City",
                Street = "123 Adventure St",
                PostalCode = "12345"
            };

            var mockService = new Mock<ITravelAgencyService>();
            mockService.Setup(service => service.GetById(1)).Returns(expectedTravelAgencyDto);
            var controller = new TravelAgencyController(mockService.Object);

            // Act
            var actionResult = controller.GetTravelAgencyById(1);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(actionResult.Result);

            var okResult = actionResult.Result as OkObjectResult;
            var resultTravelAgency = okResult.Value as TravelAgencyDto;

            Assert.AreEqual(expectedTravelAgencyDto.Id, resultTravelAgency.Id);
            Assert.AreEqual(expectedTravelAgencyDto.Name, resultTravelAgency.Name);
            Assert.AreEqual(expectedTravelAgencyDto.Description, resultTravelAgency.Description);
            Assert.AreEqual(expectedTravelAgencyDto.ContactEmail, resultTravelAgency.ContactEmail);
            Assert.AreEqual(expectedTravelAgencyDto.ContactNumber, resultTravelAgency.ContactNumber);
            Assert.AreEqual(expectedTravelAgencyDto.City, resultTravelAgency.City);
            Assert.AreEqual(expectedTravelAgencyDto.Street, resultTravelAgency.Street);
            Assert.AreEqual(expectedTravelAgencyDto.PostalCode, resultTravelAgency.PostalCode);
        }

        //Testowanie zachowania kontrolera gdy usuniêcie agencji podró¿y zakoñczy siê wyj¹tkiem 
        [Test]
        public void TestDelete_TravelAgencyNotFound()
        {
            // Arrange
            var mockRepository = new Mock<ITravelAgencyService>();
            mockRepository.Setup(repo => repo.Delete(It.IsAny<int>())).Throws(new NotFoundException("Travel agency not found"));
            var controller = new TravelAgencyController(mockRepository.Object);

            // Act & Assert
            Assert.Throws<NotFoundException>(() => controller.Delete(999));
        }


        //Testowanie dzia³ania metody Delete kontrolera
        [Test]
        public void TestDelete_ExistingTravelAgency_ReturnsNoContent()
        {
            // Arrange
            var mockRepository = new Mock<ITravelAgencyService>();
            mockRepository.Setup(repo => repo.Delete(1)); // Ustawienie zachowania dla istniej¹cego identyfikatora
            var controller = new TravelAgencyController(mockRepository.Object);

            // Act
            var result = controller.Delete(1) as NoContentResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(204, result.StatusCode);
        }

        //Testowanie czy utworzenie nowej oferty zwraca odpowiedz 'Created'
        [Test]
        public void TestCreateTravelAgency_ReturnsCreatedResponse()
        {
            // Arrange
            var mockRepository = new Mock<ITravelAgencyService>();
            var controller = new TravelAgencyController(mockRepository.Object);
            var dto = new CreateTravelAgencyDto
            {
                Name = "Test Agency",
                City = "Test City",
                Street = "Test Street"
            };

            // Act
            var result = controller.CreateTravelAgency(dto) as CreatedResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(201, result.StatusCode);
        }

        //Sprawdzenie metody GetAll (poprawnosc zwracanych danych, czy nie jest pusty, status kodu)
        [Test]
        public void TestGetAll_ReturnsOkWithTravelAgencies()
        {
            // Arrange
            var mockRepository = new Mock<ITravelAgencyService>();
            var expectedTravelAgencies = new List<TravelAgencyDto>
            {
                new TravelAgencyDto { Id = 1, Name = "Agency 1", City = "City 1", Street = "Street 1" },
                new TravelAgencyDto { Id = 2, Name = "Agency 2", City = "City 2", Street = "Street 2" }
            };
            mockRepository.Setup(repo => repo.GetAll()).Returns(expectedTravelAgencies);
            var controller = new TravelAgencyController(mockRepository.Object);

            // Act
            var actionResult = controller.GetAll();
            var result = actionResult.Result as OkObjectResult;
            var travelAgencies = result.Value as List<TravelAgencyDto>;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsNotNull(travelAgencies);
            Assert.AreEqual(expectedTravelAgencies.Count, travelAgencies.Count);
            Assert.AreEqual(200, result.StatusCode);
        }


    }
}