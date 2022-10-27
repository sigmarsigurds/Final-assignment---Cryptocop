using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Cryptocop.Software.API.Repositories.Entities;

namespace Cryptocop.Software.API.Repositories.Contexts
{
    public class CryptocopDbContext : DbContext
    {
        public CryptocopDbContext(DbContextOptions<CryptocopDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // modelBuilder.Entity<User>()
            // .HasOne(u => u.ShoppingCart)
            // .WithOne(s => s.User)
            // .HasForeignKey<ShoppingCart>(x => new { x.Id, x.User });
            //.HasForeignKey<ShoppingCart>(u => u.Id);



            // Manual configuration of relations (many-to-many)
            // modelBuilder.Entity<Order>()
            //     .HasKey(x => new { x.OrderId, x. });

        }

        public DbSet<Address> Addresses { get; set; }
        public DbSet<JwtToken> JwtTokens { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<PaymentCard> Cards { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<ShoppingCart> Carts { get; set; }
        public DbSet<ShoppingCartItem> ShoppingCartItems { get; set; }




    }
}