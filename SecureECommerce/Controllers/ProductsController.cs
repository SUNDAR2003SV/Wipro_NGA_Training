using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SecureECommerce.Controllers
{
    [Authorize]  // Any logged-in user
    public class ProductsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}