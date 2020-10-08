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
    public interface IPantryService
    {
        Task<AssignItemResponse> AssignItemsToPantry(int accountId, int pantryId, AssignItemRequest request);
        Task<PantryResponse> CreatePantry(int accountId, CreatePantryRequest request);
        Task DeletePantry(int accountId, int pantryId);
        IEnumerable<PantryResponse> GetAll(int accountId);
        Task<PantryResponse> GetPantry(int accountId, int pantryId);
        Task<IEnumerable<ItemResponse>> GetPantryItems(int accountId, int pantryId);
    }

    public class PantryService : IPantryService
    {
        private readonly PantryMateContext _context;
        private readonly IMapper _mapper;

        public PantryService(PantryMateContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<AssignItemResponse> AssignItemsToPantry(int accountId, int pantryId, AssignItemRequest request)
        {
            var pantry = await GetUserPantry(accountId, pantryId, true);
            var items = _context.Item.Where(e => request.ItemIds.Contains(e.ItemId));

            var assignedItemIds = pantry.PantryItems.Select(e => e.ItemId);
            var assignedItemCount = 0;
            
            foreach(var item in items)
            {
                if (!assignedItemIds.Contains(item.ItemId))
                {
                    pantry.PantryItems.Add(new PantryItem
                    {
                        ItemId = item.ItemId,
                        PantryId = pantry.PantryId
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

        public async Task<PantryResponse> CreatePantry(int accountId, CreatePantryRequest request)
        {
            var existingUserInventories = _context.Pantry.Where(e => e.AccountId == accountId);

            // Check if the new pantry name is unique
            var nameIsTaken = existingUserInventories.Any(e => e.Name == request.Name);
            if (nameIsTaken)
            {
                throw new AppException($"Pantry name '{request.Name}' is already taken.");
            }

            var pantry = _mapper.Map<Pantry>(request);
            pantry.AccountId = accountId;

            _context.Pantry.Add(pantry);
            await _context.SaveChangesAsync();

            return _mapper.Map<PantryResponse>(pantry);
        }

        public async Task DeletePantry(int accountId, int pantryId)
        {
            var pantry = await GetUserPantry(accountId, pantryId);

            _context.Pantry.Remove(pantry);
            await _context.SaveChangesAsync();
        }

        public IEnumerable<PantryResponse> GetAll(int accountId)
        {
            var pantries = _context.Pantry.Where(e => e.AccountId == accountId);
            return _mapper.Map<IEnumerable<PantryResponse>>(pantries);
        }

        public async Task<PantryResponse> GetPantry(int accountId, int pantryId)
        {
            var pantry = await GetUserPantry(accountId, pantryId);

            return _mapper.Map<PantryResponse>(pantry);
        }

        public async Task<IEnumerable<ItemResponse>> GetPantryItems(int accountId, int pantryId)
        {
            var pantry = await GetUserPantry(accountId, pantryId, true, true);
            var items = pantry.PantryItems.Select(e => e.Item);
            
            return _mapper.Map<IEnumerable<ItemResponse>>(items);
        }

        private async Task<Pantry> GetUserPantry(int accountId, int pantryId, bool includePantryItems = false, bool includePantryItemItem = false)
        {
            Pantry pantry;
            var includedProperties = string.Empty;

            if (includePantryItems)
            {
                var s = "PantryItems";

                if (includePantryItemItem)
                {
                    s = "PantryItems.Item.Brand";
                }

                includedProperties = s;
                pantry = await _context.Pantry.Include(includedProperties).FirstOrDefaultAsync(e => e.AccountId == accountId && e.PantryId == pantryId);
            }
            else
            {
                pantry = await _context.Pantry.FirstOrDefaultAsync(e => e.AccountId == accountId && e.PantryId == pantryId);
            }

            if (pantry == null)
            {
                throw new KeyNotFoundException($"Pantry not found for id: {pantryId}");
            }

            return pantry;
        }
    }
}
