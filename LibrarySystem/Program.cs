using LibrarySystem.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<LibraryDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("LibraryConnection")));

builder.Services.AddControllers();

var app = builder.Build();

app.MapControllers();

app.Run();