using System.ComponentModel.DataAnnotations.Schema;

namespace MyEndProjectCode.Models
{
    public class Blog : BaseEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }
   
        public ICollection<BlogImage> Images { get; set; }

        [NotMapped]
        public IFormFile Photo { get; set; }

        public ICollection<BlogComment> BlogComments { get; set; }


    }
}
