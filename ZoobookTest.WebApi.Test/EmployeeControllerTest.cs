using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ZoobookTest.Service.Employee;
using ZoobookTest.Service.Model;
using ZoobookTest.WebApi.Controllers;

namespace ZoobookTest.WebApi.Test
{
    public class EmployeeControllerTest
    {
        private readonly EmployeeController _employeeController;
        private readonly IEmployeeService _employeeService;
        private readonly ILogger<EmployeeController> _logger;

        public EmployeeControllerTest()
        {
            _employeeService = new EmployeeServiceFake();
            _employeeController = new EmployeeController(_logger, _employeeService);

        }

        [Fact]
        public void GetAll_WhenCalled_ReturnsOkResult()
        {
            //Act
            var result = _employeeController.GetAll() as ObjectResult;

            //Assert
            Assert.IsType<ObjectResult>(result as ObjectResult);
        }
        [Fact]
        public void GetAll_WhenCalled_ReturnsAllEmployees()
        {
            //Act
            var result = _employeeController.GetAll();

            //Assert
            var employees = Assert.IsType<List<EmployeeDto>>(result);
            Assert.Equal(3, employees.Count);
        }

        [Fact]
        public void Get_ZeroPassed_ReturnsNotFoundResult()
        {
            //Act
            var result = _employeeController.Get(0);

            //Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void Get_NullPassed_ReturnsNotFoundResult()
        {
            //Act
            var result = _employeeController.Get(null);

            //Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void Get_ExistingIdPassed_ReturnsOkResult()
        {
            //Arrange
            var existedId = 1;
            //Act
            var result = _employeeController.Get(existedId);

            //Assert
            Assert.IsType<OkObjectResult>(result as OkObjectResult);
        }

        [Fact]
        public void Get_ExistingIdPassed_ReturnsRightEmployee()
        {
            //Arrange
            var existedId = 1;
            //Act
            var result = _employeeController.Get(existedId) as OkObjectResult;

            //Assert
            Assert.IsType<EmployeeDto>(result.Value);
            Assert.Equal(existedId, (result.Value as EmployeeDto).Id);
        }

        [Fact]
        public void Create_InvalidEmployeePassed_ReturnsBadRequst()
        {
            //Arrange
            var newEmployee = new EmployeeDto() { FirstName = "Jack" };
            _employeeController.ModelState.AddModelError("LastName", "Required");
            //Act
            var result = _employeeController.Create(newEmployee);

            //Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public void Create_ValidEmployeePassed_ReturnsCreatedResponse()
        {
            //Arrange
            var newEmployee = new EmployeeDto() { FirstName = "Jack", LastName = "Aqua", MiddleName = "RA" };

            //Act
            var result = _employeeController.Create(newEmployee);

            //Assert
            Assert.IsType<ObjectResult>(result);
        }

        [Fact]
        public void Create_ValidEmployeePassed_ReturnsResponseHasCreatedEmployee()
        {
            //Arrange
            var newEmployee = new EmployeeDto() { FirstName = "Jack", LastName = "Aqua", MiddleName = "RA" };

            //Act
            var result = _employeeController.Create(newEmployee) as ObjectResult;
            var createdEmployee = result?.Value as EmployeeDto;

            //Assert
            Assert.IsType<EmployeeDto>(createdEmployee);
            Assert.Equal("Jack", createdEmployee?.FirstName);
        }

        [Fact]
        public void Remove_NotExistingIdPassed_ReturnsNotFoundResponse()
        {
            //Arrange
            var notExistingId = new Random().Next();

            //Act
            var result = _employeeController.Remove(notExistingId);

            //Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void Remove_ExistingIdPassed_RemovesOneEmployee()
        {
            //Arrange
            var existingId = 1;

            //Act
            var result = _employeeController.Remove(existingId);

            //Assert
            Assert.Equal(2, _employeeService.GetAll().Count());
        }

        [Fact]
        public void Edit_NotExistingIdPassed_ReturnsNotFoundResponse()
        {
            //Arrange
            var notExistingId = new Random().Next();
            var updatedObj = new EmployeeDto() { Id = notExistingId, FirstName = "AA", LastName = "BB", MiddleName = "" };

            //Act
            var result = _employeeController.Edit(updatedObj);

            //Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void Edit_ExistingIdPassed_ReturnOkResult()
        {
            //Arrange
            var updatedObj = new EmployeeDto() { Id = 1, FirstName = "AA", LastName = "BB", MiddleName = "" };

            //Act
            var result = _employeeController.Edit(updatedObj);

            //Assert
            Assert.IsType<StatusCodeResult>(result as StatusCodeResult);
            Assert.Equal((result as StatusCodeResult).StatusCode, 200);
        }

        [Fact]
        public void Edit_ExistingIdPassed_UpdatesOneEmployee()
        {
            //Arrange
            var updatedObj = new EmployeeDto() { Id = 1, FirstName = "AA", LastName = "BB", MiddleName = "" };

            //Act
            var result = _employeeController.Edit(updatedObj);
            var updatedObjAfter = _employeeController.Get(updatedObj.Id) as ObjectResult;

            //Assert
            Assert.IsType<StatusCodeResult>(result as StatusCodeResult);
            Assert.Equal((updatedObjAfter.Value as EmployeeDto).FirstName, updatedObj.FirstName);
        }
    }
}
