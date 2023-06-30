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
            return await _context.Products.Where(m=>!m.SoftDelete)
                                                                    .Include(m => m.ProductImages)

                                                                    .Include(m => m.ProductTags)

                                                                    .Include(m => m.ProductCategories)?

                                                                    .Take(6)
                                                                   

                                                                    .ToListAsync();
        }



        public async Task<List<Product>> GetAllProduct()
        {
            return await _context.Products.Where(m => !m.SoftDelete)
                                                                    .Include(m => m.ProductImages)

                                                                    .Include(m => m.ProductTags)

                                                                    .Include(m => m.ProductCategories)?

                                                                    


                                                                    .ToListAsync();
        }


        public async Task<Product> GetById(int id) => await _context.Products.Include(m => m.ProductImages).
                                                                  Include(m => m.ProductTags)
                                                                  .ThenInclude(m => m.Tag)
                                                                  
                                                                  .Include(m => m.ProductCategories)
                                                                  .ThenInclude(m => m.Category)
                                                                  .FirstOrDefaultAsync(m => m.Id == id);


        public async Task<int> GetCountAsync() => await _context.Products.CountAsync();


        public async Task<List<Product>> GetPaginatedDatas(int page, int take)
        {


            return await _context.Products
                                //.Include(m => m.ProductSizes)
                                //.Include(m => m.ProductTags)
                                //.Include(m => m.Color)
                                //.Include(m => m.Comments)
                                .Include(m => m.ProductCategories)?
                                .Include(m=>m.ProductImages)

                                //.Include(m => m.Images)
                                .Where(m => !m.SoftDelete)
                                .Skip((page * take) - take)
                                .Take(take).ToListAsync();

        }




        

    }
}
