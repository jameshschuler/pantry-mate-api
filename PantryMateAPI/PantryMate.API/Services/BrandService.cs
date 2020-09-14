using Microsoft.EntityFrameworkCore;
using PantryMate.API.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PantryMate.API.Repositories
{
    public interface IBrandService
    {
        Task<Brand> GetBrand(int brandId);
        Task<IEnumerable<Brand>> GetBrands();
    }

    public class BrandService : IBrandService
    {
        private readonly PantryMateContext _context;

        public BrandService(PantryMateContext context)
        {
            _context = context;
        }

        public async Task<Brand> GetBrand(int brandId)
        {
            return await _context.Brand.FirstOrDefaultAsync(e => e.BrandId == brandId);
        }

        public async Task<IEnumerable<Brand>> GetBrands()
        {
            return await _context.Brand.ToListAsync();
        }
    }
}
