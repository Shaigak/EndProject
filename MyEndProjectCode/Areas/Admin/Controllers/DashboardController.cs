using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MyEndProjectCode.Areas.Admin.Controllers
{

    [Authorize(Roles = "Admin")]
    public class DashboardController : Controller
    {
        [Area("Admin")]
        public IActionResult Index(string viewName, string controllerName)
        {

            if (viewName == "Index" && controllerName == "Dashboard")
            {
                return View();
            }
            return RedirectToAction("AdminLogin", "Account");
           
        }
    }
}
