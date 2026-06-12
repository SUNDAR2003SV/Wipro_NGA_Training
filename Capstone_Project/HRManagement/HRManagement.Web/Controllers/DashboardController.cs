using HRManagement.Infrastructure.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HRManagement.Web.Controllers
{
    [Authorize]
    public class DashboardController : Controller
    {
        private readonly HRDbContext _context;

        public DashboardController(HRDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            // Dashboard Statistics

            ViewBag.EmployeeCount =
                await _context.Employees.CountAsync();

            ViewBag.LeaveCount =
                await _context.Leaves.CountAsync();

            ViewBag.AuditCount =
                await _context.AuditLogs.CountAsync();

            return View();
        }
    }
}