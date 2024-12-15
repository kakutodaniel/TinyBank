using Domain;
using Domain.Repository;

namespace Application.UserFeatures
{
    public class UserApplication : IUserApplication
    {
        private readonly IUserRepository _userRepository;

        public UserApplication(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<Guid> CreateAsync(CreateUserDto dto)
        {
            var account = new Account(Guid.NewGuid(), 0);
            var user = new User(Guid.NewGuid(), dto.Name, (UserType)dto.UserType.GetHashCode(), dto.Document, dto.Country, true, account);

            await _userRepository.CreateAsync(user);

            return user.Id;
        }

        public async Task<bool?> DeactivateAsync(Guid id)
        {
            var user = await _userRepository.GetAsync(id);

            if (user == null)
            {
                return null;
            }

            user.Deactivate();

            await _userRepository.UpdateAsync(user);

            return true;
        }

        public async Task<IEnumerable<UserResponseDto>> GetAllAsync()
        {
            var users = await _userRepository.GetAllAsync();

            if (users == null)
            {
                return null;
            }

            return users.Select(x => new UserResponseDto
            {
                Id = x.Id,
                Name = x.Name,
                UserType = x.UserType.ToString(),
                Document = x.Document,
                Country = x.Country,
                Active = x.Active,
                Account = new AccountResponseDto
                {
                    Id = x.Account.Id,
                    Balance = x.Account.Balance,
                }
            });
        }

        public async Task<UserResponseDto> GetByIdAsync(Guid id)
        {
            var user = await _userRepository.GetAsync(id);

            if (user == null)
            {
                return null;
            }

            return new UserResponseDto
            {
                Id = user.Id,
                Name = user.Name,
                UserType = user.UserType.ToString(),
                Document = user.Document,
                Country = user.Country,
                Active = user.Active,
                Account = new AccountResponseDto
                {
                    Id = user.Account.Id,
                    Balance = user.Account.Balance,
                }
            };
        }
    }
}
