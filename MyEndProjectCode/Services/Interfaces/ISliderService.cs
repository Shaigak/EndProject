using MyEndProjectCode.Models;

namespace MyEndProjectCode.Services.Interfaces
{
    public interface ISliderService
    {
        Task<List<Slider>> GetAll();
    }
}
