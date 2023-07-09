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
                                                                    .Include(m => m.ProductBrands).ThenInclude(m => m.BrandPro)

                                                                  .FirstOrDefaultAsync(m => m.Id == id);


        public async Task<int> GetCountAsync() => await _context.Products.CountAsync();


        public async Task<List<Product>> GetPaginatedDatas(int page, int take, int? value1, int? value2)
        {


             


            List<Product> products = await  _context.Products

                                .Include(m => m.ProductCategories)?
                                .Include(m => m.ProductImages)

                                //.Include(m => m.Images)
                                .Where(m => !m.SoftDelete)
                                .Skip((page * take) - take)
                                .Take(take).ToListAsync();

            if (value1 != null && value2 != null)
            {
                products = await _context.Products
               .Include(p => p.ProductImages)
               .Where(p => p.Price >= value1 && p.Price <= value2)
               .Skip((page * take) - take)
               .Take(take)
               .ToListAsync();

            }

            return products;

        }

      

        //public Task<List<Product>> GetPaginatedDatas(int page, int take, int value1, int? value2)
        //{
        //    throw new NotImplementedException();
        //}

        public async Task<int> GetProductsCountByRangeAsync(int? value1, int? value2)
        {
            return await _context.Products.Where(p => p.Price >= value1 && p.Price <= value2)
                                 .Include(p => p.ProductImages)
                                 .CountAsync();
        }





    }
}
