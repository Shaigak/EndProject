using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyEndProjectCode.Areas.View_Models;
using MyEndProjectCode.Data;
using MyEndProjectCode.Models;

namespace MyEndProjectCode.Areas.Admin.Controllers
{

    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    public class PopularController : Controller
    {

        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public PopularController(AppDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }



        public async Task<IActionResult> Index()
        {
            IEnumerable<Popular> populars = await _context.Populars.ToListAsync();
            return View(populars);

        }


        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Create(PopularCreateVM popular)
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


                Popular newPopular = new()
                {
                    Image = fileName,
                    Name = popular.Name

                };



                await _context.Populars.AddAsync(newPopular);



                await _context.SaveChangesAsync();


            }
            return RedirectToAction(nameof(Index));

        }


        public async Task<IActionResult> Delete(int? id)
        {

            try
            {
                if (id == null) return BadRequest();
                Popular popular = await _context.Populars.FirstOrDefaultAsync(m => m.Id == id);

                if (popular is null) return NotFound();

                string path = Path.Combine(_webHostEnvironment.WebRootPath, "assets/images", popular.Image); // root duzeldirik . 

                if (System.IO.File.Exists(path))
                {
                    System.IO.File.Delete(path);
                }

                _context.Populars.Remove(popular);

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

            Popular dbPopular = await _context.Populars.FirstOrDefaultAsync(m => m.Id == id);

            if (dbPopular is null) return NotFound();


            PopularUpdateVM model = new()
            {
                Id = dbPopular.Id,
                Image = dbPopular.Image,
                Name = dbPopular.Name,

            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, PopularUpdateVM popular)
        {

            try
            {
                if (id == null) return BadRequest();


                Popular dbPopular = await _context.Populars.FirstOrDefaultAsync(m => m.Id == id);

                if (dbPopular is null) return NotFound();


                PopularUpdateVM model = new()
                {
                    Id = dbPopular.Id,
                    Image = dbPopular.Image,
                    Name = dbPopular.Name

                };



                if (popular.Photo != null)
                {

                    if (!popular.Photo.ContentType.Contains("image/"))  // Typesinin image olb olmadiqini yoxlayur 
                    {
                        ModelState.AddModelError("Photo", "File type must be image");

                        return View(dbPopular);

                    }

                    string oldPath = Path.Combine(_webHostEnvironment.WebRootPath, "assets/images", dbPopular.Image); // wekilin rutunu gotururuk old path 


                    if (System.IO.File.Exists(oldPath))
                    {
                        System.IO.File.Delete(oldPath);
                    }

                    string fileName = Path.Combine(Guid.NewGuid().ToString() + "_" + popular.Photo.FileName);

                    string newPath = Path.Combine(_webHostEnvironment.WebRootPath, "assets/images", fileName);


                    using (FileStream stream = new FileStream(newPath, FileMode.Create)) // Kompa sekil yuklemek ucun muhit yaradiriq stream yaradiriq 
                    {
                        await popular.Photo.CopyToAsync(stream);
                    }

                    dbPopular.Image = fileName;

                }
                else
                {
                    Popular newPopular = new()
                    {
                        Image = dbPopular.Image
                    };

                    dbPopular.Name = popular.Name;
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