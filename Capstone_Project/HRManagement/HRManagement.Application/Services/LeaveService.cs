using HRManagement.Core.Entities;
using HRManagement.Core.Exceptions;
using HRManagement.Core.Interfaces;

namespace HRManagement.Application.Services
{
    public class LeaveService
    {
        // Repository dependency
        private readonly ILeaveRepository _leaveRepository;

        // Constructor Injection
        public LeaveService(ILeaveRepository leaveRepository)
        {
            _leaveRepository = leaveRepository;
        }

        // ==================================================
        // Apply Leave
        // ==================================================
        public async Task ApplyLeaveAsync(Leave leave)
        {
            // Start date cannot be after end date
            if (leave.StartDate > leave.EndDate)
            {
                throw new InvalidLeaveException(
                    "Start date cannot be greater than end date.");
            }

            // Cannot apply for past dates
            if (leave.StartDate.Date < DateTime.Today)
            {
                throw new InvalidLeaveException(
                    "Leave cannot be applied for past dates.");
            }

            // Default status
            leave.Status = "Pending";

            await _leaveRepository.AddAsync(leave);
        }

        // ==================================================
        // Get All Leaves
        // ==================================================
        public async Task<IEnumerable<Leave>> GetAllLeavesAsync()
        {
            return await _leaveRepository.GetAllAsync();
        }

        // ==================================================
        // Approve Leave
        // ==================================================
        public async Task ApproveLeaveAsync(int leaveId)
        {
            var leave =
                await _leaveRepository.GetByIdAsync(leaveId);

            if (leave == null)
            {
                throw new InvalidLeaveException(
                    "Leave request not found.");
            }

            leave.Status = "Approved";

            await _leaveRepository.UpdateAsync(leave);
        }

        // ==================================================
        // Reject Leave
        // ==================================================
        public async Task RejectLeaveAsync(int leaveId)
        {
            var leave =
                await _leaveRepository.GetByIdAsync(leaveId);

            if (leave == null)
            {
                throw new InvalidLeaveException(
                    "Leave request not found.");
            }

            leave.Status = "Rejected";

            await _leaveRepository.UpdateAsync(leave);
        }
    }
}