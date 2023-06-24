using Microsoft.EntityFrameworkCore;
using MyEndProjectCode.Data;
using MyEndProjectCode.Models;
using MyEndProjectCode.Services.Interfaces;

namespace MyEndProjectCode.Services
{
    public class ProductService : IProductService
    {
        private readonly AppDbContext _context;
        public ProductService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Product>> GetAll()
            {
                return await _context.Products
                                                                        .Include(m => m.ProductImages)
                                                                        
                                                                        .Include(m => m.ProductTags)
                                                                   
                                                                        .Include(m => m.ProductCategories)?

                                                                        .ToListAsync();
            }
        
    }
}
