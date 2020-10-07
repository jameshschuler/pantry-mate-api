using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PantryMate.API.Controllers;
using PantryMate.API.Entities;
using PantryMate.API.Helpers;
using PantryMate.API.Models.Request;
using PantryMate.API.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PantryMate.API.Services
{
    public interface IInventoryService
    {
        Task<AssignItemResponse> AssignItemsToInventory(int accountId, int inventoryId, AssignItemRequest request);
        Task<InventoryResponse> CreateInventory(int accountId, CreateInventoryRequest request);
        Task DeleteInventory(int accountId, int inventoryId);
        IEnumerable<InventoryResponse> GetAll(int accountId);
        Task<InventoryResponse> GetInventory(int accountId, int inventoryId);
        Task<IEnumerable<ItemResponse>> GetInventoryItems(int accountId, int inventoryId);
    }

    public class InventoryService : IInventoryService
    {
        private readonly PantryMateContext _context;
        private readonly IMapper _mapper;

        public InventoryService(PantryMateContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<AssignItemResponse> AssignItemsToInventory(int accountId, int inventoryId, AssignItemRequest request)
        {
            var inventory = await GetUserInventory(accountId, inventoryId, true);
            var items = _context.Item.Where(e => request.ItemIds.Contains(e.ItemId));

            var assignedItemIds = inventory.InventoryItems.Select(e => e.ItemId);
            var assignedItemCount = 0;
            
            foreach(var item in items)
            {
                if (!assignedItemIds.Contains(item.ItemId))
                {
                    inventory.InventoryItems.Add(new InventoryItem
                    {
                        ItemId = item.ItemId,
                        InventoryId = inventory.InventoryId
                    });

                    assignedItemCount++;
                }
            }

            if (assignedItemCount == 0)
            {
                throw new AppException("No items were assigned.");
            }

            await _context.SaveChangesAsync();

            return new AssignItemResponse
            {
                TotalItemCount = request.ItemIds.Length,
                AssignedItemCount = assignedItemCount
            };
        }

        public async Task<InventoryResponse> CreateInventory(int accountId, CreateInventoryRequest request)
        {
            var existingUserInventories = _context.Inventory.Where(e => e.AccountId == accountId);

            // Check if the new inventory name is required
            var nameIsTaken = existingUserInventories.Any(e => e.Name == request.Name);
            if (nameIsTaken)
            {
                throw new AppException($"Inventory name '{request.Name}' is already taken");
            }

            var inventory = _mapper.Map<Inventory>(request);
            inventory.AccountId = accountId;

            _context.Inventory.Add(inventory);
            await _context.SaveChangesAsync();

            return _mapper.Map<InventoryResponse>(inventory);
        }

        public async Task DeleteInventory(int accountId, int inventoryId)
        {
            var inventory = await GetUserInventory(accountId, inventoryId);

            _context.Inventory.Remove(inventory);
            await _context.SaveChangesAsync();
        }

        public IEnumerable<InventoryResponse> GetAll(int accountId)
        {
            var inventories = _context.Inventory.Where(e => e.AccountId == accountId);
            return _mapper.Map<IEnumerable<InventoryResponse>>(inventories);
        }

        public async Task<InventoryResponse> GetInventory(int accountId, int inventoryId)
        {
            var inventory = await GetUserInventory(accountId, inventoryId);

            return _mapper.Map<InventoryResponse>(inventory);
        }

        public async Task<IEnumerable<ItemResponse>> GetInventoryItems(int accountId, int inventoryId)
        {
            var inventory = await GetUserInventory(accountId, inventoryId, true, true);
            var items = inventory.InventoryItems.Select(e => e.Item);
            
            return _mapper.Map<IEnumerable<ItemResponse>>(items);
        }

        private async Task<Inventory> GetUserInventory(int accountId, int inventoryId, bool includeInventoryItems = false, bool includeInventoryItemItem = false)
        {
            Inventory inventory;
            var includedProperties = string.Empty;

            if (includeInventoryItems)
            {
                var s = "InventoryItems";

                if (includeInventoryItemItem)
                {
                    s = "InventoryItems.Item.Brand";
                }

                includedProperties = s;
                inventory = await _context.Inventory.Include(includedProperties).FirstOrDefaultAsync(e => e.AccountId == accountId && e.InventoryId == inventoryId);
            }
            else
            {
                inventory = await _context.Inventory.FirstOrDefaultAsync(e => e.AccountId == accountId && e.InventoryId == inventoryId);
            }

            if (inventory == null)
            {
                throw new KeyNotFoundException($"Inventory not found for id: {inventoryId}");
            }

            return inventory;
        }
    }
}
