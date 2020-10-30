using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PantryMate.API.Entities;
using PantryMate.API.Models.Request;
using PantryMate.API.Models.Response;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PantryMate.API.Services
{
    public interface IItemService
    {
        Task<ItemResponse> CreateItem(int accountId, CreateItemRequest request);
        Task DeleteItem(int accountId, int pantryId);
        IEnumerable<ItemResponse> GetAll(int accountId);
        Task<ItemResponse> GetItem(int accountId, int itemId);
        Task<ItemResponse> UpdateItem(int accountId, int itemId, UpdateItemRequest request);
    }

    public class ItemService : IItemService
    {
        private readonly PantryMateContext _context;
        private readonly IMapper _mapper;

        public ItemService(PantryMateContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ItemResponse> CreateItem(int accountId, CreateItemRequest request)
        {
            Brand brand = null;
            if ( request.BrandId != null)
            {
                brand = await _context.Brand.FirstOrDefaultAsync(e => e.BrandId == request.BrandId);
                if (brand == null)
                {
                    throw new KeyNotFoundException($"Brand not found for id: {request.BrandId}");
                }
            }

            UnitOfMeasure uom = null;
            if (request.UnitOfMeasureId != null)
            {
                uom = await _context.UnitOfMeasure.FirstOrDefaultAsync(e => e.AccountId == accountId && e.UnitOfMeasureId == request.UnitOfMeasureId);
                if (uom == null)
                {
                    throw new KeyNotFoundException($"Unit of Measure not found for id: {request.UnitOfMeasureId}");
                }
            }

            var item = _mapper.Map<Item>(request);
            item.AccountId = accountId;

            _context.Item.Add(item);
            await _context.SaveChangesAsync();

            item.Brand = brand;
            item.UnitOfMeasure = uom ?? new UnitOfMeasure();

            return _mapper.Map<ItemResponse>(item);
        }

        public async Task DeleteItem(int accountId, int itemId)
        {
            var item = await GetUserItem(accountId, itemId);

            _context.Item.Remove(item);
            await _context.SaveChangesAsync();
        }

        public IEnumerable<ItemResponse> GetAll(int accountId)
        {
            var items = _context.Item.Where(e => e.AccountId == accountId)
                .Include(e => e.Brand).Include(e => e.UnitOfMeasure);

            return _mapper.Map<IEnumerable<ItemResponse>>(items);
        }

        public async Task<ItemResponse> GetItem(int accountId, int itemId)
        {
            var item = await _context.Item
                .Include(e => e.Brand)
                .Include(e => e.UnitOfMeasure)
                .FirstOrDefaultAsync(e => e.AccountId == accountId && e.ItemId == itemId);
            
            if (item == null)
            {
                throw new KeyNotFoundException($"Item not found for id: {itemId}");
            }

            return _mapper.Map<ItemResponse>(item);
        }

        public async Task<ItemResponse> UpdateItem(int accountId, int itemId, UpdateItemRequest request)
        {
            var item = await GetUserItem(accountId, itemId);

            if (request.BrandId != null && request.BrandId != item.BrandId)
            {
                var brand = await _context.Brand.FirstOrDefaultAsync(e => e.BrandId == request.BrandId);
                if (brand == null)
                {
                    throw new KeyNotFoundException($"Brand not found for id: {request.BrandId}");
                }
            }

            if (request.UnitOfMeasureId != null && request.UnitOfMeasureId != item.UnitOfMeasureId)
            {
                var uom = await _context.UnitOfMeasure.FirstOrDefaultAsync(e => e.AccountId == accountId && e.UnitOfMeasureId == request.UnitOfMeasureId);
                if (uom == null)
                {
                    throw new KeyNotFoundException($"Unit of Measure not found for id: {request.UnitOfMeasureId}");
                }
            }

            _mapper.Map(request, item);

            _context.Item.Update(item);
            await _context.SaveChangesAsync();

            return _mapper.Map<ItemResponse>(item);
        }

        private async Task<Item> GetUserItem(int accountId, int itemId)
        {
            var item = await _context.Item.FirstOrDefaultAsync(e => e.AccountId == accountId && e.ItemId == itemId);

            if (item == null)
            {
                throw new KeyNotFoundException($"Item not found for id: {itemId}");
            }

            return item;
        }
    }
}
