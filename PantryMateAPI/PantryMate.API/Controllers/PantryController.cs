using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PantryMate.API.Models.Request;
using PantryMate.API.Models.Response;
using PantryMate.API.Services;
using System.Threading.Tasks;

namespace PantryMate.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/v1/pantry")]
    public class PantryController : BaseController
    {
        private readonly IPantryService _pantryService;

        public PantryController(IPantryService pantryService)
        {
            _pantryService = pantryService;
        }

        [HttpGet]
        public ActionResult<PantryResponse[]> GetAll()
        {
            if (Account == null)
            {
                return Unauthorized(new { message = "Unauthorized" });
            }

            var inventories = _pantryService.GetAll(Account.AccountId);

            return Ok(inventories);
        }

        [HttpGet("{pantryId:int:min(1)}")]
        public async Task<ActionResult<PantryResponse>> GetPantry(int pantryId)
        {
            if (Account == null)
            {
                return Unauthorized(new { message = "Unauthorized" });
            }

            var pantry = await _pantryService.GetPantry(Account.AccountId, pantryId);
            return Ok(pantry);
        }

        [HttpGet("{pantryId:int:min(1)}/item")]
        public async Task<ActionResult<ItemResponse[]>> GetPantryItems(int pantryId)
        {
            if (Account == null)
            {
                return Unauthorized(new { message = "Unauthorized" });
            }

            var pantryItems = await _pantryService.GetPantryItems(Account.AccountId, pantryId);
            return Ok(pantryItems);
        }

        [HttpPost]
        public async Task<IActionResult> CreatePantry(CreatePantryRequest request)
        {
            if (Account == null)
            {
                return Unauthorized(new { message = "Unauthorized" });
            }

            var pantry = await _pantryService.CreatePantry(Account.AccountId, request);

            return Created(string.Empty, pantry);
        }

        [HttpDelete("{pantryId:int:min(1)}")]
        public async Task<IActionResult> DeletePantry(int pantryId)
        {
            if (Account == null)
            {
                return Unauthorized(new { message = "Unauthorized" });
            }

            await _pantryService.DeletePantry(Account.AccountId, pantryId);

            return NoContent();
        }

        [HttpPut("{pantryId:int:min(1)}")]
        public async Task<IActionResult> UpdatePantry(int pantryId, UpdatePantryRequest request)
        {
            if (Account == null)
            {
                return Unauthorized(new { message = "Unauthorized" });
            }

            await _pantryService.UpdatePantry(Account.AccountId, pantryId, request);

            return NoContent();
        }

        [HttpPost("{pantryId:int:min(1)}/assignItems")]
        public async Task<IActionResult> AssignItemsToPantry(int pantryId, AssignItemRequest request)
        {
            if (Account == null)
            {
                return Unauthorized(new { message = "Unauthorized" });
            }

            var response = await _pantryService.AssignItemsToPantry(Account.AccountId, pantryId, request);
            return Ok(response);
        }

        [HttpPost("{pantryId:int:min(1)}/unassignItems")]
        public async Task<IActionResult> UnassignItemsFromPantry(int pantryId, UnassignItemRequest request)
        {
            if (Account == null)
            {
                return Unauthorized(new { message = "Unauthorized" });
            }

            var response = await _pantryService.UnassignItemsFromPantry(Account.AccountId, pantryId, request);
            return Ok(response);
        }

        [HttpPost("{pantryId:int:min(1)}/updateItems")]
        public async Task<IActionResult> UpdatePantryItem(int pantryId, UpdatePantryItemRequest request)
        {
            if (Account == null)
            {
                return Unauthorized(new { message = "Unauthorized" });
            }

            var response = await _pantryService.UpdatePantryItem(Account.AccountId, pantryId, request);
            return Ok(response);
        }
    }
}
