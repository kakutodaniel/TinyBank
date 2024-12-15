namespace Domain.Repository
{
    public interface IUserRepository
    {
        Task CreateAsync(User user);

        Task<User> GetAsync(Guid id);

        Task UpdateAsync(User user);

        Task<IEnumerable<User>> GetAllAsync();
    }
}
