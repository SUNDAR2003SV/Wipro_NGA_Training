using HRManagement.Application.Services;
using HRManagement.Core.Entities;
using Microsoft.AspNetCore.Mvc;

namespace HRManagement.Web.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly EmployeeService _service;

        public EmployeeController(EmployeeService service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index()
        {
            var employees =
                await _service.GetAllEmployeesAsync();

            return View(employees);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Employee employee)
        {
            if (!ModelState.IsValid)
                return View(employee);

            await _service.AddEmployeeAsync(employee);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> GetEmployees()
        {
            // Get all employees from service
            var employees = await _service.GetAllEmployeesAsync();

            // Return JSON data
            return Json(employees);
        }
    }
}