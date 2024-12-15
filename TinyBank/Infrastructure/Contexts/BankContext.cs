using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Contexts
{
    public class BankContext : DbContext
    {
        public BankContext(DbContextOptions<BankContext> options)
                : base(options)
        { }

        public DbSet<UserModel> Users { get; set; }
        public DbSet<AccountModel> Accounts { get; set; }
        public DbSet<TransactionModel> Transactions { get; set; }
    }
}
