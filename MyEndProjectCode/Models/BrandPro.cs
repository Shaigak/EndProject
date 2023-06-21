namespace MyEndProjectCode.Models
{
    public class BrandPro:BaseEntity
    {
        public string Name { get; set; }
        public ICollection<ProductBrand> ProductBrands { get; set; }
    }
}
