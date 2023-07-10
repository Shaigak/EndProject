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
    public class BrandProController : Controller
    {

        private readonly AppDbContext _context;


        public BrandProController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {

            List<BrandPro> brandpros = await _context.BrandPros.ToListAsync();
            return View(brandpros);
        }


        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BrandProCreateVM brand)
        {

            var existdata = await _context.BrandPros.FirstOrDefaultAsync(m => m.Name == brand.Name);

            if (existdata is not null)

            {

                ModelState.AddModelError("Name", "This data already exist");
                return View();
            }
            BrandPro newBrand = new()
            {
                Name = brand.Name

            };

            await _context.BrandPros.AddAsync(newBrand);



            await _context.SaveChangesAsync();



            return RedirectToAction(nameof(Index));

        }



        public async Task<IActionResult> Delete(int? id)
        {

            try
            {
                if (id == null) return BadRequest();
                BrandPro brand = await _context.BrandPros.FirstOrDefaultAsync(m => m.Id == id);


                if (brand is null) return NotFound();



                _context.BrandPros.Remove(brand);

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
            if (id is null) return BadRequest();
            BrandPro brand = await _context.BrandPros.FindAsync(id);
            if (brand is null) return NotFound();

            return View(brand);
        }


        [HttpPost]
        public async Task<IActionResult> Edit(int? id, BrandPro brand)
        {
            if (id is null) return BadRequest();
            BrandPro dbBrand = await _context.BrandPros.AsNoTracking().FirstOrDefaultAsync(m => m.Id == id);
            if (dbBrand is null) return NotFound();

            if (dbBrand.Name == brand.Name)
            {
                return RedirectToAction(nameof(Index));
            }

            _context.BrandPros.Update(brand);

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }



    }

}