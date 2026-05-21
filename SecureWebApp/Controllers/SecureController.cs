using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace SecureWebApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]   // All endpoints require a valid JWT
    public class SecureController : ControllerBase
    {
        /// <summary>
        /// Accessible by ANY authenticated user (Admin or User)
        /// GET /api/secure/profile
        /// </summary>
        [HttpGet("profile")]
        public IActionResult GetProfile()
        {
            var username = User.FindFirstValue(ClaimTypes.Name);
            var role = User.FindFirstValue(ClaimTypes.Role);
            var userId = User.FindFirstValue("UserId");

            return Ok(new
            {
                message = "Profile accessed successfully.",
                username,
                role,
                userId
            });
        }

        /// <summary>
        /// Accessible by ADMIN role only
        /// GET /api/secure/admin
        /// </summary>
        [HttpGet("admin")]
        [Authorize(Roles = "Admin")]
        public IActionResult AdminOnly()
        {
            return Ok(new
            {
                message = "Welcome, Admin! You have full access.",
                data = "Sensitive admin data goes here."
            });
        }

        /// <summary>
        /// Accessible by both User and Admin roles
        /// GET /api/secure/dashboard
        /// </summary>
        [HttpGet("dashboard")]
        [Authorize(Roles = "User,Admin")]
        public IActionResult Dashboard()
        {
            var role = User.FindFirstValue(ClaimTypes.Role);
            return Ok(new
            {
                message = $"Dashboard loaded for role: {role}",
                data = "General user data."
            });
        }
    }
}