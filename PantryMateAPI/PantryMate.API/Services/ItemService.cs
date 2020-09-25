using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PantryMate.API.Models.Response;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PantryMate.API.Services
{
    public interface IItemService
    {
        /*Task<InventoryResponse> CreateInventory(int accountId, CreateInventoryRequest request);
        Task DeleteInventory(int accountId, int inventoryId);*/
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
