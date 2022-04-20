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
    public class ProductTest
    {

        private readonly ApiDbcontext _Dbcontext;

        public ProductTest()
        {
            var options = new DbContextOptionsBuilder<ApiDbcontext>()
                                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                                .Options;
            _Dbcontext = new ApiDbcontext(options);
            _Dbcontext.Database.EnsureCreated();
        }

        [Fact]
        public async Task TestProductCreation()
        {
            _Dbcontext.Categories.Add(new Category() { Name = "TestCategory" });
            await _Dbcontext.SaveChangesAsync();

            var controller = new ProductController(_Dbcontext);

            ProductModel model = new ProductModel()
            {
                Name = "test",
                BuyPrice = 986986,
                Description = "test",
                EAN = "fjiejfoi"
            };
            model.Categories = new List<int>() { 1 };

            var result = await controller.CreateProductAsync(model);

            Assert.Equal(typeof(OkObjectResult), result.GetType());
        }

        [Theory]
        [InlineData(1, 10, 1)]
        [InlineData(5, 2, 3)]
        [InlineData(10, 5, 2)]
        [InlineData(100, 10, 10)]
        public async Task TestGetPageCount(int products, int itemsPerPage, int expectedpages)
        {
            await CreateProductsAsync(products);

            var controller = new ProductController(_Dbcontext);

            int actualpages = await controller.GetProductPageCountAsync(itemsPerPage);

            Assert.Equal(expectedpages, actualpages);
        }

        [Theory]
        [InlineData(1, 10, 1, 1)]
        [InlineData(5, 2, 2, 2)]
        [InlineData(10, 5, 2, 5)]
        [InlineData(97, 10, 10, 7)]
        public async Task TestGetPagination(int products, int itemsPerPage, int page, int ExpectedItemsOnPage)
        {
            await CreateProductsAsync(products);

            var controller = new ProductController(_Dbcontext);

            var result = await controller.GetProductPagesAsync(itemsPerPage, page);

            Assert.Equal(ExpectedItemsOnPage, result.Count);
        }

        public async Task CreateProductsAsync(int amount)
        {
            for (int i = 0; i < amount; i++)
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

            await _Dbcontext.SaveChangesAsync();
        }
    }
}
