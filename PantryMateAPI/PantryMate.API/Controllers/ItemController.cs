using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PantryMate.API.Models.Request;
using PantryMate.API.Models.Response;
using PantryMate.API.Repositories;
using PantryMate.API.Services;
using System.Threading;
using System.Threading.Tasks;


namespace PantryMate.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/v1/item")]
    public class ItemController : BaseController
    {
        private readonly IItemService _itemService;

        public ItemController(IItemService itemService)
        {
            _itemService = itemService;
        }

        [HttpGet]
        public ActionResult<ItemResponse[]> GetAll()
        {
            if (Account == null)
            {
                return Unauthorized(new { message = "Unauthorized" });
            }

            var items = _itemService.GetAll(Account.AccountId);

            return Ok(items);
        }

        [HttpGet("{itemId:int:min(1)}")]
        public async Task<ActionResult<InventoryResponse>> GetItem(int itemId)
        {
            if (Account == null)
            {
                return Unauthorized(new { message = "Unauthorized" });
            }

            var item = await _itemService.GetItem(Account.AccountId, itemId);
            return Ok(item);
        }

        // TODO: Delete item

        [HttpPost]
        public async Task<IActionResult> CreateItem(CreateItemRequest request)
        {
            if (Account == null)
            {
                return Unauthorized(new { message = "Unauthorized" });
            }

            var item = await _itemService.CreateItem(Account.AccountId, request);

            return Created(string.Empty, item);
        }

        // TODO: update item

        // TODO: assign item to an inventory
    }
}
