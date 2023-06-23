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


        public HomeController(AppDbContext context, ISliderService sliderService, IBannerService bannerService)
        {
            _context = context;
            _sliderService = sliderService;
            _bannerService = bannerService;
        }

        public async Task<IActionResult> Index()
        {
            List<Slider> sliders = await _sliderService.GetAll();
            Dictionary<string, string> settings = _context.Settings.AsEnumerable().ToDictionary(m => m.Key, m => m.Value);
            List<Banner> banners = await _bannerService.GetAll();

            HomeVM model = new()
            {
                Sliders = sliders,
                Settings=settings,
                Banners=banners
               

            };

            return View(model);
        }

    }
}