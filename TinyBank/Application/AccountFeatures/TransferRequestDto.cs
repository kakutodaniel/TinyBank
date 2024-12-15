namespace Application.AccountFeatures
{
    public class TransferRequestDto
    {
        public Guid DestinationAccountId { get; set; }

        public decimal Value { get; set; }
    }
}
