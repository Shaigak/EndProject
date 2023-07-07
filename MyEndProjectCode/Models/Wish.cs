namespace MyEndProjectCode.Models
{
    public class Wish:BaseEntity
    {
        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }
        public ICollection<WishProduct> WishProducts { get; set; }
    }
}
