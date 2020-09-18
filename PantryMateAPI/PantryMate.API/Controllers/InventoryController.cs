using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PantryMate.API.Models.Request;
using PantryMate.API.Models.Response;
using PantryMate.API.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PantryMate.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/v1/inventory")]
    public class InventoryController : BaseController
    {
        private readonly IInventoryService _inventoryService;

        public InventoryController(IInventoryService inventoryService)
        {
            _inventoryService = inventoryService;
        }

        [HttpGet]
        public ActionResult<InventoryResponse[]> GetAll()
        {
            if (Account == null)
            {
                return Unauthorized(new { message = "Unauthorized" });
            }

            var inventories = _inventoryService.GetAll(Account.AccountId);

            return Ok(inventories);
        }

        [HttpGet("{inventoryId}")]
        public async Task<ActionResult<InventoryResponse>> GetInventory(int inventoryId)
        {
            if (Account == null)
            {
                return Unauthorized(new { message = "Unauthorized" });
            }

            var inventory = await _inventoryService.GetInventory(Account.AccountId, inventoryId);

            if (inventory == null)
            {
                return NotFound(new { message = $"Inventory not found for id: {inventoryId}" });
            }

            return Ok(inventory);
        }

        [HttpPost]
        public async Task<IActionResult> CreateInventory(CreateInventoryRequest request)
        {
            if (Account == null)
            {
                return Unauthorized(new { message = "Unauthorized" });
            }

            var inventory = await _inventoryService.CreateInventory(Account.AccountId, request);

            return Created(string.Empty, inventory);
        }

        [HttpDelete("{inventoryId}")]
        public async Task<IActionResult> DeleteInventory(int inventoryId)
        {
            if (Account == null)
            {
                return Unauthorized(new { message = "Unauthorized" });
            }

            await _inventoryService.DeleteInventory(Account.AccountId, inventoryId);

            return NoContent();
        }
    }
}
