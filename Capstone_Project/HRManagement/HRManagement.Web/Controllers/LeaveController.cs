using HRManagement.Application.Services;
using HRManagement.Core.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HRManagement.Web.Controllers
{
    [Authorize]
    public class LeaveController : Controller
    {
        private readonly LeaveService _service;

        public LeaveController(LeaveService service)
        {
            _service = service;
        }

        // ==================================================
        // Employee Apply Leave
        // ==================================================

        [HttpGet]
        [Authorize(Roles = "Employee")]
        public IActionResult Apply()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Employee")]
        public async Task<IActionResult> Apply(Leave leave)
        {
            if (!ModelState.IsValid)
                return View(leave);

            await _service.ApplyLeaveAsync(leave);

            return RedirectToAction(nameof(MyLeaves));
        }

        // ==================================================
        // Employee View Leave Status
        // ==================================================

        [HttpGet]
        [Authorize(Roles = "Employee")]
        public async Task<IActionResult> MyLeaves()
        {
            var leaves =
                await _service.GetAllLeavesAsync();

            return View(leaves);
        }

        // ==================================================
        // HR / Manager Leave Approval Screen
        // ==================================================

        [HttpGet]
        [Authorize(Roles = "HR,Manager")]
        public async Task<IActionResult> Approval()
        {
            var leaves =
                await _service.GetAllLeavesAsync();

            return View(leaves);
        }

        // ==================================================
        // Approve Leave
        // ==================================================

        [HttpGet]
        [Authorize(Roles = "HR,Manager")]
        public async Task<IActionResult> Approve(int id)
        {
            await _service.ApproveLeaveAsync(id);

            return RedirectToAction(nameof(Approval));
        }

        // ==================================================
        // Reject Leave
        // ==================================================

        [HttpGet]
        [Authorize(Roles = "HR,Manager")]
        public async Task<IActionResult> Reject(int id)
        {
            await _service.RejectLeaveAsync(id);

            return RedirectToAction(nameof(Approval));
        }
    }
}