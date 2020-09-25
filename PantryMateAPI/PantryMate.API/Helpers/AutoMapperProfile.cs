using PantryMate.API.Entities;
using PantryMate.API.Models.Request;
using PantryMate.API.Models.Response;

namespace PantryMate.API.Helpers
{
    public class AutoMapperProfile : AutoMapper.Profile
    {
        public AutoMapperProfile()
        {
            // from "" to ""

            CreateMap<Account, AccountResponse>();
            CreateMap<RegisterRequest, Account>();
            CreateMap<Entities.Profile, ProfileResponse>();
            CreateMap<UpdateProfileRequest, Entities.Profile>();

            CreateMap<CreateInventoryRequest, Inventory>();
            CreateMap<Inventory, InventoryResponse>();

            CreateMap<Item, ItemResponse>()
                .ForMember(e => e.Brand, opt => opt.MapFrom(e => e.Brand.Name));
        }
    }
}
