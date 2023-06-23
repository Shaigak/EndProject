using MyEndProjectCode.Models;

namespace MyEndProjectCode.ViewModels
{
    public class HomeVM
    {
        public List<Slider> Sliders { get; set; }

        public List<Banner> Banners { get; set; }

        public List<Blog> Blogs { get; set; }

        public List<Popular> Populars { get; set; }

        public Dictionary<string, string> Settings { get; set; }
    }
}
