using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyEndProjectCode.Areas.View_Models;
using MyEndProjectCode.Data;
using MyEndProjectCode.Models;
using MyEndProjectCode.Services.Interfaces;

namespace MyEndProjectCode.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BlogController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IBlogService _blogService;

        public BlogController(AppDbContext context, IWebHostEnvironment webHostEnvironment, IBlogService blogService)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
            _blogService = blogService;

        }
        public async Task<IActionResult> Index()
        {
            IEnumerable<Blog> blogs = await _context.Blogs.Include(m => m.Images).ToListAsync();


            return View(blogs);
        }

        [HttpGet]
        public async Task<IActionResult> Detail(int? id)
        {

            if (id == null) return BadRequest();

            Blog blog = await _context.Blogs.Include(m => m.Images).FirstOrDefaultAsync(m => m.Id == id);

            if (blog is null) return NotFound();

            return View(blog);

        }



        [HttpGet]
        public async Task<IActionResult> Create()
        {

            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BlogCreateVM blog)
        {
            if (!ModelState.IsValid)
            {
                return View();  // Eger sekil secmeyibse View return elesin 
            }

            foreach (var photo in blog.Photos)
            {
                if (!photo.ContentType.Contains("image/"))  // Typesinin image olb olmadiqini yoxlayur 
                {
                    ModelState.AddModelError("Photo", "File type must be image");

                    return View();

                }
            }


            List<BlogImage> blogImages = new();

            foreach (var photo in blog.Photos)
            {
                string fileName = Guid.NewGuid().ToString() + " " + photo.FileName; // herdefe yeni ad duzeldirik . 

                string path = Path.Combine(_webHostEnvironment.WebRootPath, "assets/images", fileName); // root duzeldirik . 

                using (FileStream stream = new FileStream(path, FileMode.Create)) // Kompa sekil yuklemek ucun muhit yaradiriq stream yaradiriq 
                {
                    await photo.CopyToAsync(stream);
                }


                BlogImage blogImage = new()
                {
                    Image = fileName

                };

                blogImages.Add(blogImage);

            }

            Blog newBlog = new()
            {
                Images = blogImages,
                Title = blog.Title,
                Description = blog.Description,

            };

            await _context.BlogImages.AddRangeAsync(blogImages);


            await _context.Blogs.AddAsync(newBlog);

            await _context.SaveChangesAsync();


            return RedirectToAction(nameof(Index));

        }

        public async Task<IActionResult> Delete(int? id)
        {

            Blog blog = await _context.Blogs.FirstOrDefaultAsync(m => m.Id == id);

            _context.Blogs.Remove(blog);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }



        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return BadRequest();

            Blog dbBlog = await _blogService.GetFullDataById((int)id);

            if (dbBlog is null) return NotFound();

            BlogUpdateVM model = new()
            {
                Id = dbBlog.Id,
                Images = dbBlog.Images,
                Description = dbBlog.Description,
                Title = dbBlog.Title

            };

            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, BlogUpdateVM model)
        {


            try
            {
                if (id == null) return BadRequest();
                Blog dbBlog = await _blogService.GetFullDataById((int)id);
                if (dbBlog == null) return NotFound();
                //ViewBag.categories = await GetCategoriesAsync();


                BlogUpdateVM newBlog = new()
                {
                    Images = dbBlog.Images,
                    Title = dbBlog.Title,
                    Description = dbBlog.Description,

                };


                if (!ModelState.IsValid)
                {
                    return View(newBlog);
                }

                if (model.Photos != null)
                {
                    foreach (var item in model.Photos)
                    {
                        if (!item.ContentType.Contains("image/"))
                        {
                            ModelState.AddModelError("Photo", "File type must be image");

                            return View(newBlog);

                        }
                    }

                    foreach (var item in dbBlog.Images)
                    {
                        string oldPath = Path.Combine(_webHostEnvironment.WebRootPath, "assets/images", item.Image);


                        if (System.IO.File.Exists(oldPath))
                        {
                            System.IO.File.Delete(oldPath);
                        }
                    }

                    List<BlogImage> blogImages = new();

                    foreach (var item in model.Photos)
                    {

                        string fileName = Guid.NewGuid().ToString() + " " + item.FileName;
                        string newPath = Path.Combine(_webHostEnvironment.WebRootPath, "assets/images", fileName);
                        using (FileStream stream = new FileStream(newPath, FileMode.Create))
                        {
                            await item.CopyToAsync(stream);
                        }

                        BlogImage blogImage = new()
                        {
                            Image = fileName
                        };
                        blogImages.Add(blogImage);
                    }
                    //productImages.FirstOrDefault().IsMain = true;
                    _context.BlogImages.AddRange(blogImages);
                    dbBlog.Images = blogImages;
                }
                else
                {
                    {
                        Blog blog = new()
                        {
                            Images = dbBlog.Images
                        };
                    }


                }

                dbBlog.Description = model.Description;
                dbBlog.Title = model.Title;

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {

                throw;
            }





        }
    }
}