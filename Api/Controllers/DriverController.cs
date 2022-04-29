﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class DriverController : ControllerBase
    {

        private readonly ApiDbcontext _Dbcontext;

        public DriverController(ApiDbcontext dbcontext)
        {
            _Dbcontext = dbcontext;
        }

        [HttpGet]
        [RequiredScope(RequiredScopesConfigurationKey = "api:scopes:driver:read")]
        public async Task<List<DriverModel>> GetDriversAsync()
        {
            return await _Dbcontext.Drivers.Select(x => new DriverModel
            {
                DriverId = x.DriverId,
                Email = x.Employee.Email,
                EmployeeId = x.Employee.EmployeeId,
                Name = x.Employee.Name,
                Phone = x.Employee.Phone
            })
            .ToListAsync();
        }

        [HttpGet]
        [Route("{driverId}")]
        [RequiredScope(RequiredScopesConfigurationKey = "api:scopes:driver:read")]
        public async Task<IActionResult> GetDriverByIdAsync(int driverId)
        {
            var driver = await _Dbcontext.Drivers.Where(d => d.DriverId == driverId).FirstOrDefaultAsync();

            if (driver == null)
                return NotFound();

            return Ok(driver);
        }

        [HttpPost]
        [RequiredScope(RequiredScopesConfigurationKey = "api:scopes:driver:write")]
        public async Task<IActionResult> CreateDriverAsync(int EmployeeId)
        {
            Driver driver = new()
            {
                EmployeeId = EmployeeId,
            };

            try
            {
                _Dbcontext.Drivers.Add(driver);
                await _Dbcontext.SaveChangesAsync();
                return Ok("Driver created.");
            }
            catch (Exception)
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpDelete]
        [RequiredScope(RequiredScopesConfigurationKey = "api:scopes:driver:write")]
        public async Task<IActionResult> DeleteDriverAsync(int DriverId)
        {
            var driver = await _Dbcontext.Drivers.FirstOrDefaultAsync(x => x.DriverId == DriverId);

            if (driver == null)
                return NotFound();

            try
            {
                _Dbcontext.Drivers.Remove(driver);
                await _Dbcontext.SaveChangesAsync();
                return Ok("Driver deleted");
            }
            catch (Exception)
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
