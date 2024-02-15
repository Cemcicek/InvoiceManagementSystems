using FakeItEasy;
using FluentAssertions;
using InvoiceManagementSystems.Controllers;
using InvoiceManagementSystems.Core.DTOs;
using InvoiceManagementSystems.Models;
using InvoiceManagementSystems.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceManagementSystems.Tests.Controllers
{
    public class ApartmentControllerTests
    {
        private readonly IApartmentRepository _apartmentRepository;
        public ApartmentControllerTests()
        {
            _apartmentRepository = A.Fake<IApartmentRepository>();
        }

        [Fact]
        public void ApartmentController_GetApartments_ReturnsOk_WithApartments()
        {
            // Arrange
            var apartments = new List<Apartment>
            {
                new Apartment { Id = 1, ApartmentNo = "1", Floor = "1", ApartmentBlock = "A"},
                new Apartment { Id = 2, ApartmentNo = "2", Floor = "2", ApartmentBlock = "B" },
            };
            A.CallTo(() => _apartmentRepository.GetApartments()).Returns(apartments);

            var controller = new ApartmentController(_apartmentRepository);

            // Act
            var result = controller.GetApartments();

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            var okResult = result as OkObjectResult;
            okResult.Value.Should().BeOfType<List<Apartment>>().And.BeEquivalentTo(apartments);
        }

        [Fact]
        public void ApartmentController_GetApartments_ReturnsBadRequest_WhenModelStateIsInvalid()
        {
            // Arrange
            var controller = new ApartmentController(_apartmentRepository);
            controller.ModelState.AddModelError("ModelError", "Model state is invalid");

            // Act
            var result = controller.GetApartments();

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>();
        }

        [Fact]
        public void ApartmentController_GetApartment_ReturnsOk_WithApartment()
        {
            // Arrange
            var apartmentId = 1;
            var apartment = new
            {
                ApartmentId = apartmentId,
                ApartmentNo = "1",
                FirstName = "Ahmet",
                LastName = "Mehmet",
                Email = "ahmet@gmail.com",
                Tel = "0555555555"
            };
            A.CallTo(() => _apartmentRepository.ApartmentExists(apartmentId)).Returns(true);
            A.CallTo(() => _apartmentRepository.GetApartment(apartmentId)).Returns(new List<object> { apartment });

            var controller = new ApartmentController(_apartmentRepository);

            // Act
            var result = controller.GetApartment(apartmentId);

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            var okResult = result as OkObjectResult;
            okResult.Value.Should().BeOfType<List<object>>();
            var returnedApartment = (okResult.Value as List<object>).FirstOrDefault();
            returnedApartment.Should().BeEquivalentTo(apartment);
        }

        [Fact]
        public void ApartmentController_GetApartment_ReturnsNotFound_WhenApartmentDoesNotExist()
        {
            // Arrange
            var apartmentId = 1;
            A.CallTo(() => _apartmentRepository.ApartmentExists(apartmentId)).Returns(false);

            var controller = new ApartmentController(_apartmentRepository);

            // Act
            var result = controller.GetApartment(apartmentId);

            // Assert
            result.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public void ApartmentController_GetApartment_ReturnsBadRequest_WhenModelStateIsInvalid()
        {
            // Arrange
            var apartmentId = 1;
            var apartment = new
            {
                ApartmentId = apartmentId,
                ApartmentNo = "2",
                FirstName = "Mehmet",
                LastName = "Ahmet",
                Email = "mehmet@gmail.com",
                Tel = "05555555555"
            };
            A.CallTo(() => _apartmentRepository.ApartmentExists(apartmentId)).Returns(true);
            A.CallTo(() => _apartmentRepository.GetApartment(apartmentId)).Returns(new List<object> { apartment });

            var controller = new ApartmentController(_apartmentRepository);
            controller.ModelState.AddModelError("ModelError", "Model state is invalid");

            // Act
            var result = controller.GetApartment(apartmentId);

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>();
        }

        [Fact]
        public void ApartmentController_GetUsers_ReturnsOk_WithUsers()
        {
            // Arrange
            var users = new List<UserDto>
            {
                new UserDto { Id = 1, FirstName = "Ahmet", LastName = "Mehmet", Email = "ahmet@gmail.com" },
                new UserDto { Id = 2, FirstName = "Mehmet", LastName = "Ahmet", Email = "mehmet@gmail.com" },
            };
            A.CallTo(() => _apartmentRepository.GetUsers()).Returns(users);

            var controller = new ApartmentController(_apartmentRepository);

            // Act
            var result = controller.GetUsers();

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            var okResult = result as OkObjectResult;
            okResult.Value.Should().BeOfType<List<UserDto>>().And.BeEquivalentTo(users);
        }

        [Fact]
        public void ApartmentController_GetUsers_ReturnsBadRequest_WhenModelStateIsInvalid()
        {
            // Arrange
            var controller = new ApartmentController(_apartmentRepository);
            controller.ModelState.AddModelError("ModelError", "Model state is invalid");

            // Act
            var result = controller.GetUsers();

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>();
        }
    }
}
