using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure
{
    public class AccountModel
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid Id { get; set; }

        public decimal Balance { get; set; }

        public UserModel User { get; set; }

        public Guid UserId { get; set; }

        public virtual List<TransactionModel> Transactions { get; set; }
    }
}
