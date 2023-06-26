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


        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return BadRequest();

            Slider dbSlider = await _context.Sliders.FirstOrDefaultAsync(m => m.Id == id);

            if (dbSlider is null) return NotFound();


            SliderUpdateVM model = new()
            {
                Id = dbSlider.Id,
                Image = dbSlider.Image,
                Title = dbSlider.Title,
                Description = dbSlider.Description,
                EndDesc = dbSlider.EndDesc

            };

            return View(model);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, SliderUpdateVM slider)
        {

            try
            {
                if (id == null) return BadRequest();


                Slider dbSlider = await _context.Sliders.FirstOrDefaultAsync(m => m.Id == id);

                if (dbSlider is null) return NotFound();


                SliderUpdateVM model = new()
                {
                    Id = dbSlider.Id,
                    Image = dbSlider.Image,
                    Title = dbSlider.Title,
                    Description = dbSlider.Description,
                    EndDesc = dbSlider.EndDesc
                };



                if (slider.Photo != null)
                {

                    if (!slider.Photo.ContentType.Contains("image/"))  
                    {
                        ModelState.AddModelError("Photo", "File type must be image");

                        return View(dbSlider);

                    }

                    string oldPath = Path.Combine(_webHostEnvironment.WebRootPath, "assets/images", dbSlider.Image); 


                    if (System.IO.File.Exists(oldPath))
                    {
                        System.IO.File.Delete(oldPath);
                    }

                    string fileName = Path.Combine(Guid.NewGuid().ToString() + "_" + slider.Photo.FileName);

                    string newPath = Path.Combine(_webHostEnvironment.WebRootPath, "assets/images", fileName);


                    using (FileStream stream = new FileStream(newPath, FileMode.Create)) 
                    {
                        await slider.Photo.CopyToAsync(stream);
                    }

                    dbSlider.Image = fileName;

                }
                else
                {
                    Slider newSlider = new()
                    {
                        Image = dbSlider.Image
                    };
                }

                dbSlider.EndDesc = slider.EndDesc;
                dbSlider.Description = slider.Description;
                dbSlider.Title = slider.Title;



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
