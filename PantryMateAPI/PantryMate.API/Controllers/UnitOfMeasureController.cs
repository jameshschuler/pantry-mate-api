using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PantryMate.API.Models.Response;
using PantryMate.API.Services;

namespace PantryMate.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/v1/uom")]
    public class UnitOfMeasureController : BaseController
    {
        private readonly IUnitOfMeasureService _unitOfMeasureService;

        public UnitOfMeasureController(IUnitOfMeasureService unitOfMeasureService)
        {
            _unitOfMeasureService = unitOfMeasureService;
        }

        [HttpGet]
        public ActionResult<UnitOfMeasureResponse[]> GetAll()
        {
            if (Account == null)
            {
                return Unauthorized(new { message = "Unauthorized" });
            }

            var uoms = _unitOfMeasureService.GetUnitOfMeasures(Account.AccountId);

            return Ok(uoms);
        }
    }
}
