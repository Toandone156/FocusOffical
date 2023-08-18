using FocusOffical.Models;
using FocusOffical.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Text;

namespace FocusOffical.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProductRepository _productRepository;
        private readonly ISessionService _session;
        private readonly IMailService _mailService;

        public HomeController(IProductRepository productRepository, ISessionService session, IMailService mailService)
        {
            _productRepository = productRepository;
            _session = session;
            _mailService = mailService;
        }

        public IActionResult Index()
        {
            var cart = _session.GetSessionValue<List<Cart>>(HttpContext, "cart");

            ViewBag.numberOfProduct = cart?.Count ?? 0;
            ViewBag.total = GetTotal(cart);

            var products = _productRepository.GetAll();
            return View(products);
        }

        public IActionResult Contact()
        {
            var cart = _session.GetSessionValue<List<Cart>>(HttpContext, "cart");

            ViewBag.numberOfProduct = cart?.Count ?? 0;
            ViewBag.total = GetTotal(cart);

            return View();
        }

        public IActionResult About()
        {
            var cart = _session.GetSessionValue<List<Cart>>(HttpContext, "cart");

            ViewBag.numberOfProduct = cart?.Count ?? 0;
            ViewBag.total = GetTotal(cart);

            return View();
        }

        [HttpPost]
        public IActionResult Contact([Bind("Name,Email,Message")] Feedback feedback)
        {
            if (ModelState.IsValid)
            {
                var content = new StringBuilder();
                content.AppendLine($"Name: {feedback.Name}");
                content.AppendLine($"<br>Email: {feedback.Email}");
                content.AppendLine($"<br>Message: {feedback.Message}");

                var mailContent = new MailContent()
                {
                    To = "focusdnbrand@gmail.com",
                    Subject = "FOCUS OFFICAL - New feedback from customer",
                    Body = content.ToString()
                };

                _mailService.SendMailAsync(mailContent);

                TempData["Message"] = "Thanks for your feedback";

                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        int GetTotal(List<Cart> cart)
        {
            if (cart == null || cart.Count == 0)
            {
                return 0;
            }

            var total = 0;

            foreach (var detail in cart)
            {
                var product = _productRepository.GetById(detail.ProductId);
                total += product.Price * detail.Quantity;
            }

            return total;
        }
    }
}