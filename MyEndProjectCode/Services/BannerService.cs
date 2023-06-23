using Microsoft.EntityFrameworkCore;
using MyEndProjectCode.Data;
using MyEndProjectCode.Models;
using MyEndProjectCode.Services.Interfaces;

namespace MyEndProjectCode.Services
{
    public class BannerService:IBannerService
    {
        private readonly AppDbContext _context;

        public BannerService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Banner>> GetAll()
        {
            return await _context.Banners.Where(m => !m.SoftDelete).ToListAsync();
        }
    }
}
