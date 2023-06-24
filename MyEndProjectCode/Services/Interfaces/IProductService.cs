using MyEndProjectCode.Models;

namespace MyEndProjectCode.Services.Interfaces
{
    public interface IProductService
    {
        Task<List<Product>> GetAll();
    }
}
