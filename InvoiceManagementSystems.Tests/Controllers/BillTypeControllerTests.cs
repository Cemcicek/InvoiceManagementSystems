using FakeItEasy;
using FluentAssertions;
using InvoiceManagementSystems.Controllers;
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
    public class BillTypeControllerTests
    {
        private readonly IBillTypeRepository _billTypeRepository;
        public BillTypeControllerTests()
        {
            _billTypeRepository = A.Fake<IBillTypeRepository>();
        }

        [Fact]
        public void BillTypeController_GetBillTypes_ReturnOK()
        {
            // Arrange 
            var billTypes = A.Fake<ICollection<BillType>>();
            var billTypeList = A.Fake<List<BillType>>();
            A.CallTo(() => _billTypeRepository.GetBillTypes()).Returns(billTypeList);
            var controller = new BillTypeController(_billTypeRepository);

            // Act
            var result = controller.GetBillTypes();

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(OkObjectResult));
        }

        [Fact]
        public void BillTypeController_GetBillType_ReturnsNotFound_WhenBillTypeDoesNotExist()
        {
            // Arrange
            var nonExistentBillTypeId = 1;
            A.CallTo(() => _billTypeRepository.BillTypeExists(nonExistentBillTypeId)).Returns(false);
            var controller = new BillTypeController(_billTypeRepository);

            // Act
            var result = controller.GetBillType(nonExistentBillTypeId);

            // Assert
            result.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public void BillTypeController_GetBillType_ReturnsOkWithBillType_WhenBillTypeExists()
        {
            // Arrange
            var existingBillTypeId = 1;
            var fakeBillType = A.Fake<BillType>();
            A.CallTo(() => _billTypeRepository.BillTypeExists(existingBillTypeId)).Returns(true);
            A.CallTo(() => _billTypeRepository.GetBillType(existingBillTypeId)).Returns(fakeBillType);
            var controller = new BillTypeController(_billTypeRepository);

            // Act
            var result = controller.GetBillType(existingBillTypeId);

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            var okResult = result as OkObjectResult;
            okResult.Value.Should().Be(fakeBillType);
        }

        [Fact]
        public void BillTypeController_CreateBillType_ReturnsOk_WhenBillTypeIsValid()
        {
            // Arrange
            var billType = new BillType
            {
                Id = 1,
                BillTypeName = "Elektrik"
            };
            A.CallTo(() => _billTypeRepository.GetBillTypes()).Returns(new System.Collections.Generic.List<BillType>());
            A.CallTo(() => _billTypeRepository.CreateBillType(A<BillType>.Ignored)).Returns(true);

            var controller = new BillTypeController(_billTypeRepository);

            // Act
            var result = controller.CreateBillType(billType);

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            var okResult = result as OkObjectResult;
            okResult.Value.Should().Be("Kayıt eklendi!");
        }

        [Fact]
        public void BillTypeController_CreateBillType_ReturnsBadRequest_WhenModelStateIsInvalid()
        {
            // Arrange
            var billType = new BillType
            {
                Id = 1,
                BillTypeName = null
            };
            var controller = new BillTypeController(_billTypeRepository);
            controller.ModelState.AddModelError("BillTypeName", "BillTypeName is required");

            // Act
            var result = controller.CreateBillType(billType);

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>();
        }

        [Fact]
        public void BillTypeController_CreateBillType_ReturnsUnprocessableEntity_WhenBillTypeAlreadyExists()
        {
            // Arrange
            var existingBillType = new BillType
            {
                Id = 1,
                BillTypeName = "Elektrik"
            };
            var billType = new BillType
            {
                Id = 2,
                BillTypeName = "Elektrik"
            };
            A.CallTo(() => _billTypeRepository.GetBillTypes()).Returns(new System.Collections.Generic.List<BillType> { existingBillType });
            var controller = new BillTypeController(_billTypeRepository);

            // Act
            var result = controller.CreateBillType(billType);

            // Assert
            result.Should().BeOfType<ObjectResult>();
            var objectResult = result as ObjectResult;
            objectResult.StatusCode.Should().Be(422);
        }

        [Fact]
        public void BillTypeController_DeleteBillType_ReturnsNoContent_WhenBillTypeExists()
        {
            // Arrange
            var billTypeId = 1;
            var existingBillType = new BillType
            {
                Id = billTypeId,
                BillTypeName = "Elektrik"
            };
            A.CallTo(() => _billTypeRepository.BillTypeExists(billTypeId)).Returns(true);
            A.CallTo(() => _billTypeRepository.GetBillType(billTypeId)).Returns(existingBillType);
            A.CallTo(() => _billTypeRepository.DeleteBillType(A<BillType>.Ignored)).Returns(true);

            var controller = new BillTypeController(_billTypeRepository);

            // Act
            var result = controller.DeleteBillType(billTypeId);

            // Assert
            result.Should().BeOfType<NoContentResult>();
        }

        [Fact]
        public void BillTypeController_DeleteBillType_ReturnsNotFound_WhenBillTypeDoesNotExist()
        {
            // Arrange
            var billTypeId = 1;
            A.CallTo(() => _billTypeRepository.BillTypeExists(billTypeId)).Returns(false);

            var controller = new BillTypeController(_billTypeRepository);

            // Act
            var result = controller.DeleteBillType(billTypeId);

            // Assert
            result.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public void BillTypeController_DeleteBillType_ReturnsBadRequest_WhenModelStateIsInvalid()
        {
            // Arrange
            var billTypeId = 1;
            var existingBillType = new BillType
            {
                Id = billTypeId,
                BillTypeName = "Elektrik"
            };
            A.CallTo(() => _billTypeRepository.BillTypeExists(billTypeId)).Returns(true);
            A.CallTo(() => _billTypeRepository.GetBillType(billTypeId)).Returns(existingBillType);
            A.CallTo(() => _billTypeRepository.DeleteBillType(A<BillType>.Ignored)).Returns(false);

            var controller = new BillTypeController(_billTypeRepository);
            controller.ModelState.AddModelError("DeleteError", "erorr...");

            // Act
            var result = controller.DeleteBillType(billTypeId);

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>();
        }
    }
}
