using HRManagement.Application.Services;
using HRManagement.Core.DTOs.Employee;
using HRManagement.Core.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HRManagement.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class EmployeeController : ControllerBase
    {
        private readonly EmployeeService _employeeService;

        public EmployeeController(EmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        // =====================================================
        // Admin + HR + Employee can view a single employee
        // =====================================================
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin,HR,Employee")]
        public async Task<IActionResult> GetEmployee(int id)
        {
            var employee =
                await _employeeService.GetEmployeeAsync(id);

            return Ok(new EmployeeResponseDto
            {
                EmployeeId = employee.EmployeeId,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                Email = employee.Email,
                Department = employee.Department,
                JoiningDate = employee.JoiningDate
            });
        }

        // =====================================================
        // Admin + HR can view all employees
        // =====================================================
        [HttpGet]
        [Authorize(Roles = "Admin,HR")]
        public async Task<IActionResult> GetAllEmployees()
        {
            var employees =
                await _employeeService.GetAllEmployeesAsync();

            var response =
                employees.Select(e => new EmployeeResponseDto
                {
                    EmployeeId = e.EmployeeId,
                    FirstName = e.FirstName,
                    LastName = e.LastName,
                    Email = e.Email,
                    Department = e.Department,
                    JoiningDate = e.JoiningDate
                });

            return Ok(response);
        }

        // =====================================================
        // Admin + HR can add employees
        // =====================================================
        [HttpPost]
        [Authorize(Roles = "Admin,HR")]
        public async Task<IActionResult> AddEmployee(
            [FromBody] EmployeeRequestDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var employee = new Employee
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Email = dto.Email,
                Department = dto.Department,
                JoiningDate = dto.JoiningDate
            };

            await _employeeService.AddEmployeeAsync(employee);

            return CreatedAtAction(
                nameof(GetEmployee),
                new { id = employee.EmployeeId },
                new EmployeeResponseDto
                {
                    EmployeeId = employee.EmployeeId,
                    FirstName = employee.FirstName,
                    LastName = employee.LastName,
                    Email = employee.Email,
                    Department = employee.Department,
                    JoiningDate = employee.JoiningDate
                });
        }

        // =====================================================
        // Admin + HR can update employees
        // =====================================================
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin,HR")]
        public async Task<IActionResult> UpdateEmployee(
            int id,
            [FromBody] EmployeeRequestDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var employee = new Employee
            {
                EmployeeId = id,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Email = dto.Email,
                Department = dto.Department,
                JoiningDate = dto.JoiningDate
            };

            await _employeeService.UpdateEmployeeAsync(employee);

            return NoContent();
        }

        // =====================================================
        // Only Admin can delete employees
        // =====================================================
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            await _employeeService.DeleteEmployeeAsync(id);

            return NoContent();
        }
    }
}