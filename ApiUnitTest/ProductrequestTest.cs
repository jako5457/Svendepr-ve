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
    public class ProductrequestTest
    {

        private readonly ApiDbcontext _Dbcontext;

        public ProductrequestTest()
        {
            var options = new DbContextOptionsBuilder<ApiDbcontext>()
                                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                                .Options;
            _Dbcontext = new ApiDbcontext(options);
            _Dbcontext.Database.EnsureCreated();
        }

        [Fact]
        public async Task CreateProductrequest()
        {
            await CreateEntities();

            ProductRequestController controller = new ProductRequestController(_Dbcontext);

            ProductRequestModel request = new ProductRequestModel()
            {
                EmployeeId = 1,
                Location = "Somewhere",
                Products = new List<ProductRequestProductModel> { new ProductRequestProductModel { ProductId = 1, Amount = 10 } }
            };

            var result = await controller.CreateProductRequest(request);

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
