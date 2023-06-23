using Microsoft.EntityFrameworkCore;
using MyEndProjectCode.Data;
using MyEndProjectCode.Models;
using MyEndProjectCode.Services.Interfaces;

namespace MyEndProjectCode.Services
{
    public class SliderService : ISliderService
    {


        private readonly AppDbContext _context;
        public SliderService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<List<Slider>> GetAll()
        {
            return await _context.Sliders.Where(m => !m.SoftDelete).ToListAsync();
        }
    }
}
