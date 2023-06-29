using MyEndProjectCode.Models;

namespace MyEndProjectCode.Areas.View_Models
{
    public class BlogUpdateVM
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public ICollection<BlogImage> Images { get; set; }
        public List<IFormFile> Photos { get; set; }
    }
}
