using Microsoft.EntityFrameworkCore;
using Singular.Roulette.Domain.Models;

namespace Singular.Roulette.Repository
{
    public class SingularDbContext : DbContext
    {


        public SingularDbContext(DbContextOptions<SingularDbContext> options) :base(options)
        {

        }
        public DbSet<User> Users { get; set; }
        public DbSet<Account> Accounts { get; set; }    
        public DbSet<AccountType> AccountTypes { get; set; }
        public DbSet<Bet>   Bets { get; set; }  
        public DbSet<Spin> Spins { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<TransactionType> TransactionTypes { get; set; }

        public DbSet<TransactionStatus> TransactionStatuses { get; set; }
        public DbSet<HeartBeet> HeartBeets { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasKey(x => x.UserId);
            modelBuilder.Entity<User>().Property(x => x.Password).HasMaxLength(128);
            modelBuilder.Entity<User>().Property(x => x.UserName).HasMaxLength(100);
            modelBuilder.Entity<User>().HasMany(x => x.Accounts).WithOne().OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<AccountType>().HasKey(x => x.TypeId);
           modelBuilder.Entity<AccountType>().Property(x => x.TypeId).ValueGeneratedNever();
            modelBuilder.Entity<AccountType>().Property(x => x.Name).HasMaxLength(20);


            modelBuilder.Entity<Account>().HasKey(x => x.Id);
            modelBuilder.Entity<Account>().HasOne(x=>x.User).WithMany(x=>x.Accounts).HasForeignKey(x=>x.UserId).IsRequired(false).OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Account>().Property(x => x.Currency).HasMaxLength(5);

            modelBuilder.Entity<Account>().HasOne(x=>x.Type).WithMany().HasForeignKey(x=>x.TypeId);


            modelBuilder.Entity<HeartBeet>().HasKey(x => x.SessionId);


            modelBuilder.Entity<Spin>().HasKey(x => x.SpinId);

            modelBuilder.Entity<Bet>().HasKey(x => x.BetId);
            modelBuilder.Entity<Bet>().HasOne(x => x.Spin).WithMany().HasForeignKey(x => x.SpinId);
            modelBuilder.Entity<Bet>().Property(x => x.BetStringJson).HasMaxLength(2000);
            modelBuilder.Entity<Bet>().Property(x => x.UserIpAddress).HasMaxLength(200);
            modelBuilder.Entity<Bet>().HasOne(x => x.Spin).WithMany().HasForeignKey(x => x.SpinId).IsRequired(false);

            modelBuilder.Entity<TransactionType>().HasKey(x => x.TransactionTypeId);
            modelBuilder.Entity<TransactionType>().Property(x => x.TransactionTypeId).ValueGeneratedNever();
            modelBuilder.Entity<TransactionType>().Property(x => x.TypeName).HasMaxLength(20);


            modelBuilder.Entity<TransactionStatus>().HasKey(x => x.TransactionStatusCode);
            modelBuilder.Entity<TransactionStatus>().Property(x => x.TransactionStatusCode).ValueGeneratedNever();
            modelBuilder.Entity<TransactionStatus>().Property(x => x.Description).HasMaxLength(20);


            modelBuilder.Entity<Transaction>().HasKey(x => x.TransactionId);
            modelBuilder.Entity<Transaction>().HasOne(x => x.TransactionType).WithMany().HasForeignKey(x => x.TransactionTypeId);
            modelBuilder.Entity<Transaction>().HasOne(x => x.TransactionStatus).WithMany().HasForeignKey(x => x.TransactionStatusCode);


            base.OnModelCreating(modelBuilder);
        }


   
    }
}
