using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MyEndProjectCode.Areas.Admin.Controllers
{

    [Authorize(Roles = "Admin")]
    public class DashboardController : Controller
    {
        [Area("Admin")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
