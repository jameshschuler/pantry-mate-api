using Microsoft.AspNetCore.Mvc;
using PantryMate.API.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PantryMate.API.Controllers
{
    [ApiController]
    [Route("api/v1/brand")]
    public class BrandController : BaseController
    {
        private readonly IBrandService _brandRepository;

        public BrandController(IBrandService brandRepository)
        {
            _brandRepository = brandRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetBrands()
        {
            var brands = await _brandRepository.GetBrands();
            return Ok(brands);
        }

        [HttpGet("{brandId:int:min(1)}")]
        public async Task<IActionResult> GetBrand(int brandId)
        {
            if (brandId <= 0)
            {
                return BadRequest();
            }

            var brand = await _brandRepository.GetBrand(brandId);

            if (brand == null)
            {
                return NotFound();
            }

            return Ok(brand);
        }
    }
}
