namespace MyEndProjectCode.Areas.View_Models
{
    public class BlogCreateVM
    {
        public string Title { get; set; }
        public string Description { get; set; }

        public List<IFormFile> Photos { get; set; }
    }
}
