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

        public AuthService(
            IUserRepository userRepository,
            IConfiguration configuration)
        {
            _userRepository = userRepository;
            _configuration = configuration;
        }

        // ============================================
        // LOGIN
        // ============================================
        public async Task<string?> Login(
            string username,
            string password)
        {
            // Get user from database
            var user =
                await _userRepository
                .GetByUsernameAsync(username);

            if (user == null)
                return null;

            // Verify BCrypt hashed password
            bool isPasswordValid =
                BCrypt.Net.BCrypt.Verify(
                    password,
                    user.PasswordHash);

            if (!isPasswordValid)
                return null;

            // Generate JWT Token
            return GenerateToken(user);
        }

        // ============================================
        // JWT TOKEN GENERATION
        // ============================================
        private string GenerateToken(User user)
        {
            var claims = new[]
            {
                new Claim(
                    ClaimTypes.Name,
                    user.Username),

                new Claim(
                    ClaimTypes.Email,
                    user.Email),

                new Claim(
                    ClaimTypes.Role,
                    user.Role)
            };

            var key =
                new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(
                        _configuration["Jwt:Key"]!));

            var credentials =
                new SigningCredentials(
                    key,
                    SecurityAlgorithms.HmacSha256);

            var token =
                new JwtSecurityToken(
                    issuer:
                        _configuration["Jwt:Issuer"],

                    audience:
                        _configuration["Jwt:Audience"],

                    claims:
                        claims,

                    expires:
                        DateTime.Now.AddMinutes(
                            Convert.ToDouble(
                                _configuration["Jwt:DurationInMinutes"])),

                    signingCredentials:
                        credentials);

            return new JwtSecurityTokenHandler()
                .WriteToken(token);
        }
    }
}