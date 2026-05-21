using SecureWebApp.Models;

namespace SecureWebApp.Services
{
    // Simulates a database of users
    public static class UserStore
    {
        public static List<User> Users = new()
        {
            new User
            {
                Id = 1,
                Username = "admin",
                // In real apps, use BCrypt or ASP.NET Identity to hash passwords
                PasswordHash = BCryptHash("admin123"),
                Role = "Admin"
            },
            new User
            {
                Id = 2,
                Username = "john",
                PasswordHash = BCryptHash("user123"),
                Role = "User"
            }
        };

        // Simple SHA256 hash for demo — use BCrypt in production
        private static string BCryptHash(string password)
        {
            using var sha = System.Security.Cryptography.SHA256.Create();
            var bytes = System.Text.Encoding.UTF8.GetBytes(password);
            var hash = sha.ComputeHash(bytes);
            return Convert.ToBase64String(hash);
        }

        public static User? Validate(string username, string password)
        {
            var hash = BCryptHash(password);
            return Users.FirstOrDefault(u =>
                u.Username == username && u.PasswordHash == hash);
        }
    }
}