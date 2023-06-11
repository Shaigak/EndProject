using Microsoft.AspNetCore.Mvc;

namespace MyEndProjectCode.Controllers
{
    public class BlogController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }



        public IActionResult GetBlogView(int? id)
        {
            return View();
        }
    }
}
