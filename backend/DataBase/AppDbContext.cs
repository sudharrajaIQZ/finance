using backend.Models;
using Microsoft.EntityFrameworkCore;

namespace backend.DataBase
{
    public class AppDbContext :DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options) { }

        public DbSet<AuthModel> users { get; set; }
        public DbSet<CustomerModule> customers { get; set; }
        public DbSet<OrganizationModule> organizations {get; set;}
        public DbSet<TransactionModule> transactions { get; set; }
        public DbSet<RoleModule> roles { get; set; }
        public DbSet<UserRole> userRoles { get; set; }
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
            modelBuilder.Entity<AuthModel>().HasData(
                new AuthModel { Id = new Guid("66666666-6666-6666-6666-666666666666"), UserName = "adminUser", EmailId = "admin@example.com", Password = "admin", Otp = 123456, isVerified = true, createdAt = new DateTime(2024, 01, 01, 00, 00, 00), updatedAt = new DateTime(2024, 01, 01, 00, 00, 00)} 
                );

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

            modelBuilder.Entity<UserRole>()
         .HasKey(ur => new { ur.UserId, ur.RoleId });

            modelBuilder.Entity<UserRole>()
                .HasOne(ur => ur.User)
                .WithMany(u => u.UserRoles)
                .HasForeignKey(ur => ur.UserId);

            modelBuilder.Entity<UserRole>()
                .HasOne(ur => ur.Role)
                .WithMany(r => r.userRoles)
                .HasForeignKey(ur => ur.RoleId);

            modelBuilder.Entity<RoleModule>().HasData
(
    new RoleModule { Id = new Guid("11111111-1111-1111-1111-111111111111"), Name = "Admin", Description = "Admin Role" },
    new RoleModule { Id = new Guid("22222222-2222-2222-2222-222222222222"), Name = "Editor", Description = "Editor Role" },
    new RoleModule { Id = new Guid("33333333-3333-3333-3333-333333333333"), Name = "Super Admin", Description = "Super Admin Role" },
    new RoleModule { Id = new Guid("44444444-4444-4444-4444-444444444444"), Name = "Manager", Description = "Manager Role" },
    new RoleModule { Id = new Guid("55555555-5555-5555-5555-555555555555"), Name = "Guest", Description = "Guest Role" }
);
        }
    }
}
