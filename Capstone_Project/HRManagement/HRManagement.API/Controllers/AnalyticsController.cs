using HRManagement.Infrastructure.ADO;
using Microsoft.AspNetCore.Mvc;

namespace HRManagement.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AnalyticsController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public AnalyticsController(
            IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet("employee-count")]
        public async Task<IActionResult> GetEmployeeCount()
        {
            var repository =
                new EmployeeAdoRepository(
                    _configuration.GetConnectionString(
                        "DefaultConnection"));

            var count =
                await repository.GetEmployeeCountAsync();

            return Ok(count);
        }
    }
}