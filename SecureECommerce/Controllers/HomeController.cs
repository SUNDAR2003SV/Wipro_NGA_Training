using Microsoft.AspNetCore.Mvc;

namespace SecureECommerce.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}