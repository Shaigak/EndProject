namespace MyEndProjectCode.Models
{
    public class Blog : BaseEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }
   
        public ICollection<BlogImage> Images { get; set; }

        public IFormFile Photo { get; set; }

        public ICollection<BlogComment> BlogComments { get; set; }


    }
}
