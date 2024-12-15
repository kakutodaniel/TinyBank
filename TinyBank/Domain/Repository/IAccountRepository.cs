namespace Domain.Repository
{
    public interface IAccountRepository
    {

        Task DepositAsync(Account account);

        Task WithdrawAsync(Account account);

        Task TransferAsync(Account sender, Account destination);

        Task<Account> GetByIdAsync(Guid userId, Guid id);
    }
}
