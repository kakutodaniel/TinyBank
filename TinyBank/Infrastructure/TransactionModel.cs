using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure
{
    public class TransactionModel
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid Id { get; set; }

        public int TransactionType { get; set; }

        public decimal Value { get; set; }

        public DateTime Date { get; set; }

        public Guid? AccountSenderId { get; set; }

        public Guid? AccountDestinationId { get; set; }

        public int? TransferType { get; set; }
    }
}
