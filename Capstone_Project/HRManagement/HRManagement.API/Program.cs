using System.Text;
using HRManagement.API.Middleware;
using HRManagement.Application.Services;
using HRManagement.Core.Interfaces;
using HRManagement.Infrastructure.Data;
using HRManagement.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// ── Controllers ────────────────────────────────────────────────
builder.Services.AddControllers();

// ── Swagger ────────────────────────────────────────────────────
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Enter: Bearer {your JWT token}"
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id   = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

// ── Database ───────────────────────────────────────────────────
builder.Services.AddDbContext<HRDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")));

// ── CORS ───────────────────────────────────────────────────────
builder.Services.AddCors(options =>
{
    options.AddPolicy("HRCorsPolicy", policy =>
    {
        policy
            .WithOrigins(
                "https://localhost:7108",  // Swagger / API
                "https://localhost:7200") // MVC Web project
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

// ── Dependency Injection ───────────────────────────────────────
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddScoped<EmployeeService>();

builder.Services.AddScoped<ILeaveRepository, LeaveRepository>();
builder.Services.AddScoped<LeaveService>();

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<AuthService>();

// ── JWT Authentication ─────────────────────────────────────────
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!))
    };
});

// ── Build ──────────────────────────────────────────────────────
var app = builder.Build();

// ── Swagger (dev only) ─────────────────────────────────────────
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// ── Security headers ───────────────────────────────────────────
app.UseMiddleware<SecurityHeadersMiddleware>();

// ── Global exception handler ───────────────────────────────────
app.UseMiddleware<ExceptionMiddleware>();

// ── HSTS (production only) ────────────────────────────────────
if (!app.Environment.IsDevelopment())
{
    app.UseHsts();
}

// ── HTTPS redirect ────────────────────────────────────────────
app.UseHttpsRedirection();

// ── CORS ──────────────────────────────────────────────────────
app.UseCors("HRCorsPolicy");

// ── Auth pipeline ─────────────────────────────────────────────
app.UseAuthentication(); // validates JWT token
app.UseAuthorization();  // enforces [Authorize(Roles=...)]

app.MapControllers();

app.Run();