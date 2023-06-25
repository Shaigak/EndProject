using MyEndProjectCode.Models;

namespace MyEndProjectCode.Services.Interfaces
{
    public interface ICategoryService
    {

        Task<List<Category>> GetCategories();

    }
}
