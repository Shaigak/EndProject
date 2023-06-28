using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyEndProjectCode.Areas.View_Models;
using MyEndProjectCode.Data;
using MyEndProjectCode.Models;
using MyEndProjectCode.Services.Interfaces;

namespace MyEndProjectCode.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly ICategoryService _categoryService;

        public CategoryController(AppDbContext context, IWebHostEnvironment webHostEnvironment, ICategoryService categoryService)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
            _categoryService = categoryService;
        }

        public async Task <IActionResult> Index()
        {
            IEnumerable<Category> categories = await _categoryService.GetCategories();

            return View(categories);
        }


        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CategoryCreateVM category)
        {

            var existdata = await _context.Categories.FirstOrDefaultAsync(m => m.Name == category.Name);

            if (existdata is not null)

            {

                ModelState.AddModelError("Name", "This data already exist");
                return View();
            }
            Category newCategory = new()
            {
                Name = category.Name

            };

            await _context.Categories.AddAsync(newCategory);



            await _context.SaveChangesAsync();



            return RedirectToAction(nameof(Index));

        }


        public async Task<IActionResult> Delete(int? id)
        {

            try
            {
                if (id == null) return BadRequest();
                Category cate = await _context.Categories.FirstOrDefaultAsync(m => m.Id == id);


                if (cate is null) return NotFound();



                _context.Categories.Remove(cate);

                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {

                throw;
            }

        }



        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id is null) return BadRequest();
            Category category = await _context.Categories.FindAsync(id);
            if (category is null) return NotFound();

            return View(category);
        }


        [HttpPost]
        public async Task<IActionResult> Edit(int? id, Category category)
        {
            if (id is null) return BadRequest();
            Category dbCategory = await _context.Categories.AsNoTracking().FirstOrDefaultAsync(m => m.Id == id);
            if (category is null) return NotFound();

            if (dbCategory.Name == category.Name)
            {
                return RedirectToAction(nameof(Index));
            }

            _context.Categories.Update(category);

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

    }
}
