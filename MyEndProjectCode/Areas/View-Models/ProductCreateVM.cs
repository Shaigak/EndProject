namespace MyEndProjectCode.Areas.View_Models
{
    public class ProductCreateVM
    {
        public string Name { get; set; }
        public string Price { get; set; }
        public int SaleCount { get; set; }
        public int StockCount { get; set; }
        public string Description { get; set; }
        public int Rate { get; set; } = 5;


        public List<int> TagIds { get; set; }

        public List<int> CategoryIds { get; set; }


        public List<int> BrandIds { get; set; }


        public List<IFormFile> Photos { get; set; }
    }
}
