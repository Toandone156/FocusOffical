using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace FocusOffical.Controllers
{
    public class HomeController : Controller
    {
        public HomeController()
        {
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }
    }
}