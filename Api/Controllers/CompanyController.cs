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

        [HttpPost]
        public async Task<IActionResult> CreateCompany(CompanyModel model)
        {
            Company company = new()
            {
                Name = model.Name,
                Address = model.Address,
                Email = model.Email,
                Phone = model.Phone,
                Zipcode = model.Zipcode,
            };

            try
            {
                _Dbcontext.Companys.Add(company);
                await _Dbcontext.SaveChangesAsync();
                return Ok("Company created");
            }
            catch (Exception)
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCompany(Company company)
        {
            try
            {
                _Dbcontext.Companys.Attach(company);
                _Dbcontext.Entry(company).State = EntityState.Modified;
                await _Dbcontext.SaveChangesAsync();
                return Ok("Company updated");
            }
            catch (Exception)
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteCompany(int CompanyId)
        {
            try
            {
                var comp = _Dbcontext.Companys.Where(e => e.CompanyId == CompanyId).FirstOrDefault();

                if (comp == null)
                {
                    return NotFound();
                }

                _Dbcontext.Companys.Remove(comp);
                await _Dbcontext.SaveChangesAsync();
                return Ok("Company Deleted.");
            }
            catch (Exception)
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
