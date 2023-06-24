using Microsoft.AspNetCore.Mvc;
using MyEndProjectCode.Data;
using MyEndProjectCode.Models;
using MyEndProjectCode.Services.Interfaces;
using MyEndProjectCode.ViewModels;
using System.Diagnostics;

namespace MyEndProjectCode.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;
        private readonly ISliderService _sliderService;
        private readonly IBannerService _bannerService;
        private readonly IPopularService _popularService;
        private readonly IBlogService _blogService;
        private readonly IBrandService _brandService;
        private readonly IProductService _productService;


        public HomeController(AppDbContext context, ISliderService sliderService, IBannerService bannerService, IPopularService popularService, IBlogService blogService, IBrandService brandService, IProductService productService)
        {
            _context = context;
            _sliderService = sliderService;
            _bannerService = bannerService;
            _popularService = popularService;
            _blogService = blogService;
            _brandService = brandService;
            _productService = productService;   
        }

        public async Task<IActionResult> Index()
        {
            List<Slider> sliders = await _sliderService.GetAll();
            Dictionary<string, string> settings = _context.Settings.AsEnumerable().ToDictionary(m => m.Key, m => m.Value);
            List<Banner> banners = await _bannerService.GetAll();
            List<Popular> populars = await _popularService.GetAll();
            List<Blog> blogs = await _blogService.GetAll();
            List<Brand> brands = await _brandService.GetAll();
            List<Product> products = await _productService.GetAll();
            HomeVM model = new()
            {
                Sliders = sliders,
                Settings=settings,
                Banners=banners,
                Populars=populars,
                Blogs=blogs,
                Brands=brands,
                Products=products
               

            };

            return View(model);
        }

    }
}