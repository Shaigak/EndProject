﻿using System.ComponentModel.DataAnnotations.Schema;

namespace MyEndProjectCode.Models
{
    public class Product:BaseEntity
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int SaleCount { get; set; }
        public int StockCount { get; set; }
        public string Description { get; set; }
        public int Rate { get; set; } = 5;

        public ICollection<ProductTag> ProductTags { get; set; }
        public ICollection<ProductImage> ProductImages { get; set; }

        public ICollection<ProductCategory> ProductCategories { get; set; }

        public ICollection<ProductBrand> ProductBrands { get; set; }

        [NotMapped]
        public IFormFile Photo { get; set; }

    }
}
