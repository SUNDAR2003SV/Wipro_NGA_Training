using Microsoft.AspNetCore.Mvc;

namespace RoutingDemoMvc.Controllers
{
    public class RestaurantController : Controller
    {
        // DEFAULT ROUTE → /
        public IActionResult Index()
        {
            return View();
        }

        // CUSTOM ROUTE → /order-food
        public IActionResult Menu()
        {
            return View();
        }

        // ATTRIBUTE ROUTING → /details/5
        [Route("details/{id}")]
        public IActionResult Details(int id)
        {
            ViewBag.Id = id;
            return View();
        }
    }
}