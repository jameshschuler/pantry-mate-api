using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using PantryMate.API.Entities;
using PantryMate.API.Helpers;
using PantryMate.API.Models.Request;
using PantryMate.API.Models.Response;
using PantryMate.API.Repositories;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace PantryMate.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/v1/account")]
    public class AccountController : BaseController
    {
        private readonly IAccountService _accountService;
        private readonly IMapper _mapper;
        private readonly AppSettings _appSettings;

        public AccountController(IAccountService accountService, IMapper mapper, IOptions<AppSettings> appSettings)
        {
            _accountService = accountService;
            _mapper = mapper;
            _appSettings = appSettings.Value;
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate([FromBody] AuthenticateRequest request)
        {
            try
            {
                var account = await _accountService.Authenticate(request.Username, request.Password);

                if (account == null)
                {
                    return BadRequest(new { message = "Username or password is incorrect" });
                }

                // return basic user info and authentication token
                return Ok(new
                {
                    AccountId = account.AccountId,
                    Username = account.Username,
                    Token = GenerateToken(account.AccountId),
                });
            }
            catch (AppException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("{accountId}")]
        public async Task<ActionResult<AccountResponse>> GetById(int accountId)
        {
            if (Account == null || Account.AccountId != accountId)
            {
                return Unauthorized(new { message = "Unauthorized" });
            }

            if (accountId <= 0)
            {
                return BadRequest();
            }

            var account = await _accountService.GetById(accountId);

            if (account == null)
            {
                return NotFound();
            }

            return Ok(account);
        }

        [AllowAnonymous]
        [HttpPost("register", Name = "Register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            if (request == null)
            {
                return BadRequest();
            }

            // map model to entity
            var account = _mapper.Map<Account>(request);

            try
            {
                await _accountService.Create(account, request.Password);
                return Ok();
            }
            catch (AppException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        private string GenerateToken(int accountId)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                        new Claim(ClaimTypes.Name, accountId.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            return tokenString;
        }
    }
}
