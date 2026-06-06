using HRManagement.Core.Entities;
using HRManagement.Core.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace HRManagement.Application.Services
{
    public class AuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;

        // Constructor Injection
        public AuthService(
            IUserRepository userRepository,
            IConfiguration configuration)
        {
            _userRepository = userRepository;
            _configuration = configuration;
        }

        // LOGIN METHOD
        public async Task<string?> Login(string username, string password)
        {
            // 1. Get user from DB
            var user = await _userRepository.GetByUsernameAsync(username);

            if (user == null)
                return null;

            // 2. Validate password (simple check for now)
            // Later we can upgrade to BCrypt hashing
            if (user.PasswordHash != password)
                return null;

            // 3. Generate JWT Token
            return GenerateToken(user);
        }

        // TOKEN GENERATION LOGIC
        private string GenerateToken(User user)
        {
            // 1. Create claims (data inside token)
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role)
            };

            // 2. Get secret key from appsettings.json
            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));

            // 3. Signing credentials
            var creds = new SigningCredentials(
                key, SecurityAlgorithms.HmacSha256);

            // 4. Create token
            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(
                    Convert.ToDouble(_configuration["Jwt:DurationInMinutes"])),
                signingCredentials: creds
            );

            // 5. Return token string
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}