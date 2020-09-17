using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PantryMate.API.Entities;
using PantryMate.API.Helpers;
using PantryMate.API.Models.Response;
using System;
using System.Linq;
using System.Threading.Tasks;
using BC = BCrypt.Net.BCrypt;

namespace PantryMate.API.Repositories
{
    public interface IAccountService
    {
        Task<Account> Authenticate(string username, string password);
        Task<Account> Create(Account account, string password);
        Task<AccountResponse> GetById(int accountId);
        Task<Account> VerifyAccount(int accountId);
    }

    public class AccountService : IAccountService
    {
        private readonly PantryMateContext _context;
        private readonly IMapper _mapper;

        public AccountService(PantryMateContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Account> Authenticate(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                return null;
            }

            var account = await _context.Account.SingleOrDefaultAsync(e => e.Username == username);

            // check if username exists
            if (account == null)
            {
                return null;
            }

            if (!account.Active)
            {
                throw new AppException("Account is inactive", account.AccountId);
            }

            // check if password is correct
            if (!BC.Verify(password, account.PasswordHash))
            {
                return null;
            }

            account.LastLogin = DateTime.Now;
            await _context.SaveChangesAsync();

            // authentication successful
            return account;
        }

        public async Task<Account> Create(Account account, string password)
        {
            // validation
            if (string.IsNullOrWhiteSpace(password))
            {
                throw new AppException("Password is required");
            }

            if (_context.Account.Any(e => e.Username == account.Username))
            {
                throw new AppException("Username \"" + account.Username + "\" is already taken");
            }

            account.Active = true;
            account.PasswordHash = BC.HashPassword(password);
            account.CreatedOn = DateTime.Now;
            account.Profile = new Entities.Profile { AccountID = account.AccountId };

            _context.Account.Add(account);

            await _context.SaveChangesAsync();

            return account;
        }

        public async Task<AccountResponse> GetById(int accountId)
        {
            var account = await _context.Account.Include(e => e.Profile).FirstOrDefaultAsync(e => e.AccountId == accountId);
            if (account != null)
            {
                return _mapper.Map<AccountResponse>(account);
            }

            return null;
        }

        public async Task<Account> VerifyAccount(int accountId)
        {
            return await _context.Account.FirstOrDefaultAsync(e => e.AccountId == accountId);
        }
    }
}
