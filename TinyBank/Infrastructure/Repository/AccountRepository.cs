using Domain;
using Domain.Repository;
using Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class AccountRepository : IAccountRepository
    {
        private readonly BankContext _bankContext;

        public AccountRepository(BankContext bankContext)
        {
            _bankContext = bankContext;
        }

        public async Task DepositAsync(Account account)
        {
            await DepositOrWithdrawAsync(account);
        }

        public async Task<Account> GetByIdAsync(Guid userId, Guid id)
        {
            var queryAccountModel = _bankContext.Accounts
                .Include(x => x.User)
                .Include(x => x.Transactions)
                .Where(x => x.Id == id);

            if (userId != Guid.Empty)
            {
                queryAccountModel = queryAccountModel.Where(x => x.UserId == userId);
            }

            var accountModel = await queryAccountModel.SingleOrDefaultAsync();

            if (accountModel == null)
            {
                return null;
            }

            var user = new User(accountModel.User.Id, accountModel.User.Name, (UserType)accountModel.User.UserType, accountModel.User.Document, accountModel.User.Document, accountModel.User.Active);

            var transactions = new List<Transaction>();

            accountModel.Transactions?.ForEach(async x =>
            {
                switch (x.TransactionType)
                {
                    case (int)TransactionType.Transfer:
                        var senderModel = await _bankContext.Users.Include(x => x.Account).SingleAsync(y => y.Account.Id == x.AccountSenderId.Value);
                        var destinationModel = await _bankContext.Users.Include(x => x.Account).SingleAsync(y => y.Account.Id == x.AccountDestinationId.Value);

                        var userSender = new User(senderModel.Id, senderModel.Name, (UserType)senderModel.UserType, senderModel.Document, senderModel.Country, senderModel.Active);
                        var accountSender = new Account(senderModel.Account.Id, senderModel.Account.Balance, userSender);

                        var userDestination = new User(destinationModel.Id, destinationModel.Name, (UserType)destinationModel.UserType, destinationModel.Document, destinationModel.Country, destinationModel.Active);
                        var accountDestination = new Account(destinationModel.Account.Id, destinationModel.Account.Balance, userDestination);

                        transactions.Add(new Transfer(x.Id, x.Value, accountSender, accountDestination, x.Date, (TransferType)x.TransferType.GetValueOrDefault()));
                        break;
                    case (int)TransactionType.Deposit:
                        transactions.Add(new Deposit(x.Id, x.Value, x.Date));
                        break;
                    case (int)TransactionType.Withdraw:
                        transactions.Add(new Withdraw(x.Id, x.Value, x.Date));
                        break;
                }
            });

            var account = new Account(accountModel.Id, accountModel.Balance, user, transactions);

            return account;
        }

        public async Task TransferAsync(Account sender, Account destination)
        {
            var accountSenderModel = await _bankContext.Accounts.SingleAsync(x => x.Id == sender.Id);
            var accountDestinationModel = await _bankContext.Accounts.SingleAsync(x => x.Id == destination.Id);

            accountSenderModel.Balance = sender.Balance;
            accountDestinationModel.Balance = destination.Balance;

            var transactionSender = (Transfer)sender.Transactions.Last();

            var transactionSenderModel = CreateTransactionModel(transactionSender);
            transactionSenderModel.AccountSenderId = accountSenderModel.Id;
            transactionSenderModel.AccountDestinationId = accountDestinationModel.Id;
            transactionSenderModel.TransferType = transactionSender.TransferType.GetHashCode();

            if (accountSenderModel.Transactions == null)
            {
                accountSenderModel.Transactions = new List<TransactionModel> { transactionSenderModel };
            }
            else
            {
                accountSenderModel.Transactions.Add(transactionSenderModel);
            }

            var transactionDestination = (Transfer)destination.Transactions.Last();

            var transactionDestinationModel = CreateTransactionModel(transactionDestination);
            transactionDestinationModel.AccountSenderId = accountSenderModel.Id;
            transactionDestinationModel.AccountDestinationId = accountDestinationModel.Id;
            transactionDestinationModel.TransferType = transactionDestination.TransferType.GetHashCode();

            if (accountDestinationModel.Transactions == null)
            {
                accountDestinationModel.Transactions = new List<TransactionModel> { transactionDestinationModel };
            }
            else
            {
                accountDestinationModel.Transactions.Add(transactionDestinationModel);
            }

            await _bankContext.SaveChangesAsync();
        }

        public async Task WithdrawAsync(Account account)
        {
            await DepositOrWithdrawAsync(account);
        }

        private async Task DepositOrWithdrawAsync(Account account)
        {
            var accountModel = await _bankContext.Accounts.SingleAsync(x => x.Id == account.Id);

            accountModel.Balance = account.Balance;

            var transaction = account.Transactions.Last();

            var transactionModel = CreateTransactionModel(transaction);

            if (accountModel.Transactions == null)
            {
                accountModel.Transactions = new List<TransactionModel> { transactionModel };
            }
            else
            {
                accountModel.Transactions.Add(transactionModel);
            }

            await _bankContext.SaveChangesAsync();
        }

        private TransactionModel CreateTransactionModel(Transaction transaction)
        {
            return new TransactionModel
            {
                Id = transaction.Id,
                TransactionType = transaction.TransactionType.GetHashCode(),
                Value = transaction.Value,
                Date = transaction.Date,
            };
        }
    }
}
