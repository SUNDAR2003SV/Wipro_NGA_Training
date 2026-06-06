using HRManagement.Application.Services;
using HRManagement.Core.Interfaces;
using HRManagement.Infrastructure.Data;
using HRManagement.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// ========================================
// Add MVC Services
// ========================================

builder.Services.AddControllersWithViews();

// Razor Pages Support
builder.Services.AddRazorPages();

// ========================================
// Configure SQL Server
// ========================================

builder.Services.AddDbContext<HRDbContext>(options =>
{
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection"));
});

// ========================================
// Dependency Injection
// ========================================

// Employee Repository
builder.Services.AddScoped<IEmployeeRepository,
                           EmployeeRepository>();

// Leave Repository
builder.Services.AddScoped<ILeaveRepository,
                           LeaveRepository>();

// Employee Service
builder.Services.AddScoped<EmployeeService>();

// Leave Service
builder.Services.AddScoped<LeaveService>();

var app = builder.Build();

// ========================================
// Configure HTTP Pipeline
// ========================================

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");

    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

// MVC Routing
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Employee}/{action=Index}/{id?}");

// Razor Pages Routing
app.MapRazorPages();

app.Run();