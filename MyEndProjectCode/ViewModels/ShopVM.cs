using MyEndProjectCode.Helpers;
using MyEndProjectCode.Models;

namespace MyEndProjectCode.ViewModels
{
    public class ShopVM
    {
      public List<Product> Products { get; set; }

      public List<Category> Categories { get; set; }

      public List<BrandPro> BrandPros { get; set; }

      public List<Tag> Tags { get; set; }

      public Paginate<Product> PaginateProduct { get; set; }

    }
}
