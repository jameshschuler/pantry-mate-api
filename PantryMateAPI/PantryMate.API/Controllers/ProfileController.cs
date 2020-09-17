using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PantryMate.API.Models.Request;
using PantryMate.API.Models.Response;
using PantryMate.API.Repositories;
using PantryMate.API.Services;
using System.Threading;
using System.Threading.Tasks;

namespace PantryMate.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/v1/profile")]
    public class ProfileController : BaseController
    {
        private readonly IProfileService _profileService;

        public ProfileController(IProfileService profileService)
        {
            _profileService = profileService;
        }

        [HttpPut("{accountId}")]
        public async Task<ActionResult<ProfileResponse>> UpdateProfile(int accountId, UpdateProfileRequest request)
        {
            if (Account == null || accountId != Account.AccountId)
            {
                return Unauthorized(new { message = "Unauthorized" });
            }

            var profile = await _profileService.UpdateProfile(accountId, request);

            return Ok(profile);
        }
    }
}
