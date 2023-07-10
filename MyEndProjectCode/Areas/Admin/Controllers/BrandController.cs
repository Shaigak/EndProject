using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyEndProjectCode.Areas.View_Models;
using MyEndProjectCode.Data;
using MyEndProjectCode.Models;

namespace MyEndProjectCode.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class BrandController : Controller
    {

        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public BrandController(AppDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<IActionResult> Index()
        {

            IEnumerable<Brand> brands = await _context.Brands.ToListAsync();
           
            return View(brands);
        }


        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }



        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Create(BrandCreateVM popular)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            foreach (var photo in popular.Photos)
            {
                if (!photo.ContentType.Contains("image/"))
                {
                    ModelState.AddModelError("Photo", "File type must be image");

                    return View();

                }
            }

            foreach (var photo in popular.Photos)
            {
                string fileName = Guid.NewGuid().ToString() + " " + photo.FileName;

                string path = Path.Combine(_webHostEnvironment.WebRootPath, "assets/images", fileName);

                using (FileStream stream = new FileStream(path, FileMode.Create)) // Kompa sekil yuklemek ucun muhit yaradiriq stream yaradiriq 
                {
                    await photo.CopyToAsync(stream);
                }


                Brand newBrand = new()
                {
                    Image = fileName,
                   

                };



                await _context.Brands.AddAsync(newBrand);



                await _context.SaveChangesAsync();


            }
            return RedirectToAction(nameof(Index));

        }


        public async Task<IActionResult> Delete(int? id)
        {

            try
            {
                if (id == null) return BadRequest();
                Brand brand = await _context.Brands.FirstOrDefaultAsync(m => m.Id == id);

                if (brand is null) return NotFound();

                string path = Path.Combine(_webHostEnvironment.WebRootPath, "assets/images", brand.Image); // root duzeldirik . 

                if (System.IO.File.Exists(path))
                {
                    System.IO.File.Delete(path);
                }

                _context.Brands.Remove(brand);

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

            Brand dbbrand = await _context.Brands.FirstOrDefaultAsync(m => m.Id == id);

            if (dbbrand is null) return NotFound();


            BrandUpdateVM model = new()
            {
                Id = dbbrand.Id,
                Image = dbbrand.Image,
               

            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, BrandUpdateVM brand)
        {

            try
            {
                if (id == null) return BadRequest();


                Brand dbBrand = await _context.Brands.FirstOrDefaultAsync(m => m.Id == id);

                if (dbBrand is null) return NotFound();


                BrandUpdateVM model = new()
                {
                    Id = dbBrand.Id,
                    Image = dbBrand.Image,
               
                };



                if (brand.Photo != null)
                {

                    if (!brand.Photo.ContentType.Contains("image/"))  // Typesinin image olb olmadiqini yoxlayur 
                    {
                        ModelState.AddModelError("Photo", "File type must be image");

                        return View(dbBrand);

                    }

                    string oldPath = Path.Combine(_webHostEnvironment.WebRootPath, "assets/images", dbBrand.Image); // wekilin rutunu gotururuk old path 


                    if (System.IO.File.Exists(oldPath))
                    {
                        System.IO.File.Delete(oldPath);
                    }

                    string fileName = Path.Combine(Guid.NewGuid().ToString() + "_" + brand.Photo.FileName);

                    string newPath = Path.Combine(_webHostEnvironment.WebRootPath, "assets/images", fileName);


                    using (FileStream stream = new FileStream(newPath, FileMode.Create)) // Kompa sekil yuklemek ucun muhit yaradiriq stream yaradiriq 
                    {
                        await brand.Photo.CopyToAsync(stream);
                    }

                    dbBrand.Image = fileName;

                }
                else
                {
                    Brand newBrand = new()
                    {
                        Image = dbBrand.Image
                    };

                    
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
