using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyEndProjectCode.Data;
using MyEndProjectCode.Models;
using MyEndProjectCode.Services.Interfaces;
using MyEndProjectCode.ViewModels;

namespace MyEndProjectCode.Controllers
{
    public class ShopController : Controller
    {

        private readonly AppDbContext _context;
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        private readonly ITagService _tagService;


        public ShopController(AppDbContext context, IProductService productService, ICategoryService categoryService, ITagService tagService)
        {
            _context = context;
            _productService = productService;
            _categoryService = categoryService;
            _tagService = tagService;
        }

        public async Task <IActionResult> Index()
        {
            List<Product> products = await _productService.GetAll();
            List<Category> categories = await _categoryService.GetCategories();
            List<BrandPro> brandPros = await _context.BrandPros.ToListAsync();
            List<Tag> tags = await _tagService.GetAllTags();


            ShopVM model = new ShopVM()
            {
                Products = products,
                Categories = categories,
                BrandPros=brandPros,
                Tags=tags
                
            };


            return View(model);
        }
    }
}
