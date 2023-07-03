using MyEndProjectCode.Helpers;
using MyEndProjectCode.Models;

namespace MyEndProjectCode.ViewModels
{
    public class BlogVM
    {
        public List<Blog> Blogs { get; set; }
        public Dictionary<string, string> Settings { get; set; }

        public Paginate<Blog> PaginateBlog { get; set; }
    }
}
