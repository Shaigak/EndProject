using MyEndProjectCode.Models;
using System.ComponentModel.DataAnnotations;

namespace MyEndProjectCode.Areas.View_Models
{
    public class ProductUpdateVM
    {

        public int Id { get; set; }
        public List<IFormFile> Photos { get; set; }
        public List<ProductImage> Images { get; set; }
        [Required(ErrorMessage = "Don`t be empty")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Don`t be empty")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Don`t be empty")]
        public decimal Price { get; set; }
        [Required(ErrorMessage = "Don`t be empty")]
        public int SaleCount { get; set; }
        [Required(ErrorMessage = "Don`t be empty")]
        public int StockCount { get; set; }
        [Required(ErrorMessage = "Don`t be empty")]

        public List<int> TagIds { get; set; }
        [Required(ErrorMessage = "Don`t be empty")]
        public List<int> BrandIds { get; set; }
        [Required(ErrorMessage = "Don`t be empty")]
        public List<int> CategoryIds { get; set; }
    }
}
