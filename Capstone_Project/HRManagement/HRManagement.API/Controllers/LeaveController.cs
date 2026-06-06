using HRManagement.Application.Services;
using HRManagement.Core.DTOs.Leave;
using HRManagement.Core.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HRManagement.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class LeaveController : ControllerBase
    {
        private readonly LeaveService _leaveService;

        public LeaveController(LeaveService leaveService)
        {
            _leaveService = leaveService;
        }

        // Any authenticated user can apply for leave
        [HttpPost]
        [Authorize(Roles = "Admin,Manager,Employee")]
        public async Task<IActionResult> ApplyLeave(
            [FromBody] LeaveRequestDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var leave = new Leave
            {
                EmployeeId = dto.EmployeeId,
                StartDate = dto.StartDate,
                EndDate = dto.EndDate,
                Reason = dto.Reason,
                Status = "Pending"
            };

            await _leaveService.ApplyLeaveAsync(leave);

            return CreatedAtAction(nameof(GetAllLeaves), null,
                new LeaveResponseDto
                {
                    LeaveId = leave.LeaveId,
                    EmployeeId = leave.EmployeeId,
                    StartDate = leave.StartDate,
                    EndDate = leave.EndDate,
                    Reason = leave.Reason,
                    Status = leave.Status
                });
        }

        // Only Admin and Manager can view all leave requests
        [HttpGet]
        [Authorize(Roles = "Admin,Manager")]
        public async Task<IActionResult> GetAllLeaves()
        {
            var leaves = await _leaveService.GetAllLeavesAsync();

            var response = leaves.Select(l => new LeaveResponseDto
            {
                LeaveId = l.LeaveId,
                EmployeeId = l.EmployeeId,
                StartDate = l.StartDate,
                EndDate = l.EndDate,
                Reason = l.Reason,
                Status = l.Status
            });

            return Ok(response);
        }
    }
}