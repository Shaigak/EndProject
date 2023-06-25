using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyEndProjectCode.Data;
using MyEndProjectCode.Models;
using MyEndProjectCode.ViewModels;

namespace MyEndProjectCode.Controllers
{
    public class AboutController : Controller
    {

        private readonly AppDbContext _context;


        public AboutController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            List<About> abouts = await  _context.Abouts.ToListAsync();
            List<History> historys = await _context.Historys.ToListAsync();
            List<Team> teams = await _context.Teams.ToListAsync();


            AboutVM model = new()
            {
                Abouts = abouts,
                Historys = historys,
                Teams = teams
                

            };

            return View(model);
        }
    }
}
