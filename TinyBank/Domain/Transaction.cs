namespace Domain
{
    public class Deposit : Transaction
    {
        public Deposit(Guid id, decimal value, DateTime date)
            : base(id, TransactionType.Deposit, value, date)
        {
        }
    }

    public class Withdraw : Transaction
    {
        public Withdraw(Guid id, decimal value, DateTime date)
            : base(id, TransactionType.Withdraw, value, date)
        {
        }
    }

    public class Transfer : Transaction
    {
        public Account Sender { get; }

        public Account Destination { get; }

        public TransferType TransferType { get; set; }

        public Transfer(Guid id, decimal value, Account sender, Account destination, DateTime date, TransferType transferType)
            : base(id, TransactionType.Transfer, value, date)
        {
            Sender = sender;
            Destination = destination;
            TransferType = transferType;
        }
    }

    public abstract class Transaction
    {
        public Guid Id { get; }

        public TransactionType TransactionType { get; }


        //public string Currency { get; set; }

        public decimal Value { get; }


        public DateTime Date { get; }


        public Transaction(Guid id, TransactionType transactionType, decimal value, DateTime date)
        {
            Id = id;
            TransactionType = transactionType;
            Value = value;
            Date = date;
        }
    }

    public enum TransactionType
    {
        Deposit = 1,
        Withdraw = 2,
        Transfer = 3
    }

    public enum TransferType
    {
        Output = 1,
        Input = 2
    }
}
