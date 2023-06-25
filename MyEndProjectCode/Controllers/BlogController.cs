using Microsoft.AspNetCore.Mvc;
using MyEndProjectCode.Data;
using MyEndProjectCode.Models;
using MyEndProjectCode.Services.Interfaces;
using MyEndProjectCode.ViewModels;

namespace MyEndProjectCode.Controllers
{
    public class BlogController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IBlogService _blogService;

        public BlogController(IBlogService blogService, AppDbContext context)
        {
            _blogService = blogService;
            _context = context;
        }

        public async Task <IActionResult> Index()
        {
            List<Blog> blogs = await _blogService.GetAll();
            Dictionary<string, string> settings = _context.Settings.AsEnumerable().ToDictionary(m => m.Key, m => m.Value);


            BlogVM model = new BlogVM()
            {
                Blogs=blogs,
                Settings = settings,

            };




            return View(model);
        }



        public async Task<IActionResult> GetBlogView(int? id)
        {

            List<Blog> blogs = await _blogService.GetAll();


            BlogDetailVM model = new()
            {
                Blogs = blogs,
              


            };

            return View(model);
        }
    }
}
