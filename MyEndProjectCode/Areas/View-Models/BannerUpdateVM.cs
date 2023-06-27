namespace MyEndProjectCode.Areas.View_Models
{
    public class BannerUpdateVM
    {

        public int Id { get; set; }

        public string Description { get; set; }

        public string Image { get; set; }

        public IFormFile Photo { get; set; }
    }
}
