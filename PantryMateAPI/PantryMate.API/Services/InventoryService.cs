using AutoMapper;
using Microsoft.EntityFrameworkCore;
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
        Task<InventoryResponse> CreateInventory(int accountId, CreateInventoryRequest request);
        Task DeleteInventory(int accountId, int inventoryId);
        IList<InventoryResponse> GetAll(int accountId);
        Task<InventoryResponse> GetInventory(int accountId, int inventoryId);
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
            var inventory = await _context.Inventory.FirstOrDefaultAsync(e => e.AccountId == accountId && e.InventoryId == inventoryId);

            if (inventory == null)
            {
                throw new KeyNotFoundException($"Inventory not found for id: {inventoryId}");
            }

            _context.Inventory.Remove(inventory);
            await _context.SaveChangesAsync();
        }

        public IList<InventoryResponse> GetAll(int accountId)
        {
            var inventories = _context.Inventory.Where(e => e.AccountId == accountId);
            return _mapper.Map<IList<InventoryResponse>>(inventories);
        }

        public async Task<InventoryResponse> GetInventory(int accountId, int inventoryId)
        {
            var inventory = await _context.Inventory.FirstOrDefaultAsync(e => e.AccountId == accountId && e.InventoryId == inventoryId);

            return _mapper.Map<InventoryResponse>(inventory);
        }
    }
}
