using Microsoft.AspNetCore.Mvc;
using PantryMate.API.Entities;
using PantryMate.API.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PantryMate.API.Controllers
{
    [ApiController]
    [Route("api/v1/brand")]
    public class BrandController : BaseController
    {
        private readonly IBrandService _brandService;

        public BrandController(IBrandService brandService)
        {
            _brandService = brandService;
        }

        [HttpGet]
        public async Task<ActionResult<Brand[]>> GetBrands()
        {
            var brands = await _brandService.GetBrands();
            return Ok(brands);
        }

        [HttpGet("{brandId:int:min(1)}")]
        public async Task<ActionResult<Brand>> GetBrand(int brandId)
        {
            if (brandId <= 0)
            {
                return BadRequest();
            }

            var brand = await _brandService.GetBrand(brandId);

            if (brand == null)
            {
                return NotFound();
            }

            return Ok(brand);
        }
    }
}
