using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SecureECommerce.Controllers
{
    [Authorize(Roles = "Seller")]  // Seller only
    public class SellerController : Controller
    {
        public IActionResult Dashboard()
        {
            return View();
        }
    }
}