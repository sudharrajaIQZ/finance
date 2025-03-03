using backend.Models;
using Microsoft.EntityFrameworkCore;

namespace backend.DataBase
{
    public class AppDbContext :DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options) { }

        public DbSet<AuthModel> userDetails { get; set; }
        public DbSet<CustomerModule> customerDetails { get; set; }
        public DbSet<OrganizationModule> organization {get; set;}
        public DbSet<TransactionModule> transaction { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<AuthModel>(entity =>
            {
                entity.HasIndex(e => e.UserName).IsUnique();
                entity.HasIndex(e => e.EmailId).IsUnique();

                entity.Property(e => e.UserName).IsRequired().HasMaxLength(60);
                entity.Property(e => e.EmailId).IsRequired().HasMaxLength(50);
                entity.Property(e => e.Password).IsRequired().HasMaxLength(50);
                entity.Property(e => e.Otp).IsRequired().HasMaxLength(6);
                entity.Property(e => e.isVerified).IsRequired().HasMaxLength(4);

            });

            modelBuilder.Entity<CustomerModule>(entity =>
            {
                entity.HasIndex(e => e.Id).IsUnique();
                entity.Property(e => e.firstName).IsRequired();
                entity.Property(e => e.lastName).HasMaxLength(50);
                entity.HasIndex(e => e.phoneNumber).IsUnique();
                entity.Property(e => e.phoneNumber).IsRequired();
                entity.HasIndex(e => e.AccountNumber).IsUnique();
                entity.Property(e => e.AccountNumber).IsRequired();
                entity.Property(e => e.AccountType).IsRequired();
                entity.Property(e => e.UserId).IsRequired();
                entity.Property(e => e.BankId).IsRequired();

            });

            modelBuilder.Entity<OrganizationModule>(entity =>
            {
                entity.HasIndex(e => e.BankName).IsUnique();
                entity.HasIndex(e => e.BranchCode).IsUnique();
                entity.HasIndex(e => e.BranchName).IsUnique();
                entity.HasIndex(e => e.Id).IsUnique();
                entity.Property(e => e.BankName).IsRequired();
                entity.Property(e => e.BranchName).IsRequired();
                entity.Property(e => e.IFSCCode).IsRequired();
                entity.Property(e => e.MICRCode).IsRequired();
            });

            modelBuilder.Entity<TransactionModule>(entity =>
            {
                entity.HasIndex(e => e.Id).IsUnique();
                entity.Property(e => e.CustomerId).IsRequired();
                entity.Property(e => e.Type).IsRequired();
                entity.Property(e => e.Amount).IsRequired();
            });

        }
    }
}
