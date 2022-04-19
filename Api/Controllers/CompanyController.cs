using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {

        private readonly ApiDbcontext _Dbcontext;

        public CompanyController(ApiDbcontext dbcontext)
        {
            _Dbcontext = dbcontext;
        }

        [HttpGet]
        [Route("list")]
        public async Task<List<Company>> GetCompanies()
        {
            return await _Dbcontext.Companys.ToListAsync();
        }

        [HttpGet]
        [Route("{Companyid}")]
        public async Task<IActionResult> GetCompanyByidAsync(int Companyid)
        {
            var company = await _Dbcontext.Companys.Where(c => c.CompanyId == Companyid).FirstOrDefaultAsync();

            if (company == null)
            {
                return NotFound();
            }
            return Ok(company);
        }

        [HttpGet]
        [Route("page/{ItemCount}/{Page}")]
        public async Task<IActionResult> SearchCompanyAsync(string query,int ItemCount,int Page)
        {
            if (query == string.Empty)
            {
                return Ok(await _Dbcontext.Companys
                                            .GetPage(Page,ItemCount)
                                            .AsQueryable()
                                            .ToListAsync());
            }

            return Ok(await _Dbcontext.Companys
                                            .Where(c => c.Name.Contains(query))
                                            .GetPage(Page, ItemCount)
                                            .AsQueryable()
                                            .ToListAsync());
        }

    }
}
