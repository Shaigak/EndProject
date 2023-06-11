using Microsoft.AspNetCore.Mvc;
using MyEndProjectCode.Data;
using MyEndProjectCode.Models;
using System.Diagnostics;

namespace MyEndProjectCode.Controllers
{
    public class HomeController : Controller
    {
     

        public IActionResult Index()
        {
            return View();
        }

    }
}