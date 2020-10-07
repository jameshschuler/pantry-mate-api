using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PantryMate.API.Controllers;
using PantryMate.API.Entities;
using PantryMate.API.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PantryMate.API.Test
{
    [TestClass]
    public class Should_GetBrands
    {
        private Mock<IBrandService> _mockBrandService;

        [TestInitialize]
        public void Initialize()
        {
            _mockBrandService = new Mock<IBrandService>();
            _mockBrandService.Setup(e => e.GetBrands())
                .ReturnsAsync(GetBrands());
        }

        [TestMethod]
        public async Task ShouldGetAllBrands()
        {
            var controller = new BrandController(_mockBrandService.Object);

            var actionResult = await controller.GetBrands();
            var result = actionResult.Result as OkObjectResult;

            Assert.AreEqual(2, ((List<Brand>)result.Value).Count);
            Assert.AreEqual(200, result.StatusCode);
        }

        [TestMethod]
        public async Task ShouldGetBrandById()
        {
            _mockBrandService.Setup(e => e.GetBrand(1))
                .ReturnsAsync(new Brand() { BrandId = 1 });

            var controller = new BrandController(_mockBrandService.Object);

            var actionResult = await controller.GetBrand(1);
            var result = actionResult.Result as OkObjectResult;

            Assert.IsNotNull(result.Value);
            Assert.AreEqual(1, ((Brand)result.Value).BrandId);
            Assert.AreEqual(200, result.StatusCode);
        }

        [TestMethod]
        public async Task ShouldGetBrandById_NotFound()
        {
            _mockBrandService.Setup(e => e.GetBrand(1))
                .ReturnsAsync(new Brand() { BrandId = 1 });

            var controller = new BrandController(_mockBrandService.Object);

            var actionResult = await controller.GetBrand(2);
            var result = actionResult.Result as StatusCodeResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(404, result.StatusCode);
        }

        private List<Brand> GetBrands()
        {
            return new List<Brand>
            { 
                new Brand
                {
                    BrandId = 1
                },
                new Brand
                {
                    BrandId = 2
                }
            };
        }
    }
}
