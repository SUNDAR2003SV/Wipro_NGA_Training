using BookStoreApp.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();

// Register repository
builder.Services.AddScoped<BookRepository>();

var app = builder.Build();

app.UseStaticFiles();
app.UseRouting();

app.MapRazorPages();

app.Run();