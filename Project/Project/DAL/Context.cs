using Microsoft.EntityFrameworkCore;
using Project.Models;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Reflection.Emit;

namespace Project.DAL
{
    public class Context : DbContext
    {
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Gift> Gifts { get; set; }
        public DbSet<Sale> Sales { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Winner> Winners { get; set; }

        public Context(DbContextOptions<Context> contextOptions) : base(contextOptions)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>().Property(c => c.Id).UseIdentityColumn(1, 17);
            modelBuilder.Entity<Gift>().Property(p => p.Id).UseIdentityColumn(45, 25);
            modelBuilder.Entity<Sale>().Property(p => p.Id).UseIdentityColumn(35, 88);
            modelBuilder.Entity<Category>().Property(p => p.Id).UseIdentityColumn(9, 14);
            modelBuilder.Entity<Winner>().Property(p => p.Id).UseIdentityColumn(18, 24);
           
            modelBuilder.Entity<Customer>()
                .HasIndex(c => new { c.Name, c.Password })
                .IsUnique(true);

            modelBuilder.Entity<Category>().HasIndex(c => c.Name).IsUnique(true);
            modelBuilder.Entity<Gift>().HasIndex(c => c.Name).IsUnique(true);
            modelBuilder.Entity<Winner>().HasIndex(c => c.GiftId).IsUnique(true);

            modelBuilder.Entity<Sale>()
           .HasOne(s => s.Customer)
           .WithMany(c => c.SaleList)
           .HasForeignKey(s => s.CustomerId)
           .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Sale>()
                .HasOne(s => s.Gift)
                .WithMany(g => g.SaleList)
                .HasForeignKey(s => s.GiftId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Gift>()
                .HasOne(g => g.Customer)
                .WithMany()
                .HasForeignKey(g => g.CustomerId)
                .OnDelete(DeleteBehavior.Restrict);
            base.OnModelCreating(modelBuilder);
        }

    }


}
