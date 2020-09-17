using AutoMapper;
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
    }
}
