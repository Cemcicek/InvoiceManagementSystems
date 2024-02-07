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
     
    }
}
