using API_Rest_Simple.Controllers;
using API_Rest_Simple.Models;
using API_Rest_Simple.Repositories;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace TestProject
{
    [TestClass]
    public class PeopleController_test
    {

        private PeopleController _controller;
        private Mock<IPersonRepository> _mockService;

        [TestInitialize]
        public void SetUp()
        {
            _mockService = new Mock<IPersonRepository>();
            _controller = new PeopleController(_mockService.Object);
        }

        [TestMethod]
        public async Task Get_ReturnsOkResult_WithDataAsync()
        {
            // Arrange
            List<Professional_exp> professional_ = new List<Professional_exp>();
            var person = new Person { id = 0, name = "John Doe", age = 30, professional_exp = professional_  };

            // Mock the AddPersonAsync method to simulate it working correctly
             _mockService.Setup(s => s.AddPersonAsync(person)).Returns(Task.CompletedTask);

            var result = await _controller.PostPerson(person);

            // Assert
            // Check that the result is a CreatedAtActionResult
            var createdAtActionResult = result.Result as CreatedAtActionResult;
            Assert.IsNotNull(createdAtActionResult, "Expected CreatedAtActionResult but got null.");

            // Verify the action name is "GetPerson"
            Assert.AreEqual("GetPerson", createdAtActionResult.ActionName);

            // Verify that the route value contains the correct id
            Assert.AreEqual(person.id, createdAtActionResult.RouteValues["id"]);

            // Verify that the returned value is the expected person object
            Assert.AreEqual(person, createdAtActionResult.Value);
        }
    }
}

