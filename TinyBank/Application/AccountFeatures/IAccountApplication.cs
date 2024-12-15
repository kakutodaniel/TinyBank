namespace Application.AccountFeatures
{
    public interface IAccountApplication
    {
        Task<bool?> DepositAsync(decimal value, Guid userId, Guid accountId);

        Task<bool?> WithdrawAsync(decimal value, Guid userId, Guid accountId);

        Task<bool?> TransferAsync(decimal value, Guid userId, Guid senderAccountId, Guid destinationAccountId);

        Task<decimal?> GetBalanceAsync(Guid userId, Guid accountId);

        Task<IEnumerable<TransactionResponseDto>> GetTransactionsAsync(Guid userId, Guid accountId);

    }
}
