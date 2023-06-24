using Microsoft.EntityFrameworkCore;
using MyEndProjectCode.Data;
using MyEndProjectCode.Models;
using MyEndProjectCode.Services.Interfaces;

namespace MyEndProjectCode.Services
{
    public class BrandService : IBrandService
    {
        private readonly AppDbContext _context;
        public BrandService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<List<Brand>> GetAll()
        {
            return await _context.Brands.Where(m => !m.SoftDelete).ToListAsync();
        }
    }
}
