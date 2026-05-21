using Microsoft.AspNetCore.Mvc;
using SecureWebApp.Models;
using SecureWebApp.Services;

namespace SecureWebApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly JwtTokenService _tokenService;

        public AuthController(JwtTokenService tokenService)
        {
            _tokenService = tokenService;
        }

        /// <summary>
        /// Login and receive a JWT token
        /// POST /api/auth/login
        /// </summary>
        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            // Validate credentials against the user store
            var user = UserStore.Validate(request.Username, request.Password);

            if (user == null)
                return Unauthorized(new { message = "Invalid username or password." });

            // Generate and return the JWT token
            var response = _tokenService.GenerateToken(user);
            return Ok(response);
        }
    }
}