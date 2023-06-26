namespace MyEndProjectCode.Areas.View_Models
{
    public class SliderUpdateVM
    {
        public int Id { get; set; }

        public string Title { get; set; }
        public string Description { get; set; }

        public string EndDesc { get; set; }


        public string Image { get; set; }

        public IFormFile Photo { get; set; }
    }
}
