using AutoMapper;
using PantryMate.API.Entities;
using PantryMate.API.Models.Request;
using PantryMate.API.Models.Response;

namespace PantryMate.API.Helpers
{
    public class AutoMapperProfile : AutoMapper.Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Account, AccountResponse>();
            CreateMap<RegisterRequest, Account>();
            CreateMap<Entities.Profile, ProfileResponse>();
        }
    }
}
