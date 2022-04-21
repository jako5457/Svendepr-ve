using ApiDataLayer;
using System;
using Xunit;
using ApiDataLayer.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Controllers;
using Api.Model;
using Microsoft.AspNetCore.Mvc;

namespace ApiUnitTest
{
    public class OrderTest
    {

        private readonly ApiDbcontext _Dbcontext;

        public OrderTest()
        {
            var options = new DbContextOptionsBuilder<ApiDbcontext>()
                                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                                .Options;
            _Dbcontext = new ApiDbcontext(options);
            _Dbcontext.Database.EnsureCreated();
        }

        [Fact]
        public async Task CreateOrder()
        {
            await CreateEntities();

            OrderController controller = new OrderController(_Dbcontext);

            OrderModel order = new OrderModel
            {
                DriverId = 1,
                EmployeeId = 2,
                DeliveryAddress = "Somewhere",
                DeliveryLocation = "Around the corner",
                TrackingNumber = "1234",
                Products = new List<OrderProductModel>()
                {
                    new OrderProductModel()
                    {
                        ProductId = 1,
                        Amount = 22,
                    }
                }
            };

            var result = await controller.CreateOrderAsync(order);

            Assert.Equal(typeof(OkObjectResult), result.GetType());
        }

        public async Task CreateEntities()
        {
            _Dbcontext.Employees.AddRange(new List<Employee> {
                new Employee
                {
                    Name = "bob",
                    Email = "bob@ekample.com",
                    Phone = "1234"
                },
                new Employee
                {
                    Name = "alice",
                    Email = "alice@ekample.com",
                    Phone = "1234"
                }
            });

            _Dbcontext.Drivers.Add(new Driver()
            {
                EmployeeId = 1,
            });

            _Dbcontext.Products.Add(new Product
            {
                Name = "product",
                EAN = "1234",
                BuyPrice = 22,
                Description = "some description"
            });

            await _Dbcontext.SaveChangesAsync();
        }
    }
}
