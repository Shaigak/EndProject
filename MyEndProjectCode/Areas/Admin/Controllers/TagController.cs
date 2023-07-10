using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyEndProjectCode.Areas.View_Models;
using MyEndProjectCode.Data;
using MyEndProjectCode.Models;
using MyEndProjectCode.Services.Interfaces;

namespace MyEndProjectCode.Areas.Admin.Controllers
{

    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class TagController : Controller
    {

        private readonly AppDbContext _context;
        private readonly ITagService _tagService;

        public TagController(AppDbContext context, ITagService tagService)
        {
            _context = context;
            _tagService = tagService;
        }

        public async Task<IActionResult> Index()
        {
            List<Tag> tag = await _tagService.GetAllTags();

            return View(tag);
        }


        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TagCreateVM tag)
        {

            var existdata = await _context.Tags.FirstOrDefaultAsync(m => m.Year == tag.Year);

            if (existdata is not null)

            {

                ModelState.AddModelError("Name", "This data already exist");
                return View();
            }
            Tag newTag = new()
            {
                Year = tag.Year

            };

            await _context.Tags.AddAsync(newTag);



            await _context.SaveChangesAsync();



            return RedirectToAction(nameof(Index));

        }


        public async Task<IActionResult> Delete(int? id)
        {

            try
            {
                if (id == null) return BadRequest();
                Tag tag = await _context.Tags.FirstOrDefaultAsync(m => m.Id == id);


                if (tag is null) return NotFound();



                _context.Tags.Remove(tag);

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
            Tag tag = await _context.Tags.FindAsync(id);
            if (tag is null) return NotFound();

            return View(tag);
        }



        [HttpPost]
        public async Task<IActionResult> Edit(int? id, Tag tag)
        {
            if (id is null) return BadRequest();
            Tag dbTag = await _context.Tags.AsNoTracking().FirstOrDefaultAsync(m => m.Id == id);
            if (dbTag is null) return NotFound();

            if (dbTag.Year == tag.Year)
            {
                return RedirectToAction(nameof(Index));
            }

            _context.Tags.Update(tag);

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }





    }
}
