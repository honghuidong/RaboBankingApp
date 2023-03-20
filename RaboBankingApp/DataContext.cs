using Microsoft.EntityFrameworkCore;
using RaboBankingApp.Entities;
using RaboBankingApp.Entities.NotUsed;
using Account = RaboBankingApp.Entities.Account;
using Transaction = RaboBankingApp.Entities.Transaction;

namespace RaboBankingApp
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {
            //Database.EnsureCreated(); //before you run our application you make sure that our tables are there
        }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<CategoryAccount> CategoryAccounts { get; set; }
        


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Transaction>()
            //    .ToTable("Transactions")
            //    .HasKey(c => c.TransactionId);

            //modelBuilder.Entity<UserAccount>()
            //    .ToTable("UserAccounts")
            //    .HasKey(c => c.UserAccountId);

            //modelBuilder.Entity<Account>()
            //    .ToTable("Accounts")
            //    .HasKey(c => c.AccountId);

            //modelBuilder
            //    .Entity<Transaction>()
            //    .HasOne<Account>(a => a.FromAccount)
            //    //.WithMany(m => m.FromTransactions)
            //    //.HasForeignKey(a => a.FromAccountId)
            //    //.OnDelete(DeleteBehavior.NoAction)
            //    .HasPrincipalKey(p => p.Id);

            //modelBuilder
            //    .Entity<Transaction>()
            //    .HasOne<Account>(a => a.ToAccount)
            //    //.WithMany(m => m.ToTransactions)
            //    .HasForeignKey(a => a.ToAccountId)
            //    .OnDelete(DeleteBehavior.NoAction)
            //    .HasPrincipalKey(p => p.Id);
        }
    }
}