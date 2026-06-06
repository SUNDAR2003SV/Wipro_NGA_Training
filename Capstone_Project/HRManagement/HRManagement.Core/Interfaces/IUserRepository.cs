using HRManagement.Core.Entities;

namespace HRManagement.Core.Interfaces
{
    public interface IUserRepository
    {
        // Find user by username
        Task<User?> GetByUsernameAsync(string username);

        // Create new user
        Task AddUserAsync(User user);
    }
}