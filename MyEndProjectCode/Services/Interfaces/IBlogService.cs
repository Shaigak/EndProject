using MyEndProjectCode.Models;

namespace MyEndProjectCode.Services.Interfaces
{
    public interface IBlogService
    {

        Task<List<Blog>> GetAll();

        Task<Blog> GetFullDataById(int id);

        Task<List<Blog>> GetBlogsRecently();
    }
}
