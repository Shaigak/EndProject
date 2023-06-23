using MyEndProjectCode.Models;

namespace MyEndProjectCode.Services.Interfaces
{
    public interface IBlogService
    {

        Task<List<Blog>> GetAll();
    }
}
