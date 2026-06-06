using HRManagement.Core.Entities;

namespace HRManagement.Core.Interfaces
{
    public interface ILeaveRepository
    {
        Task AddAsync(Leave leave);

        Task<IEnumerable<Leave>> GetAllAsync();

        Task<Leave?> GetByIdAsync(int id);

        Task UpdateAsync(Leave leave);

        Task DeleteAsync(int id);
    }
}