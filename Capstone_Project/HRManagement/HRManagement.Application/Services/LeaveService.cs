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

        // Apply Leave
        public async Task ApplyLeaveAsync(Leave leave)
        {
            // Business Rule:
            // Start date cannot be after end date
            if (leave.StartDate > leave.EndDate)
            {
                throw new InvalidLeaveException(
                    "Start date cannot be greater than end date.");
            }

            // Business Rule:
            // Leave cannot be applied for past dates
            if (leave.StartDate.Date < DateTime.Today)
            {
                throw new InvalidLeaveException(
                    "Leave cannot be applied for past dates.");
            }

            // Default Status
            leave.Status = "Pending";

            await _leaveRepository.AddAsync(leave);
        }

        // Get all leave requests
        public async Task<IEnumerable<Leave>> GetAllLeavesAsync()
        {
            return await _leaveRepository.GetAllAsync();
        }
    }
}