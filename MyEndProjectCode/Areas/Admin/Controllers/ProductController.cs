using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyEndProjectCode.Data;
using MyEndProjectCode.Models;
using MyEndProjectCode.Services.Interfaces;

namespace MyEndProjectCode.Areas.Admin.Controllers
{

    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IProductService _productService;
        private readonly ITagService _tagService;
        private readonly ICategoryService _categoryService;

        public ProductController(AppDbContext context, IWebHostEnvironment webHostEnvironment, IProductService productService, ITagService tagService, ICategoryService categoryService)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
            _productService = productService;
            _tagService = tagService;
            _categoryService = categoryService;
        }

        public async Task<IActionResult> Index()
        {
            IEnumerable<Product> products = await _context.Products.Include(m => m.ProductTags).Include(m=>m.ProductImages).ToListAsync();

            return View(products);
        }
    }
}
