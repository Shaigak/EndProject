namespace MyEndProjectCode.Models
{
    public class Tag:BaseEntity
    {
        public int Year { get; set; }
        public ICollection<ProductTag> ProductTags { get; set; }
    }
}
