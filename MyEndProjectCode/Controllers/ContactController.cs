using Microsoft.AspNetCore.Mvc;
using MyEndProjectCode.Areas.View_Models;
using MyEndProjectCode.Data;
using MyEndProjectCode.Models;
using MyEndProjectCode.ViewModels;

namespace MyEndProjectCode.Controllers
{
    public class ContactController : Controller
    {

        private readonly AppDbContext _context;

        public ContactController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }



        public async Task<IActionResult> PostComment(ContactUserVM  user)
        {
            Contact newContact = new()
            {
                FullName = user.FullName,
                Subject = user.Subject,
                Message = user.Message,
                Email = user.Email,

            };

            await _context.AddAsync(newContact);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));



        }
    }
}
