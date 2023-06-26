using System.ComponentModel.DataAnnotations.Schema;

namespace MyEndProjectCode.Models
{
    public class Slider:BaseEntity
    {
        public string Image { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string EndDesc { get; set; }

        [NotMapped]
        public IFormFile Photo { get; set; }

    }
}
