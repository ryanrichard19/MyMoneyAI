using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MyMoneyAI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMoneyAI.Infrastructure.Data
{
    public class ApplicationDbContext: IdentityDbContext<User>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Account>(entity =>
            {
                entity.Property(e => e.Balance)
                    .HasPrecision(18, 2);

                entity.HasOne(e => e.Owner)
                    .WithMany(u => u.Accounts)
                    .HasForeignKey(e => e.OwnerId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Budget>(entity =>
            {
                entity.Property(e => e.Amount)
                    .HasPrecision(18, 2);

                entity.HasOne(e => e.Owner)
                    .WithMany(u => u.Budgets)
                    .HasForeignKey(e => e.OwnerId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Transaction>(entity =>
            {
                entity.Property(e => e.Amount)
                    .HasPrecision(18, 2);
            });
        }


        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Budget> Budgets { get; set; }
    }
}
