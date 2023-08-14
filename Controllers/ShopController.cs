using Microsoft.AspNetCore.Mvc;

namespace FocusOffical.Controllers
{
    public class ShopController : Controller
    {
        public IActionResult Cart()
        {
            return View();
        }

        public IActionResult CheckOut()
        {
            return View();
        }
    }
}
