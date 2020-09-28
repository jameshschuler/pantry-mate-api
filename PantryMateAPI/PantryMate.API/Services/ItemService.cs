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
        /*Task DeleteInventory(int accountId, int inventoryId);*/
        IEnumerable<ItemResponse> GetAll(int accountId);
        Task<ItemResponse> GetItem(int accountId, int itemId);
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

            var item = _mapper.Map<Item>(request);
            item.AccountId = accountId;

            _context.Item.Add(item);
            await _context.SaveChangesAsync();

            item.Brand = brand;

            return _mapper.Map<ItemResponse>(item);
        }

        public IEnumerable<ItemResponse> GetAll(int accountId)
        {
            var items = _context.Item.Where(e => e.AccountId == accountId).Include(e => e.Brand);

            return _mapper.Map<IEnumerable<ItemResponse>>(items);
        }

        public async Task<ItemResponse> GetItem(int accountId, int itemId)
        {
            var item = await _context.Item.Include(e => e.Brand).FirstOrDefaultAsync(e => e.AccountId == accountId && e.ItemId == itemId);
            
            if (item == null)
            {
                throw new KeyNotFoundException($"Item not found for id: {itemId}");
            }

            return _mapper.Map<ItemResponse>(item);
        }
    }
}
