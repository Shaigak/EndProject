using MyEndProjectCode.Models;

namespace MyEndProjectCode.ViewModels
{
    public class HomeVM
    {
        public List<Slider> Sliders { get; set; }

        public List<Banner> Banners { get; set; }

        public Dictionary<string, string> Settings { get; set; }
    }
}
