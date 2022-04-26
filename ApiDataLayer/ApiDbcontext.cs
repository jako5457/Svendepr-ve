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

        public DbSet<Warehouse> Warehouses { get; set; } = default!;

        public DbSet<Employee> Employees { get; set; } = default!;

        public DbSet<Order> Orders { get; set; } = default!;

        public DbSet<Product> Products { get; set; } = default!;

        public DbSet<ProductRequest> ProductRequests { get; set; } = default!;

        public DbSet<Warehouse> WarehouseOrders { get; set; } = default!;

        public DbSet<WarehouseProduct> WarehouseProducts { get; set; } = default!;

        public DbSet<TrackingInfo> TrackingInfos { get; set; } = default!;

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

            modelBuilder.Entity<Employee>().HasData(
                new Employee
                {
                    EmployeeId = 1,
                    CompanyId = 1,
                    Name = "Bob Smith",
                    Email = "BobSmith@email.com",
                    Phone = "90593032",
                },
                new Employee
                {
                    EmployeeId = 2,
                    CompanyId = 2,
                    Name = "Alice Smith",
                    Email = "AliceSmith@email.com",
                    Phone = "90534563",
                }
            );

            modelBuilder.Entity<Company>().HasData(
                new Company {
                    CompanyId = 1,
                    Name = "Lilla kors",
                    Address = "Padborg",
                    Email = "Lillakors@email.com",
                    Phone = "1234567",
                    Zipcode = "6330"
                },
                new Company
                {
                    CompanyId = 2,
                    Name = "Company",
                    Address = "Somewhere",
                    Email = "Comany@email.com",
                    Phone = "1234567",
                    Zipcode = "6330"
                }
            );

            modelBuilder.Entity<Driver>().HasData(
                new Driver()
                {
                    DriverId = 1,
                    EmployeeId = 2
                }
            );

            modelBuilder.Entity<Product>().HasData(
                new Product()
                {
                    ProductId = 1,
                    BuyPrice = 20,
                    Name = "Toiletpapir",
                    EAN = "169505",
                    Description = "",
                },
                new Product()
                {
                    ProductId = 2,
                    BuyPrice = 3000,
                    Name = "Starlink satelite dish",
                    EAN = "123456",
                    Description = ""
                }
            );

            Guid TrackingGuid = Guid.NewGuid();

            modelBuilder.Entity<Order>().HasData(new Order()
            {
                OrderId = 1,
                EmployeeId=1,
                DriverId=1,
                DeliveryAddress = "SomeWhere",
                DeliveryLocation = "222222222",
                TrackingInfoId = TrackingGuid,
            });

            modelBuilder.Entity<OrderProduct>().HasData(
            new OrderProduct()
            {
                OrderId=1,
                ProductId=1,
                Amount=100,
            },
            new OrderProduct()
            {
                OrderId = 1,
                ProductId = 2,
                Amount = 2,
            });
        }

    }

}
