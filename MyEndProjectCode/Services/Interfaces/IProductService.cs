using MyEndProjectCode.Models;

namespace MyEndProjectCode.Services.Interfaces
{
    public interface IProductService
    {
        Task<Product> GetById(int id);
        Task<List<Product>> GetAll();

        Task<List<Product>> GetPaginatedDatas(int page, int take,int? value1, int? value2);

        Task<int> GetCountAsync();

        Task<int> GetProductsCountByRangeAsync(int? value1, int? value2);

        Task<List<Product>> GetAllProduct();
    }
}
