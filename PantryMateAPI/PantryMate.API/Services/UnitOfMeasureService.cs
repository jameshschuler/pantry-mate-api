using AutoMapper;
using PantryMate.API.Models.Response;
using System.Collections.Generic;
using System.Linq;

namespace PantryMate.API.Services
{
    public interface IUnitOfMeasureService
    {
        IEnumerable<UnitOfMeasureResponse> GetUnitOfMeasures(int accountId);
    }

    public class UnitOfMeasureService : IUnitOfMeasureService
    {
        private readonly PantryMateContext _context;
        private readonly IMapper _mapper;

        public UnitOfMeasureService(PantryMateContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public IEnumerable<UnitOfMeasureResponse> GetUnitOfMeasures(int accountId)
        {
            var uoms = _context.UnitOfMeasure.Where(e => e.AccountId == accountId);

            return _mapper.Map<IEnumerable<UnitOfMeasureResponse>>(uoms);
        }
    }
}
