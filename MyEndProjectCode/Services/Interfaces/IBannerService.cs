using MyEndProjectCode.Models;

namespace MyEndProjectCode.Services.Interfaces
{
    public interface IBannerService
    {
        Task<List<Banner>> GetAll();
    }
}
