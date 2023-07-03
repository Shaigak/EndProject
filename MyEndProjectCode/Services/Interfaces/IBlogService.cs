using MyEndProjectCode.Models;

namespace MyEndProjectCode.Services.Interfaces
{
    public interface IBlogService
    {

        Task<List<Blog>> GetAll();

        Task<Blog> GetFullDataById(int id);

        Task<List<Blog>> GetBlogsRecently();

        Task<int> GetCountAsync();

        Task<List<Blog>> GetPaginatedDatas(int page, int take);
    }
}
