var builder = WebApplication.CreateBuilder(args);

// Add MVC services
builder.Services.AddControllersWithViews();

var app = builder.Build();

app.UseStaticFiles();
app.UseRouting();

// DEFAULT ROUTING
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Restaurant}/{action=Index}/{id?}"
);

// CUSTOM ROUTE
app.MapControllerRoute(
    name: "foodRoute",
    pattern: "order-food",
    defaults: new { controller = "Restaurant", action = "Menu" }
);

app.Run();