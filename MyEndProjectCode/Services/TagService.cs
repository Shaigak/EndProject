using Microsoft.EntityFrameworkCore;
using MyEndProjectCode.Data;
using MyEndProjectCode.Models;
using MyEndProjectCode.Services.Interfaces;

namespace MyEndProjectCode.Services
{
    public class TagService:ITagService
    {
        private readonly AppDbContext _context;

        public TagService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Tag>> GetAllTags()
        {
            return await _context.Tags.Include(m => m.ProductTags)?/*ThenInclude(m => m.Product)?*/.Where(m => !m.SoftDelete).ToListAsync();
        }
    }
}
