using System.Security.Claims;
using HRManagement.Infrastructure.Data;
using HRManagement.Web.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HRManagement.Web.Controllers
{
    public class AccountController : Controller
    {
        // Database Context
        private readonly HRDbContext _context;

        // Constructor Injection
        public AccountController(HRDbContext context)
        {
            _context = context;
        }

        // =====================================================
        // LOGIN PAGE (GET)
        // Displays the login form to the user
        // URL: /Account/Login
        // =====================================================

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        // =====================================================
        // LOGIN PROCESS (POST)
        // Validates user credentials
        // Creates authentication cookie
        // Redirects user to Dashboard
        // =====================================================

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            // Check if form validation passes
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // -------------------------------------------------
            // Step 1: Find user by username
            // -------------------------------------------------

            var user = await _context.Users
                .FirstOrDefaultAsync(x =>
                    x.Username == model.Username);

            // User not found
            if (user == null)
            {
                ViewBag.Error = "Invalid username or password";
                return View(model);
            }

            // -------------------------------------------------
            // Step 2: Verify BCrypt Password Hash
            // -------------------------------------------------

            bool passwordValid =
                BCrypt.Net.BCrypt.Verify(
                    model.Password,
                    user.PasswordHash);

            // Password mismatch
            if (!passwordValid)
            {
                ViewBag.Error = "Invalid username or password";
                return View(model);
            }

            // -------------------------------------------------
            // Step 3: Create User Claims
            // Claims are stored inside Authentication Cookie
            // -------------------------------------------------

            var claims = new List<Claim>
            {
                // Logged-in username
                new Claim(
                    ClaimTypes.Name,
                    user.Username),

                // User Role
                new Claim(
                    ClaimTypes.Role,
                    user.Role)
            };

            // -------------------------------------------------
            // Step 4: Create Identity
            // -------------------------------------------------

            var identity = new ClaimsIdentity(
                claims,
                CookieAuthenticationDefaults.AuthenticationScheme);

            // -------------------------------------------------
            // Step 5: Create Principal
            // -------------------------------------------------

            var principal = new ClaimsPrincipal(identity);

            // -------------------------------------------------
            // Step 6: Sign In User
            // Creates Authentication Cookie
            // -------------------------------------------------

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                principal);

            // -------------------------------------------------
            // Step 7: Redirect to Dashboard
            // -------------------------------------------------

            return RedirectToAction(
                "Index",
                "Dashboard");
        }

        // =====================================================
        // LOGOUT
        // Removes Authentication Cookie
        // =====================================================

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(
                CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Login");
        }

        // =====================================================
        // ACCESS DENIED PAGE
        // Shown when user tries to access unauthorized page
        // =====================================================

        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}