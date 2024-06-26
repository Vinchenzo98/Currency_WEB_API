using Microsoft.EntityFrameworkCore;
using Currency.API.Models;

namespace Currency.API.DAL
{
    public class CurrencyAPIContext : DbContext
    {
        public CurrencyAPIContext(DbContextOptions<CurrencyAPIContext> options)
            :base (options) 
            { 
            }

        public DbSet<AccountTypeModelAPI> AccountType { get; set; }
        public DbSet<AdminsModelAPI> Admins { get; set; }
        public DbSet<BlockedUserLogModelAPI> BlockedUsers { get; set; }
        public DbSet<CurrencyTypeModelAPI> Currency { get; set; }       
        public DbSet<TransactionLogModelAPI> TransactionLog { get; set; }     
        public DbSet<UsersModelAPI> Users { get; set; }

        public DbSet<BlockedTransactionsModelAPI> BlockedTransactions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<TransactionLogModelAPI>()
                       .HasKey(tl => new
                       {
                           tl.AccountID,
                           tl.CurrencyID,
                           tl.UserID,
                           tl.TimeSent,
                           tl.Amount
                       });

            modelBuilder.Entity<BlockedUserLogModelAPI>()
                        .HasKey(bu => new
                        {
                            bu.AccountID,
                            bu.UserID,
                            bu.AdminID,
                            bu.BlockDate
                        });

            modelBuilder.Entity<BlockedTransactionsModelAPI>()
                        .HasOne(bt => bt.TransactionLog)
                        .WithMany(tl => tl.BlockedTransactions)
                        .HasForeignKey(bt => new
                        {
                            bt.AccountID,
                            bt.UserID,
                            bt.CurrencyID,
                            bt.TimeSent,
                            bt.Amount
                        });

        }

    }
}
