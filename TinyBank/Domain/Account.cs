using Domain.Exceptions;

namespace Domain
{
    public class Account
    {
        private readonly List<Transaction> _transactions = new();

        public Guid Id { get; }

        public decimal Balance { get; private set; }

        public User User { get; }

        public List<Transaction> Transactions => _transactions;

        public Account(Guid id, decimal balance)
        {
            Id = id;
            Balance = balance;
        }

        public Account(Guid id, decimal balance, User user)
            : this(id, balance)
        {
            User = user;
        }

        public Account(Guid id, decimal balance, User user, List<Transaction> transactions)
             : this(id, balance, user)
        {
            _transactions = transactions;
        }

        public string GetAccountDescription()
        {
            return string.Concat(Id, this.User == null ? "" : string.Concat("(", this.User.Name, ")"));
        }

        public void Deposit(decimal value)
        {
            if (value <= 0)
            {
                throw new DomainException(DomainErrorCode.ValueMustBeGreaterThanZero);
            }

            if (!this.User.Active)
            {
                throw new DomainException(DomainErrorCode.InactiveUserCanNotPerformTransactions);
            }

            Balance += value;
            _transactions.Add(new Deposit(Guid.NewGuid(), value, DateTime.UtcNow));
        }

        public void Withdraw(decimal value)
        {
            if (value <= 0)
            {
                throw new DomainException(DomainErrorCode.ValueMustBeGreaterThanZero);
            }


            if (!this.User.Active)
            {
                throw new DomainException(DomainErrorCode.InactiveUserCanNotPerformTransactions);
            }

            Balance -= value;

            if (Balance < 0)
            {
                throw new DomainException(DomainErrorCode.InsufficientFunds);
            }

            _transactions.Add(new Withdraw(Guid.NewGuid(), -value, DateTime.UtcNow));
        }

        public void Transfer(decimal value, Account destination)
        {
            if (value <= 0)
            {
                throw new DomainException(DomainErrorCode.ValueMustBeGreaterThanZero);
            }


            if (!this.User.Active)
            {
                throw new DomainException(DomainErrorCode.InactiveUserCanNotPerformTransactions);
            }

            if (!destination.User.Active)
            {
                throw new DomainException(DomainErrorCode.InactiveUserCanNotPerformTransactions);
            }

            var date = DateTime.UtcNow;

            TransferOutput(value, destination, date);

            TransferInput(value, destination, date);
        }

        private void TransferOutput(decimal value, Account destination, DateTime date)
        {
            Balance -= value;

            if (Balance < 0)
            {
                throw new DomainException(DomainErrorCode.InsufficientFunds);
            }

            _transactions.Add(new Transfer(Guid.NewGuid(), -value, this, destination, date, TransferType.Output));
        }

        private void TransferInput(decimal value, Account destination, DateTime date)
        {
            destination.Balance += value;

            destination._transactions.Add(new Transfer(Guid.NewGuid(), value, this, destination, date, TransferType.Input));
        }
    }
}
