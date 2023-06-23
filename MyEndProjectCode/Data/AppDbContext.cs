using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MyEndProjectCode.Models;

namespace MyEndProjectCode.Data
{
    public class AppDbContext: DbContext
    {

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }


        public DbSet<Settings> Settings { get; set; }
        public DbSet<About> Abouts { get; set; }

        public DbSet<Slider> Sliders { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<Adversting> Adverstings { get; set; }

        public DbSet<Contact> Contacts { get; set; }

        public DbSet<Team> Teams { get; set; }

        public DbSet<Banner> Banners { get; set; }

        public DbSet<BrandPro> BrandPros { get; set; }
        public DbSet<Category> Categories { get; set; }

        public DbSet<Tag> Tags { get; set; }

        public DbSet<Popular> Populars { get; set; }
        public DbSet<Brand> Brands { get; set; }

        public DbSet<Blog> Blogs { get; set; }
        public DbSet<BlogImage> BlogImages { get; set; }

        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
      
        public DbSet<ProductTag> ProductTags { get; set; }

        public DbSet<ProductBrand> ProductBrands{ get; set; }

        public DbSet<BlogComment> BlogComments { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }



}
