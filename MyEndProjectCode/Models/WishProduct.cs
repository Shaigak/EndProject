namespace MyEndProjectCode.Models
{
    public class WishProduct:BaseEntity
    {
        public int Quantity { get; set; }
        public int WishId { get; set; }
        public Wish Wish { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}
