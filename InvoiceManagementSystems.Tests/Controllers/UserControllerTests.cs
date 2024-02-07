using AutoMapper;
using FakeItEasy;
using FluentAssertions;
using InvoiceManagementSystems.Controllers;
using InvoiceManagementSystems.Core.DTOs;
using InvoiceManagementSystems.Core.Helper;
using InvoiceManagementSystems.Models;
using InvoiceManagementSystems.Repositories.Interfaces;
using InvoiceManagementSystems.Repositories.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceManagementSystems.Tests.Controllers
{
    public class UserControllerTests
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserControllerTests()
        {
            _userRepository = A.Fake<IUserRepository>();
            _mapper = A.Fake<IMapper>();
        }

        [Fact]
        public void UserController_GetUser_ReturnsOkWithUser()
        {
            // Arrange
            var userMail = "test@gmail.com";
            var userId = 1;
            var user = new User { Id = userId, FirstName = "Test", LastName = "User", Email = userMail };
            A.CallTo(() => _userRepository.GetUserByMail(userMail)).Returns(user);
            A.CallTo(() => _userRepository.GetUser(userId)).Returns(user);

            var controller = new UserController(_userRepository, _mapper);
            controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext
                {
                    User = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
                    {
                        new Claim(ClaimTypes.Name, userMail)
                    }))
                }
            };

            // Act
            var result = controller.GetUser();

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            var okResult = result as OkObjectResult;
            okResult.Value.Should().Be(user);
        }

        [Fact]
        public void UserController_UpdateUser_ReturnsNoContent()
        {
            // Arrange
            var userMail = "test@gmail.com";
            var userId = 1;
            var updatedUser = new User { Id = userId, FirstName = "Updated", LastName = "User", Email = userMail };
            var existingUser = new User { Id = userId, FirstName = "Test", LastName = "User", Email = userMail };
            A.CallTo(() => _userRepository.GetUserByMail(userMail)).Returns(existingUser);
            A.CallTo(() => _userRepository.GetUserById(userId)).Returns(existingUser);
            A.CallTo(() => _userRepository.UpdateUser(A<User>.Ignored)).Returns(true);

            var controller = new UserController(_userRepository, _mapper);
            controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext
                {
                    User = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
                    {
                        new Claim(ClaimTypes.Name, userMail)
                    }))
                }
            };

            // Act
            var result = controller.UpdateUser(updatedUser);

            // Assert
            result.Should().BeOfType<NoContentResult>();
        }
    }
}
