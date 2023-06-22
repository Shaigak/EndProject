using Microsoft.AspNetCore.Mvc;

namespace MyEndProjectCode.Controllers
{
    public class CartController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
