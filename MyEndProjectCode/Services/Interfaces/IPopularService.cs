using MyEndProjectCode.Models;

namespace MyEndProjectCode.Services.Interfaces
{
    public interface IPopularService
    {
        Task<List<Popular>> GetAll();
    }
}
