using FocusOffical.Models;
using FocusOffical.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.JSInterop.Implementation;
using System.Text;

namespace FocusOffical.Controllers
{
    public class ShopController : Controller
    {
        private readonly ISessionService _session;
        private readonly IProductRepository _productRepository;
        private readonly IMailService _mailService;

        public ShopController(ISessionService session, IProductRepository productRepository, IMailService mailService)
        {
            _session = session;
            _productRepository = productRepository;
            _mailService = mailService;
        }

        public IActionResult Cart()
        {
            var cart = _session.GetSessionValue<List<Cart>>(HttpContext, "cart");
            ViewData["total"] = GetTotal(cart);
            ViewData["cart"] = cart;

            return View();
        }

        public IActionResult CheckOut()
        {
            var cart = _session.GetSessionValue<List<Cart>>(HttpContext, "cart");
            ViewData["total"] = GetTotal(cart);
            ViewData["cart"] = cart;

            return View();
        }

        [HttpPost]
        public IActionResult CheckOut([Bind("Name,FullAddress,AddressNote,PhoneNumber,Email,OrderNote")] Order order)
        {
            if(ModelState.IsValid)
            {
                var content = new StringBuilder();
                content.AppendLine($"Name: {order.Name}");
                content.AppendLine($"<br>Address: {order.FullAddress}");
                content.AppendLine($"<br>AddressNote: {order.AddressNote}");
                content.AppendLine($"<br>Phone number: {order.PhoneNumber}");
                content.AppendLine($"<br>Email: {order.Email}");
                content.AppendLine($"<br>Note: {order.OrderNote}");

                var order_cart = _session.GetSessionValue<List<Cart>>(HttpContext, "cart");

                content.AppendLine($"<br><br>Product: ");
                order_cart.ForEach(detail =>
                {
                    content.AppendLine($"<br>Product: {detail.Product.Name} - {detail.Product.Price.ToString("N0")} vnd; Quantity: {detail.Quantity}");
                });

                content.AppendLine($"<br>Total: {GetTotal(order_cart)}");

                var mailContent = new MailContent()
                {
                    To = "focusdnbrand@gmail.com",
                    Subject = "FOCUS OFFICAL - New order from customer",
                    Body = content.ToString()
                };

                _mailService.SendMailAsync(mailContent);

                _session.DeleteSession(HttpContext, "cart");

                TempData["Message"] = "Order successfully";

                return RedirectToAction("Index","Home");
            }

            var cart = _session.GetSessionValue<List<Cart>>(HttpContext, "cart");
            ViewData["total"] = GetTotal(cart);
            ViewData["cart"] = cart;

            return View();
        }

        [HttpPost]
        public async Task<JsonResult> AddToCart(int productId, int quantity)
        {
            List<Cart> cart = _session.GetSessionValue<List<Cart>>(HttpContext, "cart");

            var product = _productRepository.GetById(productId);

            var newDetail = new Cart()
            {
                ProductId = productId,
                Product = product,
                Quantity = quantity
            };

            if (cart == null)
            {
                //If new cart => create new list order
                cart = new List<Cart>()
                {
                    newDetail
                };
            }
            else
            {
                //Find product
                var result = cart.FirstOrDefault(p => p.ProductId == productId);

                //If exist in cart => remove -> prepare to add new
                if (result != null)
                {
                    if(result.Quantity == quantity)
                    {
                        return Json(new { success = false, message = "Product was exist in cart." });
                    }

                    cart.Remove(result);
                }

                //If quantity != 0 => add new
                if (quantity != 0)
                {
                    cart.Add(newDetail);
                }
            }

            _session.AddToSession(HttpContext, "cart", cart);

            var numberOfProduct = cart.Count;
            var total = GetTotal(cart);

            return Json(new { success = true, numberOfProduct = numberOfProduct, total = total }); ;
        }

        int GetTotal(List<Cart> cart)
        {
            if(cart == null || cart.Count == 0)
            {
                return 0;
            }

            var total = 0;

            foreach(var detail in cart)
            {
                var product = _productRepository.GetById(detail.ProductId);
                total += product.Price * detail.Quantity;
            }

            return total;
        }
    }
}
