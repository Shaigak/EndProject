using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyEndProjectCode.Data;
using MyEndProjectCode.Models;

namespace MyEndProjectCode.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class ContactUserController : Controller
    {
        private readonly AppDbContext _context;

        public ContactUserController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            List<Contact> contacts = await _context.Contacts.ToListAsync();

            return View(contacts);
        }
    }
}
