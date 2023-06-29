using Microsoft.EntityFrameworkCore;
using MyEndProjectCode.Data;
using MyEndProjectCode.Models;
using MyEndProjectCode.Services.Interfaces;

namespace MyEndProjectCode.Services
{
    public class BlogService : IBlogService
    {

        private readonly AppDbContext _context;
        public BlogService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<List<Blog>> GetAll()
        {
            return await _context.Blogs.Where(m => !m.SoftDelete).Include(m => m.Images).ToListAsync();
        }



        public async Task<Blog> GetFullDataById(int id)
        {
            return await _context.Blogs.Include(m => m.Images)
                                                                        

                                                                         .FirstOrDefaultAsync(m => m.Id == id);
        }
    }
}
