using Microsoft.EntityFrameworkCore;
using MyEndProjectCode.Data;
using MyEndProjectCode.Models;
using MyEndProjectCode.Services.Interfaces;

namespace MyEndProjectCode.Services
{
    public class PopularService : IPopularService
    {

        private readonly AppDbContext _context;
        public PopularService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<List<Popular>> GetAll()
        {
            return await _context.Populars.Where(m => !m.SoftDelete).ToListAsync();
        }
    }
}
