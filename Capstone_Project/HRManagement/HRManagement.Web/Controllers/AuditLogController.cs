using HRManagement.Infrastructure.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HRManagement.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AuditLogController : Controller
    {
        private readonly HRDbContext _context;

        public AuditLogController(HRDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var logs = await _context.AuditLogs
                                     .OrderByDescending(x => x.ActionDate)
                                     .ToListAsync();

            return View(logs);
        }
    }
}