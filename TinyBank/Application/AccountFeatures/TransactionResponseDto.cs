namespace Application.AccountFeatures
{
    public class TransactionResponseDto
    {
        public Guid Id { get; set; }

        public decimal Value { get; set; }

        public DateTime Date { get; set; }

        public TransactionResponseDetailsDto TransactionResponseDetails { get; set; }
    }

    public class TransactionResponseDetailsDto
    {
        public string TransactionType { get; set; }

        public string Sender { get; set; }

        public string Destination { get; set; }

        public string TransferType { get; set; }
    }
}
