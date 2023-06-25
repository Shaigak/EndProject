using MyEndProjectCode.Models;

namespace MyEndProjectCode.ViewModels
{
    public class BlogDetailVM
    {
        public IEnumerable<Blog> Blogs { get; set; }
        public ICollection<BlogImage> BlogImages { get; set; }

        public string Title { get; set; }
        public string Description { get; set; }
    }
}
