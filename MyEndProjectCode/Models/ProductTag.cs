namespace MyEndProjectCode.Models
{
    public class ProductTag:BaseEntity
    {
        public int TagId { get; set; }
        public Tag Tag { get; set; }

    }
}
