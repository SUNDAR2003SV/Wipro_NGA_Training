using Microsoft.AspNetCore.Mvc;

namespace SmartInventory_API_RoutingDemo.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet("/")]
        public IActionResult Index()
        {
            return Content("Welcome to the Smart Inventory API Routing Demo!");
        }
    public IActionResult Report()
        {
            return Content("This is the report page.");
        }
    }
}
