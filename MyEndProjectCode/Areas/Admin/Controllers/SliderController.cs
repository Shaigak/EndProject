using Microsoft.AspNetCore.Mvc;

namespace MyEndProjectCode.Areas.Admin.Controllers
{
    public class SliderController : Controller
    {
        [Area("Admin")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
