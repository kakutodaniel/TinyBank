using Domain;
using Domain.Repository;
using Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Security.Principal;

namespace Infrastructure.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly BankContext _bankContext;
        private readonly IAccountRepository _accountRepository;

        public UserRepository(BankContext bankContext, IAccountRepository accountRepository)
        {
            _bankContext = bankContext;
            _accountRepository = accountRepository;
        }

        public async Task CreateAsync(User user)
        {
            // TODO: we should create a mapper
            var userModel = new UserModel
            {
                Id = user.Id,
                Name = user.Name,
                UserType = user.UserType.GetHashCode(),
                Document = user.Document,
                Country = user.Country,
                Active = user.Active,
                Account = new AccountModel
                {
                    Id = user.Account.Id,
                    Balance = user.Account.Balance
                }
            };

            _bankContext.Users.Add(userModel);

            // TODO: we should create an UnitOfWork pattern to save from outside
            await _bankContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(User user)
        {
            var userModel = new UserModel
            {
                Id = user.Id,
                Active = user.Active,
            };

            _bankContext.Users.Attach(userModel).Property(x => x.Active).IsModified = true;

            // TODO: we should create an UnitOfWork pattern to save from outside
            await _bankContext.SaveChangesAsync();
        }

        public async Task<User> GetAsync(Guid id)
        {
            var userModel = await _bankContext
                .Users
                .AsNoTracking()
                .Include(x => x.Account)
                .AsNoTracking()
                .SingleOrDefaultAsync(x => x.Id == id);

            if (userModel == null)
            {
                return null;
            }

            var account = new Account(userModel.Account.Id, userModel.Account.Balance);

            var user = new User(userModel.Id, userModel.Name, (UserType)userModel.UserType, userModel.Document, userModel.Country, userModel.Active, account);

            return user;
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            var userModel = await _bankContext
                .Users.AsNoTracking()
                .Include(x => x.Account)
                .AsNoTracking()
                .ToListAsync();

            if (userModel == null)
            {
                return null;
            }

            return userModel.Select(x => new User(x.Id, x.Name, (UserType)x.UserType, x.Document, x.Country, x.Active, new Account(x.Account.Id, x.Account.Balance)));
        }
    }
}
