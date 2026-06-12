using HRManagement.Application.Services;
using HRManagement.Core.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HRManagement.Web.Controllers
{
    [Authorize(Roles = "Admin,HR")]
    public class EmployeeController : Controller
    {
        private readonly EmployeeService _service;

        public EmployeeController(EmployeeService service)
        {
            _service = service;
        }

        // ====================================
        // Employee List
        // ====================================

        public async Task<IActionResult> Index()
        {
            var employees =
                await _service.GetAllEmployeesAsync();

            return View(employees);
        }

        // ====================================
        // Employee Details
        // ====================================

        public async Task<IActionResult> Details(int id)
        {
            var employee =
                await _service.GetEmployeeAsync(id);

            return View(employee);
        }

        // ====================================
        // Create Employee (GET)
        // ====================================

        public IActionResult Create()
        {
            return View();
        }

        // ====================================
        // Create Employee (POST)
        // ====================================

        [HttpPost]
        public async Task<IActionResult> Create(Employee employee)
        {
            if (!ModelState.IsValid)
                return View(employee);

            await _service.AddEmployeeAsync(employee);

            return RedirectToAction(nameof(Index));
        }

        // ====================================
        // Edit Employee (GET)
        // ====================================

        public async Task<IActionResult> Edit(int id)
        {
            var employee =
                await _service.GetEmployeeAsync(id);

            return View(employee);
        }

        // ====================================
        // Edit Employee (POST)
        // ====================================

        [HttpPost]
        public async Task<IActionResult> Edit(Employee employee)
        {
            if (!ModelState.IsValid)
                return View(employee);

            await _service.UpdateEmployeeAsync(employee);

            return RedirectToAction(nameof(Index));
        }

        // ====================================
        // Delete Employee (GET)
        // ====================================

        public async Task<IActionResult> Delete(int id)
        {
            var employee =
                await _service.GetEmployeeAsync(id);

            return View(employee);
        }

        // ====================================
        // Delete Employee (POST)
        // ====================================

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _service.DeleteEmployeeAsync(id);

            return RedirectToAction(nameof(Index));
        }

        // ====================================
        // AJAX Employee List
        // ====================================

        [HttpGet]
        public async Task<IActionResult> GetEmployees()
        {
            var employees =
                await _service.GetAllEmployeesAsync();

            return Json(employees);
        }
    }
}