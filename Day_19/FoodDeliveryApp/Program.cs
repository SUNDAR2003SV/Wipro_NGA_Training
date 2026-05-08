var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();
var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "foodOrder",
    pattern: "order-food",
    defaults: new { controller = "Restaurant", action = "OrderPage" });

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Restaurant}/{action=Menu}/{id?}");

app.MapControllers();
app.Run();