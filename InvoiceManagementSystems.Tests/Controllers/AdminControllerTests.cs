using AutoMapper;
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
    public class AdminControllerTests
    {
        private readonly IAdminRepository _adminRepository;
        private readonly IMapper _mapper;
        public AdminControllerTests()
        {
            _adminRepository = A.Fake<IAdminRepository>();
            _mapper = A.Fake<IMapper>();
        }

        [Fact]
        public void AdminController_GetAdmins_ReturnsOkWithAdmins()
        {
            // Arrange
            var expectedAdmins = new List<Admin>
            {
                new Admin { Id = 1, FirstName = "Admin 1", Email = "admin1@gmail.com" },
                new Admin { Id = 2, FirstName = "Admin 2", Email = "admin2@gmail.com" }
            };
            A.CallTo(() => _adminRepository.GetAdmins()).Returns(expectedAdmins);
            var controller = new AdminController(_adminRepository, _mapper);

            // Act
            var result = controller.GetAdmins();

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            var okResult = result as OkObjectResult;
            okResult.Value.Should().BeAssignableTo<IEnumerable<Admin>>();
            var admins = okResult.Value as IEnumerable<Admin>;
            admins.Should().HaveCount(expectedAdmins.Count);
        }

        [Fact]
        public void AdminController_GetUsers_ReturnsOkWithUsers()
        {
            // Arrange
            var expectedUsers = new List<UserDto>
            {
                new UserDto { Id = 1, FirstName = "Ahmet", LastName = "Mehmet", Email = "ahmet@gmail.com" },
                new UserDto { Id = 2, FirstName = "Mehmet", LastName = "Ahmet", Email = "mehmet@gmail.com" }
            };
            A.CallTo(() => _adminRepository.GetUsers()).Returns(expectedUsers);
            var controller = new AdminController(_adminRepository, _mapper);

            // Act
            var result = controller.GetUsers();

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            var okResult = result as OkObjectResult;
            okResult.Value.Should().BeAssignableTo<IEnumerable<UserDto>>();
            var users = okResult.Value as IEnumerable<UserDto>;
            users.Should().HaveCount(expectedUsers.Count);
        }

        [Fact]
        public void AdminController_UpdateAdmin_ReturnsNoContent()
        {
            // Arrange
            var adminId = 1;
            var updateAdminDto = new AdminDto
            {
                Id = adminId,
                FirstName = "UpdatedAdmin",
                Email = "updated_admin@gmail.com"
            };
            var existingAdmin = new Admin
            {
                Id = adminId,
                FirstName = "Existing Admin",
                Email = "existing_admin@gmail.com"
            };
            A.CallTo(() => _adminRepository.GetAdmin(adminId)).Returns(existingAdmin);
            A.CallTo(() => _adminRepository.UpdateAdmin(A<Admin>.Ignored)).Returns(true);

            var controller = new AdminController(_adminRepository, _mapper);

            // Act
            var result = controller.UpdateAdmin(adminId, updateAdminDto);

            // Assert
            result.Should().BeOfType<NoContentResult>();
        }

        [Fact]
        public void AdminController_UpdateAdmin_ReturnsBadRequest_WhenModelStateIsInvalid()
        {
            // Arrange
            var adminId = 1;
            var updateAdminDto = new AdminDto
            {
                Id = adminId,
                FirstName = "Updated Admin",
                Email = "updated_admin@gmail.com"
            };
            var controller = new AdminController(_adminRepository, _mapper);
            controller.ModelState.AddModelError("Email", "Email is required");

            // Act
            var result = controller.UpdateAdmin(adminId, updateAdminDto);

            // Assert
            result.Should().BeOfType<BadRequestResult>();
        }

        [Fact]
        public void AdminController_UpdateUser_ReturnsNoContent()
        {
            // Arrange
            var userId = 1;
            var updateUser = new User
            {
                Id = userId,
                FirstName = "Updated First Name"
            };
            var existingUser = new User
            {
                Id = userId,
                FirstName = "Existing First Name"
            };
            A.CallTo(() => _adminRepository.GetUserById(userId)).Returns(existingUser);
            A.CallTo(() => _adminRepository.UpdateUser(A<User>.Ignored)).Returns(true);

            var controller = new AdminController(_adminRepository, _mapper);

            // Act
            var result = controller.UpdateUser(userId, updateUser);

            // Assert
            result.Should().BeOfType<NoContentResult>();
        }

        [Fact]
        public void AdminController_UpdateUser_ReturnsBadRequest_WhenModelStateIsInvalid()
        {
            // Arrange
            var userId = 1;
            var updateUser = new User
            {
                Id = userId,
                FirstName = "Updated First Name",
                LastName = "Updated Last Name"
            };
            var controller = new AdminController(_adminRepository, _mapper);
            controller.ModelState.AddModelError("Email", "Email is required");

            // Act
            var result = controller.UpdateUser(userId, updateUser);

            // Assert
            result.Should().BeOfType<BadRequestResult>();
        }
    }
}
