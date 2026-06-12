using HRManagement.Infrastructure.ADO;
using HRManagement.Infrastructure.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HRManagement.Web.Controllers
{
    [Authorize(Roles = "Admin,HR")]
    public class AnalyticsController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly HRDbContext _context;

        public AnalyticsController(
            IConfiguration configuration,
            HRDbContext context)
        {
            _configuration = configuration;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            // ==================================
            // Employee Count (ADO.NET)
            // ==================================

            var repository =
                new EmployeeAdoRepository(
                    _configuration.GetConnectionString(
                        "DefaultConnection"));

            ViewBag.EmployeeCount =
                await repository.GetEmployeeCountAsync();

            // ==================================
            // Leave Analytics (EF Core)
            // ==================================

            ViewBag.TotalLeaves =
                await _context.Leaves.CountAsync();

            ViewBag.ApprovedLeaves =
                await _context.Leaves
                    .CountAsync(x => x.Status == "Approved");

            ViewBag.RejectedLeaves =
                await _context.Leaves
                    .CountAsync(x => x.Status == "Rejected");

            ViewBag.PendingLeaves =
                await _context.Leaves
                    .CountAsync(x => x.Status == "Pending");

            return View();
        }
    }
}