using Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Context
{
    public class MasterContext : DbContext
    {
        public MasterContext()
        {

        }
        public MasterContext(DbContextOptions builder) : base(builder)
        {

        }
        public DbSet<Basket> Baskets { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<FirstSubCategory> FirstSubCategories { get; set; }
        public DbSet<SecondSubCategory> SecondSubCategories { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Wallet> Wallets { get; set; }
        public DbSet<Order> Orders { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer("Server=MSI\\MSSQLSERVER01;Database=shoppingdb;Trusted_Connection=True;");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasOne(x => x.Basket).WithOne(x => x.User).HasForeignKey<Basket>(x => x.UserId).HasPrincipalKey<User>(x => x.Id);
            modelBuilder.Entity<User>().HasOne(x => x.Wallet).WithOne(x => x.User);

            modelBuilder.Entity<Order>().HasOne(x => x.User).WithMany(x => x.Order).HasForeignKey(x => x.UserId).HasPrincipalKey(x => x.Id);

            modelBuilder.Entity<Product>().HasOne(x => x.Basket).WithMany(x => x.Product).HasForeignKey(x => x.BasketId).HasPrincipalKey(x => x.Id);

            modelBuilder.Entity<Order>().HasMany(x => x.Product).WithOne(x => x.Order).HasForeignKey(x => x.OrderId).HasForeignKey(x => x.BasketId).HasForeignKey(x => x.CategoryId).OnDelete(DeleteBehavior.NoAction).HasPrincipalKey(x => x.Id);
            modelBuilder.Entity<Category>().HasMany<Product>(x => x.Products).WithOne(x => x.Category).HasForeignKey(x => x.CategoryId).HasPrincipalKey(x => x.Id);

            modelBuilder.Entity<Category>().HasMany<FirstSubCategory>(x => x.FirstSubCategory).WithOne(x => x.Category).HasForeignKey(x => x.CategoryId).HasPrincipalKey(x => x.Id);
            modelBuilder.Entity<FirstSubCategory>().HasMany<SecondSubCategory>(x => x.SecondSubCategory).WithOne(x => x.FirstSubCategory).HasForeignKey(x => x.FirstSubCategoryId).HasPrincipalKey(x => x.Id);
            base.OnModelCreating(modelBuilder);

        }
    }
}
