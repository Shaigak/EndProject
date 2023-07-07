namespace MyEndProjectCode.ViewModels.WishViewModels
{
    public class WishProductVM
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal DiscountPrice { get; set; }
        public decimal Total { get; set; }
    }
}
