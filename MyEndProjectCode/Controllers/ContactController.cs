using Microsoft.AspNetCore.Mvc;

namespace MyEndProjectCode.Controllers
{
    public class ContactController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
