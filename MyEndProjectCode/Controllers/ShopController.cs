using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyEndProjectCode.Data;
using MyEndProjectCode.Helpers;
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

        public async Task <IActionResult> Index(int page = 1, int take =6)

        {

            List<Product> paginateProd = await _productService.GetPaginatedDatas(page, take);
            
            int pageCount = await GetPageCountAsync(take);

            Paginate<Product> paginateDatas = new(paginateProd, page, pageCount);

            List<Product> products = await _productService.GetAll();

            List<Product> allproducts = await _productService.GetAllProduct();


            List<Category> categories = await _categoryService.GetCategories();
           
            List<BrandPro> brandPros = await _context.BrandPros.Include(m=>m.ProductBrands).ToListAsync();
            
            List<Tag> tags = await _tagService.GetAllTags();


            ShopVM model = new ShopVM()
            {
                Products = products,
                Categories = categories,
                BrandPros = brandPros,
                Tags = tags,
                PaginateProduct = paginateDatas,
                AllProducts=allproducts

            };


            return View(model);
        }


      

        private async Task<int> GetPageCountAsync(int take)
        {
            var productCount = await _productService.GetCountAsync();

            return (int)Math.Ceiling((decimal)productCount / take);
        }


        public async Task<IActionResult> GetProductsByCategory(int id)
        {
            List<Product> products = await _context.ProductCategories.Where(m => m.Category.Id == id).Include(m=>m.Product).ThenInclude(m=>m.ProductImages).Select(m => m.Product).ToListAsync();

            return PartialView("_ProductsPartial", products);
        }



        public async Task<IActionResult> GetProductsByBrandName(int? id)
        {
            List<Product> products = await _context.ProductBrands.Where(m => m.BrandProId == id).Include(m=>m.Product).ThenInclude(m=>m.ProductImages).Select(m => m.Product).ToListAsync();

            return PartialView("_ProductsPartial", products);
        }

        public async Task<IActionResult> GetProductsTagName(int? id)
        {
            List<Product> products = await _context.ProductTags.Where(m => m.TagId == id).Include(m => m.Product).ThenInclude(m => m.ProductImages).Select(m => m.Product).ToListAsync();

            return PartialView("_ProductsPartial", products);
        }


        public async Task<IActionResult> GetAllProduct()
        {
            List<Product> products = await _productService.GetAllProduct();


            return PartialView("_ProductsPartial", products);
        }


        public async Task<IActionResult> MainSearch(string searchText)
        {
            var products = await _context.Products.Include(m => m.ProductImages).Include(m => m.ProductCategories).
           OrderByDescending(m => m.Id).Where(m => !m.SoftDelete && m.Name.ToLower().Trim().Contains(searchText.ToLower().Trim())).Take(6).ToListAsync();

            return View(products);
        }



    }
}
