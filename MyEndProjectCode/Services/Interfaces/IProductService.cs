using MyEndProjectCode.Models;

namespace MyEndProjectCode.Services.Interfaces
{
    public interface IProductService
    {
        Task<Product> GetById(int id);
        Task<List<Product>> GetAll();

        Task<List<Product>> GetPaginatedDatas(int page, int take);

        Task<int> GetCountAsync();

        Task<List<Product>> GetAllProduct();
    }
}
