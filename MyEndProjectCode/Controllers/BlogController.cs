using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

            if (id == null) return BadRequest();

            Blog blog = await _blogService.GetFullDataById((int)id);

            ViewBag.BlogId = id;


            List<Blog> blogs = await _blogService.GetAll();

            List<Blog> getBlogsRecently = await _blogService.GetBlogsRecently();

            Dictionary<string, string> settings = _context.Settings.AsEnumerable().ToDictionary(m => m.Key, m => m.Value);

            List<BlogComment> comments = await _context.BlogComments.Where(m => m.BlogId == id).ToListAsync();

            CommentVM commentVM = new();


            BlogDetailVM model = new()
            {
                Blogs = blogs,
                BlogImages=blog.Images,
                Title=blog.Title,
                Description=blog.Description,
                GetBlogsRecently=getBlogsRecently,
                Settings=settings,
                BlogComments=comments,
                CommentVM=commentVM
              


            };

            return View(model);
        }




        [HttpPost, Authorize]
        public async Task<IActionResult> PostComment(BlogDetailVM model, string userId, int blogId)
        {

            if (model.CommentVM.Message == null) return RedirectToAction(nameof(GetBlogView), new { id = blogId });

            BlogComment blogComment = new()
            {
                FullName = model.CommentVM.FullName,
                Email = model.CommentVM.Email,
                Subject = model.CommentVM.Subject,
                Message = model.CommentVM.Message,
                AppUserId = userId,
                BlogId = blogId

            };

            await _context.BlogComments.AddAsync(blogComment);

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(GetBlogView), new { id = blogId });



        }
    }
}
