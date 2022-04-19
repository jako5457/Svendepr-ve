using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        
        private readonly ApiDbcontext _Dbcontext;

        public EmployeeController(ApiDbcontext dbcontext)
        {
            _Dbcontext = dbcontext;
        }

        [HttpGet]
        [Route("list")]
        public async Task<List<Employee>> GetEmployees()
        {
            return await _Dbcontext.Employees.ToListAsync();
        }

        [HttpGet]
        [Route("{Email}")]
        public async Task<IActionResult> GetEmployeeByEmailAsync(string Email)
        {
            var employee = await _Dbcontext.Employees.Where(x => x.Email == Email).FirstOrDefaultAsync();

            if (employee == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(employee);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateEmployeeAsync(EmployeeModel model)
        {
            if (model == null)
            {
                return BadRequest();
            }

            Employee employee = new()
            {
                CompanyId = model.CompanyId,
                Name = model.Name,
                Phone = model.Phone,
                Email = model.Email
            };

            try
            {
                _Dbcontext.Employees.Add(employee);
                await _Dbcontext.SaveChangesAsync();
                return Ok("Employee created.");
            }
            catch (Exception)
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }

        }

        [HttpPut]
        public async Task<IActionResult> EditEmployeeAsync(Employee employee)
        {
            try
            {
                _Dbcontext.Attach<Employee>(employee);
                _Dbcontext.Entry(employee).State = EntityState.Modified;
                await _Dbcontext.SaveChangesAsync();
                return Ok("Employee Updated.");
            }
            catch (Exception)
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteEmployeeAsync(int EmployeeId)
        {
            try
            {
                var empl = _Dbcontext.Employees.Where(e => e.EmployeeId == EmployeeId).FirstOrDefault();

                if (empl == null)
                {
                    return NotFound();
                }

                _Dbcontext.Employees.Remove(empl);
                await _Dbcontext.SaveChangesAsync();
                return Ok("Employee Deleted.");
            }
            catch (Exception)
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
