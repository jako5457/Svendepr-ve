using ApiDataLayer.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiDataLayer
{
    public class ApiDbcontext : DbContext
    {

        public ApiDbcontext(DbContextOptions options) :base(options)
        {}

        public DbSet<Category> Categories { get; set; } = default!;

        public DbSet<Company> Companys { get; set; } = default!;

        public DbSet<Driver> Drivers { get; set; } = default!;

        public DbSet<Employee> Employeees { get; set; } = default!;

        public DbSet<Order> Orders { get; set; } = default!;

        public DbSet<Product> Products { get; set; } = default!;

        public DbSet<ProductRequest> ProductRequests { get; set; } = default!;

        public DbSet<Warehouse> WarehouseOrders { get; set; } = default!;

        public DbSet<WarehouseProduct> WarehouseProducts { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProductCategory>().HasKey(c => new { c.CategoryId, c.ProductId });
            modelBuilder.Entity<WarehouseProduct>().HasKey(c => new { c.ProductId, c.WarehouseId });
            modelBuilder.Entity<ProductRequestProduct>().HasKey(c => new { c.ProductId, c.ProductRequestId });
            modelBuilder.Entity<OrderProduct>().HasKey(c => new { c.ProductId, c.OrderId });

            modelBuilder.Entity<Employee>()
                .HasMany(c => c.Orders)
                .WithOne(c => c.Employee)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<Employee>()
                .HasOne(c => c.Driver)
                .WithOne(c => c.Employee)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Driver>()
                .HasMany(c => c.Orders)
                .WithOne(c => c.Driver)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<Order>()
                .HasMany(c => c.Products)
                .WithOne(c => c.Order)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Order>()
                .HasOne(c => c.Driver)
                .WithMany(c => c.Orders)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Order>()
                .HasOne(c => c.Employee)
                .WithMany(c => c.Orders)
                .OnDelete(DeleteBehavior.NoAction);

        }

    }

}
