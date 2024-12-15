using Domain;
using Domain.Repository;

namespace Application.AccountFeatures
{
    public class AccountApplication : IAccountApplication
    {
        private readonly IAccountRepository _accountRepository;

        public AccountApplication(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public async Task<bool?> DepositAsync(decimal value, Guid userId, Guid accountId)
        {
            var account = await _accountRepository.GetByIdAsync(userId, accountId);

            if (account == null)
            {
                return null;
            }

            account.Deposit(value);

            await _accountRepository.DepositAsync(account);

            return true;
        }

        public async Task<decimal?> GetBalanceAsync(Guid userId, Guid accountId)
        {
            var account = await _accountRepository.GetByIdAsync(userId, accountId);

            if (account == null)
            {
                return null;
            }

            return account.Balance;
        }

        public async Task<IEnumerable<TransactionResponseDto>> GetTransactionsAsync(Guid userId, Guid accountId)
        {
            var account = await _accountRepository.GetByIdAsync(userId, accountId);

            if (account == null)
            {
                return null;
            }

            return account.Transactions.Select(x =>
                new TransactionResponseDto
                {
                    Id = x.Id,
                    Value = x.Value,
                    Date = x.Date,
                    TransactionResponseDetails = new TransactionResponseDetailsDto
                    {
                        TransactionType = x.TransactionType.ToString(),
                        Sender = x.TransactionType == TransactionType.Transfer ? ((Transfer)x).Sender.GetAccountDescription() : null,
                        Destination = x.TransactionType == TransactionType.Transfer ? ((Transfer)x).Destination.GetAccountDescription() : null,
                        TransferType = x.TransactionType == TransactionType.Transfer ? ((Transfer)x).TransferType.ToString() : null,
                    },
                }
            );
        }

        public async Task<bool?> TransferAsync(decimal value, Guid userId, Guid senderAccountId, Guid destinationAccountId)
        {
            var senderAccount = await _accountRepository.GetByIdAsync(userId, senderAccountId);

            if (senderAccount == null)
            {
                return null;
            }

            var destinationAccount = await _accountRepository.GetByIdAsync(Guid.Empty, destinationAccountId);

            if (destinationAccount == null)
            {
                return null;
            }

            senderAccount.Transfer(value, destinationAccount);

            await _accountRepository.TransferAsync(senderAccount, destinationAccount);

            return true;
        }

        public async Task<bool?> WithdrawAsync(decimal value, Guid userId, Guid accountId)
        {
            var account = await _accountRepository.GetByIdAsync(userId, accountId);

            if (account == null)
            {
                return null;
            }

            account.Withdraw(value);

            await _accountRepository.WithdrawAsync(account);

            return true;
        }

    }
}
