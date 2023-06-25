using MyEndProjectCode.Models;

namespace MyEndProjectCode.Services.Interfaces
{
    public interface ITagService
    {
        Task<List<Tag>> GetAllTags();
    }
}
