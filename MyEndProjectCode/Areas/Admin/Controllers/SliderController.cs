using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyEndProjectCode.Areas.View_Models;
using MyEndProjectCode.Data;
using MyEndProjectCode.Models;

namespace MyEndProjectCode.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SliderController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public SliderController(AppDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }


        public async  Task<IActionResult> Index()
        {

            IEnumerable<Slider> sliders = await _context.Sliders.ToListAsync();
            return View(sliders);
            
        }



        [HttpGet]
        public async Task<IActionResult> Detail(int? id)
        {

            if (id == null) return BadRequest();

            Slider slider = await _context.Sliders.FirstOrDefaultAsync(m => m.Id == id);

            if (slider is null) return NotFound();

            return View(slider);

        }



        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Create(SliderCreateVM slider)
        {
            if (!ModelState.IsValid)
            {
                return View();  
            }

            foreach (var photo in slider.Photos)
            {
                if (!photo.ContentType.Contains("image/"))  
                {
                    ModelState.AddModelError("Photo", "File type must be image");

                    return View();

                }
            }

            foreach (var photo in slider.Photos)
            {
                string fileName = Guid.NewGuid().ToString() + " " + photo.FileName;  

                string path = Path.Combine(_webHostEnvironment.WebRootPath, "assets/images", fileName);  

                using (FileStream stream = new FileStream(path, FileMode.Create)) // Kompa sekil yuklemek ucun muhit yaradiriq stream yaradiriq 
                {
                    await photo.CopyToAsync(stream);
                }


                Slider newSliderImage = new()
                {
                    Image = fileName,
                    Title = slider.Title,
                    Description = slider.Description,
                    EndDesc = slider.EndDesc,
                };

                await _context.Sliders.AddAsync(newSliderImage);

                await _context.SaveChangesAsync();

            }
            return RedirectToAction(nameof(Index));


        }


        public async Task<IActionResult> Delete(int? id)
        {

            try
            {
                if (id == null) return BadRequest();
                Slider slider = await _context.Sliders.FirstOrDefaultAsync(m => m.Id == id);

                if (slider is null) return NotFound();

                string path = Path.Combine(_webHostEnvironment.WebRootPath, "assets/images", slider.Image); // root duzeldirik . 

                if (System.IO.File.Exists(path))
                {
                    System.IO.File.Delete(path);
                }

                _context.Sliders.Remove(slider);

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
