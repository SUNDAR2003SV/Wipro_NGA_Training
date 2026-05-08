using Microsoft.AspNetCore.Mvc;

namespace FoodDeliveryApp.Controllers
{
    public class RestaurantController : Controller
    {
        // Works with conventional: /Restaurant/Menu  OR attribute: /restaurant/menu
        [Route("restaurant/menu")]
        public IActionResult Menu()
        {
            return Content("Restaurant Menu Page");
        }

        [Route("restaurant/details/{id:int}")] // US-104: constraint
        public IActionResult Details(int id)
        {
            return Content("Restaurant ID: " + id);
        }

        public IActionResult OrderPage() // For custom route /order-food
        {
            return Content("Food Ordering Page");
        }
    }
}