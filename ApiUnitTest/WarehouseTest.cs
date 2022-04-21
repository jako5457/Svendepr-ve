using Xunit;
using ApiDataLayer;
using ApiDataLayer.Entities;
using System;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Controllers;
using Api.Model;
using Microsoft.AspNetCore.Mvc;

namespace ApiUnitTest
{
    public class WarehouseTest
    {

        private readonly ApiDbcontext _Dbcontext;

        public WarehouseTest()
        {
            var options = new DbContextOptionsBuilder<ApiDbcontext>()
                                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                                .Options;
            _Dbcontext = new ApiDbcontext(options);

            _Dbcontext.Database.EnsureCreated();
        }

        [Fact]
        public async Task WarehouseCreation()
        {
            await CreateEntities();

            WarehouseController controller = new WarehouseController(_Dbcontext);

            WarehouseModel warehouse = new WarehouseModel ()
            {
                Name = "warehouse",
                Address = "SomeWarehouse",
                CompanyId = 1,
                Email = "Some@email.com",
                Phone = "12345",
                Zipcode = "1234"
            };

            var result = await controller.CreateWarehouseAsync(warehouse);

            Assert.Equal(typeof(OkObjectResult), result.GetType());
        }

        [Fact]
        public async Task WarehouseProductCreation()
        {
            await CreateEntities();
            WarehouseController controller = new WarehouseController(_Dbcontext);

            WarehouseProductModel warehouseProduct = new WarehouseProductModel()
            {
                ProductId = 2,
                WarehouseId = 1,
                Amount = 33
            };

            var result = await controller.AddPrductAsync(warehouseProduct);

            Assert.Equal(typeof(OkObjectResult), result.GetType());
        }

        public async Task CreateEntities()
        {

            _Dbcontext.Companys.Add(new Company
            {
                Name = "SomeCompany",
                Address = "SomeAddress",
                Email = "some@email.com",
                Phone = "123345",
                Zipcode = "1234"
            });

            for (int i = 0; i < 10; i++)
            {
                string filler = new Guid().ToString();

                _Dbcontext.Products.Add(new Product
                {
                    Name = filler,
                    BuyPrice = i,
                    Description = filler,
                    EAN = filler,
                });
            }

            _Dbcontext.WarehouseProducts.Add(new WarehouseProduct
            {
                ProductId = 1,
                WarehouseId = 1,
                Amount = 22,
            });

            _Dbcontext.WarehouseProducts.Add(new WarehouseProduct
            {
                ProductId = 5,
                WarehouseId = 1,
                Amount = 22,
            });

            await _Dbcontext.SaveChangesAsync();
        }

    }
}
