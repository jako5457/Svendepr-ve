using Xunit;
using ApiDataLayer;
using ApiDataLayer.Entities;
using System;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Controllers;

namespace ApiUnitTest
{
    public class EmployeeTest
    {
        private readonly ApiDbcontext _Dbcontext;

        public EmployeeTest()
        {
            var options = new DbContextOptionsBuilder<ApiDbcontext>()
                                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                                .Options;
            _Dbcontext = new ApiDbcontext(options);
            _Dbcontext.Database.EnsureCreated();
        }

        [Theory]
        [InlineData(1,10,1)]
        [InlineData(5,2,3)]
        [InlineData(10,5,2)]
        [InlineData(100,10,10)]
        public async Task TestGetPageCount(int employees, int itemsPerPage,int expectedpages)
        {
            await CreateEmpoyeesAsync(_Dbcontext, employees);

            var controller = new EmployeeController(_Dbcontext);

            int actualpages = await controller.GetEmployeePageCountAsync(itemsPerPage);

            Assert.Equal(expectedpages, actualpages);
        }

        [Theory]
        [InlineData(1, 10, 1,1)]
        [InlineData(5, 2, 2 ,2)]
        [InlineData(10, 5, 2,5)]
        [InlineData(97, 10, 10, 7)]
        public async Task TestGetPagination(int employees, int itemsPerPage,int page,int ExpectedItemsOnPage)
        {
            await CreateEmpoyeesAsync(_Dbcontext,employees);

            var controller = new EmployeeController(_Dbcontext);

            var result = await controller.GetEmployeePageAsync(itemsPerPage, page);

            Assert.Equal(ExpectedItemsOnPage, result.Count);
        }

        private async Task CreateEmpoyeesAsync(ApiDbcontext context,int amount)
        {

            for (int i = 0; i < amount; i++)
            {
                string filler = new Guid().ToString();

                context.Employees.Add(new Employee
                {
                    Email = filler,
                    Name = filler,
                    Phone = filler,
                    Company = null,
                    Driver = null,
                });
            }

            await context.SaveChangesAsync();
        }
    }
}