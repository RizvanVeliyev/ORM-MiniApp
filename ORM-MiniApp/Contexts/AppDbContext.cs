using Microsoft.EntityFrameworkCore;
using ORM_MiniApp.Configurations;
using ORM_MiniApp.Dtos.ProductDtos;
using ORM_MiniApp.Models;

namespace ORM_MiniApp.Contexts
{
    internal class AppDbContext:DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(OrderConfiguration).Assembly);
            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=localhost;Database=ORM_MiniApp;User Id=sa;Password=!Dquery20@3");
            base.OnConfiguring(optionsBuilder);
        }

    }
}
