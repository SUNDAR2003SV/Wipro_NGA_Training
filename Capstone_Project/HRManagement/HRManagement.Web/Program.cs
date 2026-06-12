using HRManagement.Application.Services;
using HRManagement.Core.Interfaces;
using HRManagement.Infrastructure.Data;
using HRManagement.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// ======================================================
// MVC + Razor Pages
// ======================================================

builder.Services.AddControllersWithViews();

builder.Services.AddRazorPages();

// ======================================================
// SQL Server
// ======================================================

builder.Services.AddDbContext<HRDbContext>(options =>
{
    options.UseSqlServer(
        builder.Configuration.GetConnectionString(
            "DefaultConnection"));
});

// ======================================================
// Cookie Authentication
// ======================================================

builder.Services.AddAuthentication(
    CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Account/Login";

        options.AccessDeniedPath =
            "/Account/AccessDenied";

        options.ExpireTimeSpan =
            TimeSpan.FromHours(8);

        options.SlidingExpiration = true;
    });

// ======================================================
// Dependency Injection
// ======================================================

builder.Services.AddScoped<IEmployeeRepository,
                           EmployeeRepository>();

builder.Services.AddScoped<ILeaveRepository,
                           LeaveRepository>();

builder.Services.AddScoped<IUserRepository,
                           UserRepository>();

builder.Services.AddScoped<EmployeeService>();

builder.Services.AddScoped<LeaveService>();

builder.Services.AddScoped<AuthService>();

var app = builder.Build();

// ======================================================
// Pipeline
// ======================================================

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");

    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

// Authentication FIRST
app.UseAuthentication();

// Authorization SECOND
app.UseAuthorization();

// ======================================================
// Routing
// ======================================================

app.MapControllerRoute(
    name: "default",
    pattern:
    "{controller=Account}/{action=Login}/{id?}");

app.MapRazorPages();

app.Run();