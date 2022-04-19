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
    public class CompanyTest
    {

        private readonly ApiDbcontext _Dbcontext;

        public CompanyTest()
        {
            var options = new DbContextOptionsBuilder<ApiDbcontext>()
                                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                                .Options;
            _Dbcontext = new ApiDbcontext(options);
            _Dbcontext.Database.EnsureCreated();
        }

        [Theory]
        [InlineData(10, 10)]
        [InlineData(5, 5)]
        [InlineData(97, 97)]
        public async Task TestGet(int companies,int expectedcompanys)
        {
            await CreateCompaniesAsync(companies);

            var controller = new CompanyController(_Dbcontext);

            int actualcompanys = (await controller.GetCompanies()).Count;

            Assert.Equal(expectedcompanys, actualcompanys);
        }

        public async Task CreateCompaniesAsync(int amount)
        {
            for (int i = 0; i < amount; i++)
            {
                string filler = new Guid().ToString();

                _Dbcontext.Companys.Add(new Company
                {
                    Email = filler,
                    Name = filler,
                    Phone = filler,
                    Address = filler,
                    Zipcode = filler,
                });
            }

            await _Dbcontext.SaveChangesAsync();
        }
    }
}
