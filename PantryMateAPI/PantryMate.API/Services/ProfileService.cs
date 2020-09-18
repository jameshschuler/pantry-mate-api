using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PantryMate.API.Models.Request;
using PantryMate.API.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PantryMate.API.Services
{
    public interface IProfileService
    {
        Task<ProfileResponse> UpdateProfile(int accountId, UpdateProfileRequest request);
    }

    public class ProfileService : IProfileService
    {
        private readonly PantryMateContext _context;
        private readonly IMapper _mapper;

        public ProfileService(PantryMateContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ProfileResponse> UpdateProfile(int accountId, UpdateProfileRequest request)
        {
            var profile = await GetProfile(accountId);

            _mapper.Map(request, profile);

            _context.Profile.Update(profile);
            await _context.SaveChangesAsync();

            return _mapper.Map<ProfileResponse>(profile);
        }

        private async Task<Entities.Profile> GetProfile(int accountId)
        {
            var profile = await _context.Profile.FirstOrDefaultAsync(e => e.AccountID == accountId);
            if (profile == null)
            {
                throw new KeyNotFoundException("Profile not found");
            }

            return profile;
        }
    }
}
