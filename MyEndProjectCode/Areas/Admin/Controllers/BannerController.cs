using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyEndProjectCode.Areas.View_Models;
using MyEndProjectCode.Data;
using MyEndProjectCode.Models;

namespace MyEndProjectCode.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BannerController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public BannerController(AppDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<IActionResult> Index()
        {

            IEnumerable<Banner> banners = await _context.Banners.ToListAsync();
            return View(banners);
            
        }


        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Create(BannerCreateVM banner)
        {
            if (!ModelState.IsValid)
            {
                return View();  // Eger sekil secmeyibse View return elesin 
            }

            foreach (var photo in banner.Photos)
            {
                if (!photo.ContentType.Contains("image/"))  // Typesinin image olb olmadiqini yoxlayur 
                {
                    ModelState.AddModelError("Photo", "File type must be image");

                    return View();

                }
            }

            foreach (var photo in banner.Photos)
            {
                string fileName = Guid.NewGuid().ToString() + " " + photo.FileName; // herdefe yeni ad duzeldirik . 

                string path = Path.Combine(_webHostEnvironment.WebRootPath, "assets/images", fileName); // root duzeldirik . 

                using (FileStream stream = new FileStream(path, FileMode.Create)) // Kompa sekil yuklemek ucun muhit yaradiriq stream yaradiriq 
                {
                    await photo.CopyToAsync(stream);
                }


                Banner newBanner = new()
                {
                    Image = fileName,
                    Description=banner.Description

                };

                await _context.Banners.AddAsync(newBanner);



                await _context.SaveChangesAsync();


            }
            return RedirectToAction(nameof(Index));

        }

        public async Task<IActionResult> Delete(int? id)
        {

            try
            {
                if (id == null) return BadRequest();
                Banner banner = await _context.Banners.FirstOrDefaultAsync(m => m.Id == id);

                if (banner is null) return NotFound();

                string path = Path.Combine(_webHostEnvironment.WebRootPath, "assets/images", banner.Image); // root duzeldirik . 

                if (System.IO.File.Exists(path))
                {
                    System.IO.File.Delete(path);
                }

                _context.Banners.Remove(banner);

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

            Banner dbBrand = await _context.Banners.FirstOrDefaultAsync(m => m.Id == id);

            if (dbBrand is null) return NotFound();


            BannerUpdateVM model = new()
            {
                Id = dbBrand.Id,
                Image = dbBrand.Image,
                Description = dbBrand.Description,

            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, BannerUpdateVM banner)
        {

            try
            {
                if (id == null) return BadRequest();


                Banner dbBanner = await _context.Banners.FirstOrDefaultAsync(m => m.Id == id);

                if (dbBanner is null) return NotFound();


                BannerUpdateVM model = new()
                {
                    Id = dbBanner.Id,
                    Image = dbBanner.Image,
                    Description=dbBanner.Description

                };



                if (banner.Photo != null)
                {

                    if (!banner.Photo.ContentType.Contains("image/"))  // Typesinin image olb olmadiqini yoxlayur 
                    {
                        ModelState.AddModelError("Photo", "File type must be image");

                        return View(dbBanner);

                    }

                    string oldPath = Path.Combine(_webHostEnvironment.WebRootPath, "assets/images", dbBanner.Image); // wekilin rutunu gotururuk old path 


                    if (System.IO.File.Exists(oldPath))
                    {
                        System.IO.File.Delete(oldPath);
                    }

                    string fileName = Path.Combine(Guid.NewGuid().ToString() + "_" + banner.Photo.FileName);

                    string newPath = Path.Combine(_webHostEnvironment.WebRootPath, "assets/images/", fileName);


                    using (FileStream stream = new FileStream(newPath, FileMode.Create)) // Kompa sekil yuklemek ucun muhit yaradiriq stream yaradiriq 
                    {
                        await banner.Photo.CopyToAsync(stream);
                    }

                    dbBanner.Image = fileName;

                }
                else
                {
                    Banner newBanner = new()
                    {
                        Image = dbBanner.Image
                    };

                    dbBanner.Description = banner.Description;
                }

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
