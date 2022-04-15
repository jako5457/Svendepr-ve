using ApiDataLayer.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiDataLayer
{
    internal class ApiDbcontext : DbContext
    {

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
            
        }

    }

}
