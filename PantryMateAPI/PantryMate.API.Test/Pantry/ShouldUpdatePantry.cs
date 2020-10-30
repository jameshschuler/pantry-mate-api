using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PantryMate.API.Controllers;
using PantryMate.API.Entities;
using PantryMate.API.Models.Request;
using PantryMate.API.Models.Response;
using PantryMate.API.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PantryMate.API.Test.Pantry
{
    [TestClass]
    public class ShouldUpdatePantry
    {
        private Mock<IPantryService> _mockPantryService;

        [TestInitialize]
        public void Initialize()
        {
            _mockPantryService = new Mock<IPantryService>();
        }

        [TestMethod]
        public void ShouldValidateRequestModel()
        {
            var updatePantryRequest = new UpdatePantryRequest();

            var results = ValidateModel(updatePantryRequest);

            Assert.AreEqual(2, results.Count);
            Assert.IsTrue(results.ElementAt(0).MemberNames.Contains("Name"));
            Assert.IsTrue(results.ElementAt(0).ErrorMessage.Contains("required"));
            Assert.IsTrue(results.ElementAt(1).MemberNames.Contains("Description"));
            Assert.IsTrue(results.ElementAt(1).ErrorMessage.Contains("required"));
        }

        [TestMethod]
        public async Task ShouldUpdatePantrySuccessfully()
        {
            var accountId = 1;
            var pantryId = 1;
            var request = new UpdatePantryRequest() { Name = "Test Pantry - Updated" };

            var controller = new PantryController(_mockPantryService.Object)
            {
                ControllerContext = new ControllerContext
                {
                    HttpContext = new DefaultHttpContext()
                }
            };
            controller.ControllerContext.HttpContext.Items["Account"] = new Account() { AccountId = accountId };

            _mockPantryService.Setup(e => e.GetPantry(It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync(new PantryResponse { Name = "Test Pantry", PantryId = pantryId });
            
            _mockPantryService.Setup(e => e.UpdatePantry(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<UpdatePantryRequest>()))
                .Returns(Task.CompletedTask);
              
            var response = await controller.UpdatePantry(pantryId, request) as NoContentResult;
            Assert.AreEqual(204, response.StatusCode);
        }

        private IList<ValidationResult> ValidateModel(object model)
        {
            var validationResults = new List<ValidationResult>();
            var ctx = new ValidationContext(model, null, null);
            Validator.TryValidateObject(model, ctx, validationResults, true);

            return validationResults;
        }
    }
}
