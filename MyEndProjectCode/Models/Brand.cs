using System.ComponentModel.DataAnnotations.Schema;

namespace MyEndProjectCode.Models
{
    public class Brand : BaseEntity
    {
        public string Image { get; set; }

        [NotMapped]
        public IFormFile Photo { get; set; }
    }
}
