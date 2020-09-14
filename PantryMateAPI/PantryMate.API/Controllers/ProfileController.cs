using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PantryMate.API.Repositories;

namespace PantryMate.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/v1/profile")]
    public class ProfileController : BaseController
    {
        private readonly IAccountService _accountService;
        private readonly IMapper _mapper;

        public ProfileController(IAccountService accountService, IMapper mapper)
        {
            _accountService = accountService;
            _mapper = mapper;
        }
    }
}
